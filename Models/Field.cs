using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;

namespace Models
{
    [DataContract]
    public class Field
    {
        /// <summary>
        /// 2D array for containing the positions of the field.
        /// </summary>
        /// 
        [DataMember]
        private List<List<FieldPosition>> _fieldData;
        /// <summary>
        /// The List of placed ships on the field.
        /// </summary>
        [DataMember]
        public List<Ship> Ships { get; set; }

        [DataMember]
        public int Size { set; get; }

        //public GameRuleSet GameRuleSet { set; get; }

        public Field(int size)
        {
            Ships = new List<Ship>();
            //GameRuleSet = gameRuleSet;
            Size = size;
            _fieldData = new List<List<FieldPosition>>();
            for(int x=0; x< Size; x++)
            {
                var row = new List<FieldPosition>();
                for(int y=0; y< Size; y++)
                {
                    Coordinate coordinate = new Coordinate(x, y);
                    row.Add(new FieldPosition(FieldPositionStatus.Default, coordinate));
                }
                _fieldData.Add(row);
            }
            


        }

        public FieldPosition _getPosition(Coordinate c)
        {
            return _fieldData[c.X][c.Y];
        }

        public void SetField(Coordinate coordinate, FieldPositionStatus fieldPositionStatus)
        {
            _getPosition(coordinate).FieldPositionStatus = fieldPositionStatus;
        }

        public void Hover(Coordinate coordinate, bool value)
        {
            _getPosition(coordinate).Hover = value;
        }


        public int ShipTypeCount(string shipTypeName)
        {
            return Ships.Count(s => s.Equals(shipTypeName));
        }

        public int ShipTypeCount(Ship shipType)
        {
            return Ships.Count(s => s.Type.Equals(shipType.Type));
        }

        /// <summary>
        /// Returns true if the provided ship can be placed at the provided coordinate.
        /// </summary>
        /// <param name="ship"></param>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public bool ShipPlaceable(Ship ship)
        {
            
            Coordinate coordinate = ship.StartCoordinate;

                
            //if (  Ships.Count(s => s.Type.Equals(ship.Type)) == GameRuleSet.GetShipTypeCount(ship))
            //{
            //    return false;
            //}


            //Bounds-check
            if (ship.IsVertical)
            {
                for (int i = 0; i < ship.Length; i++)
                {
                    FieldPosition position = GetPosition(coordinate.X, coordinate.Y + i);
                    if (position == null || position.FieldPositionStatus != FieldPositionStatus.Default)
                    {
                        return false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < ship.Length; i++)
                {
                    FieldPosition position = GetPosition(coordinate.X+i, coordinate.Y);
                    if (position == null || position.FieldPositionStatus != FieldPositionStatus.Default)
                    {
                        return false;
                    }

                }
            }
            return true;
        }



        public bool PlaceShip(Ship ship)
        {
            return PlaceShip(ship, ship.IsVertical, ship.StartCoordinate);
        }

        /// <summary>
        /// Places the ship at the provided coordinate.
        /// </summary>
        /// <param name="ship"></param>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public bool PlaceShip(Ship ship, bool vertical, Coordinate coordinate)
        {
            ship.IsVertical = vertical;
            ship.StartCoordinate = coordinate;
            if (ShipPlaceable(ship))
            {


                ship.StartCoordinate = coordinate;
                if (ship.IsVertical)
                {
                    for (int i = 0; i < ship.Length; i++)
                    {
                        
                        FieldPosition position = GetPosition(coordinate.X, coordinate.Y + i);
                        position.FieldPositionStatus = FieldPositionStatus.Ship;
                    }
                }
                else
                {
                    for (int i = 0; i < ship.Length; i++)
                    {
                        FieldPosition position = GetPosition(coordinate.X + i, coordinate.Y);
                        position.FieldPositionStatus = FieldPositionStatus.Ship;
                    }
                }
                Ships.Add(ship);
                return true;


            }

            return false;
        }
        /// <summary>
        /// Shoots the field at the provided Coordinate.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns>
        /// 
        /// 
        /// </returns>
        public List<FieldPosition> ShootCoordinate(Coordinate coordinate)
        {
            if(!IsInBound(coordinate.X, coordinate.Y))
            {
                return null;
            }

            FieldPosition position = GetPosition(coordinate);
            FieldPositionStatus currentStatus = position.FieldPositionStatus;
            List<FieldPosition> list = new List<FieldPosition>();


            var result = new List<FieldPositionStatus>();
            switch (currentStatus)
            {
                
  
                case (FieldPositionStatus.Default):
                    position.FieldPositionStatus = FieldPositionStatus.ShotMiss;
                    list.Add(position);
                    break;
                case FieldPositionStatus.Ship:
                    position.FieldPositionStatus = FieldPositionStatus.ShotHit;
                    /**
                     * Return the first not killed ship where the shot coordinate is part of the ship.
                    */
                    Ship hitShip = Ships.Where(ship => !ship.IsKilled()).First(ship => ship.IsCoordinatePartOfShip(coordinate));




                    if (hitShip != null)
                    {

                        if (hitShip.ShootShip())
                        {
                           //Get all coordinates of the killed ship.
                           List<Coordinate> shipCoordinates = hitShip.GetCoordinates();
                           shipCoordinates.ForEach(s => GetPosition(s).FieldPositionStatus = FieldPositionStatus.ShotKill);
                           list = shipCoordinates.Select(s => GetPosition(s)).ToList();

                        }
                        else
                        {
                            position.FieldPositionStatus = FieldPositionStatus.ShotHit;
                            list.Add(position);
                        }
                    }
                    break;
                default:
                    break;

            }
            //PrintField();
            return list;
        }


        /// <summary>
        /// Returns true if the provided coordinate is within the bounds of the the field.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsInBound(int x, int y)
        {

            return x > -1 && x < Size && y > -1 && y < Size;
        }
        /// <summary>
        /// Returns the FieldPosition at the provided coordinate.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public FieldPosition GetPosition(int x, int y)
        {
            if (!IsInBound(x, y))
            {

                return null;
            }

            return _getPosition(new Coordinate(x, y));
        }

        public FieldPosition GetPosition(Coordinate coordinate)
        {
            return GetPosition(coordinate.X, coordinate.Y);
            

        }

        public void PrintField()
        {
            //Debug.WriteLine("--       F I E L D       --");
            _fieldData.ForEach(row =>
            {

                var rowList =  row.Select(f => {
                    switch (f.FieldPositionStatus)
                    {
                        case (FieldPositionStatus.ShotHit):
                            {
                                return " x ";

                            }
                        case (FieldPositionStatus.ShotKill):
                            {
                                return " X ";
                            }
                        case (FieldPositionStatus.ShotMiss):
                            {
                                return " # ";
                            }
                        case (FieldPositionStatus.Ship):
                            {
                                return " S ";
                            }
                        default:
                            {
                                return " o ";
                            }
                    }
                });
                rowList.ToList().ForEach(a => Debug.Write(a));
                Debug.WriteLine("");
            });
            //Debug.WriteLine("----------------------------------------------");

        }

        public ObservableCollection<FieldPosition> GetData()
        {
            ObservableCollection<FieldPosition> list = new ObservableCollection<FieldPosition>();
           for(int i=0; i<Size; i++)
            {
                for (int a = 0; a < Size; a++)
                {
                    list.Add(_getPosition(new Coordinate(a,i)));
                }

            }

            return list;
        }

        public int CountHitPositions()
        {
            return _fieldData.SelectMany(list => list).Count(fieldPosition => fieldPosition.FieldPositionStatus == FieldPositionStatus.ShotHit || fieldPosition.FieldPositionStatus == FieldPositionStatus.ShotKill);
        }


    }
}
