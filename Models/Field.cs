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
        private FieldPosition[,] _fieldData;
        /// <summary>
        /// The List of placed ships on the field.
        /// </summary>
        //[DataMember]
        public List<Ship> Ships { get; set; }

        //[DataMember]
        public int Size { set; get; }

        public Field(int size)
        {
            Ships = new List<Ship>();
            Size = size;
            _fieldData = new FieldPosition[size, size];
            for(int x=0; x<size; x++)
            {
                for(int y=0; y<size; y++)
                {
                    Coordinate coordinate = new Coordinate(x, y);
                    _fieldData[x, y] = new FieldPosition(FieldPositionStatus.Default, coordinate);
                }
            }


        }

        public void SetField(Coordinate coordinate, FieldPositionStatus fieldPositionStatus)
        {
            Debug.WriteLine("   SET FIELD: "+coordinate+" : "+fieldPositionStatus);
            _fieldData[coordinate.X, coordinate.Y].FieldPositionStatus = fieldPositionStatus;
        }

        public void Hover(Coordinate coordinate, bool value)
        {
            _fieldData[coordinate.X, coordinate.Y].Hover = value;
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
            if(coordinate == null)
            {
                throw new NullReferenceException("StartCoordinate of the ship can not be null");
            }
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
            Debug.WriteLine("ADDED THE SHIP "+ship);
            return true;
        }



        public Ship PlaceShip(ShipPlacement placement)
        {
            return PlaceShip(placement.ShipType, placement.IsVertical, placement.StartCoordinate);
        }

        /// <summary>
        /// Places the ship at the provided coordinate.
        /// </summary>
        /// <param name="ship"></param>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public Ship PlaceShip(ShipType shipType, bool vertical, Coordinate coordinate)
        {
            Ship ship = new Ship(shipType);
            ship.IsVertical = vertical;
            ship.StartCoordinate = coordinate;

            if(ShipPlaceable(ship))
            {

                Ships.Add(ship);
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
                return ship;


            }

            return null;
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
            List<FieldPosition> list = null;
            switch (currentStatus)
            {
  
                case (FieldPositionStatus.Default):
                    position.FieldPositionStatus = FieldPositionStatus.ShotMiss;
                    list = new List<FieldPosition>();
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
                        var fieldPositionStatuses = new List<FieldPositionStatus>();
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
                            list = new List<FieldPosition>();
                            list.Add(position);
                        }
                    }
                    break;
                default:
                    return null;

            }
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

            return _fieldData[x, y];
        }

        public FieldPosition GetPosition(Coordinate coordinate)
        {
            return GetPosition(coordinate.X, coordinate.Y);
            

        }

        

        public ObservableCollection<FieldPosition> GetData()
        {
            ObservableCollection<FieldPosition> list = new ObservableCollection<FieldPosition>();
           for(int i=0; i<Size; i++)
            {
                for (int a = 0; a < Size; a++)
                {
                    list.Add(_fieldData[a, i]);
                }

            }

            return list;
        }


    }
}
