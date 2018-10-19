using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public static class ShipFactory
    {

        public static Ship CREATE_SHIP(string shipTypeName)
        {


            if (shipTypeName.Equals(nameof(Carrier)))
            {
                return new Carrier();
            }
            if (shipTypeName.Equals(nameof(BattleShip)))
            {
                return new BattleShip();
            }
            if (shipTypeName.Equals(nameof(Cruiser)))
            {
                return new Cruiser();
            }
            if (shipTypeName.Equals(nameof(Submarine)))
            {
                return new Submarine();
            }
            if (shipTypeName.Equals(nameof(Destroyer)))
            {
                return new Destroyer();
            }
            return null;
        }
        public static Ship CREATE_SHIP(string shipTypeName, Coordinate coordinate, bool isVertical)
        {
            var ship = CREATE_SHIP(shipTypeName);
            if (ship == null) { throw new Exception("BAD"); }
            ship.StartCoordinate = coordinate;
            ship.IsVertical = isVertical;
            return ship;
        }

    }
}
