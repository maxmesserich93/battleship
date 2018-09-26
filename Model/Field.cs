using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class Field
    {
        /// <summary>
        /// 2D array for containing the positions of the field.
        /// </summary>
        /// 
        [DataMember]
        public FieldPosition[,] _fieldData;
        /// <summary>
        /// The List of placed ships on the field.
        /// </summary>
        [DataMember]
        public List<AbstractShip> Ships { get; set; }

        [DataMember]
        public int Size { set; get; }

        public Field(int size)
        {
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
        /// <summary>
        /// Returns true if the provided ship can be placed at the provided coordinate.
        /// </summary>
        /// <param name="ship"></param>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public bool ShipPlaceableAt(AbstractShip ship, Coordinate coordinate)
        {
            //Bounds-check
            if (ship.IsVertical)
            {
                for (int i = 0; i < ship.Length; i++)
                {
                    FieldPosition position = GetPosition(coordinate.X, coordinate.Y + i);
 
                    Debug.WriteLine("VALUE: " + position);


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
                    Debug.WriteLine("HORI: Checking: " + coordinate);
                    if (position == null || position.FieldPositionStatus != FieldPositionStatus.Default)
                    {
                        return false;
                    }

                }
            }

            return true;
        }

        public bool PlaceShip(AbstractShip ship, Coordinate coordinate)
        {

            if(ShipPlaceableAt(ship, coordinate))
            {
                Ships.Add(ship);
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
                return true ;


            }
            Debug.WriteLine("CAN NOT PLACE SHIP AT " + coordinate);
            return false;
        }

        

        /// <summary>
        /// Returns true if the provided coordinate is within the bounds of the the field.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsInBound(int x, int y)
        {
            Debug.WriteLine("IsInBound: " + x+"," + y);
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
            return GetPosition(new Coordinate(coordinate.X, coordinate.Y));
            

        }


        public AbstractShip GetShipAtPosition(Coordinate coordinate)
        {



            return null;
        }
    }
}
