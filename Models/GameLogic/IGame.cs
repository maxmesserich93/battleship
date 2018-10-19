using System.Collections.Generic;
using Models.Player;

namespace Models
{
    public interface IGame
    {
        bool PlaceShips(string playerId, List<Ship> placements);
        void Shoot(string playerId, Coordinate coordinate);
    }
}