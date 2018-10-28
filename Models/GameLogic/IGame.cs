using System.Collections.Generic;
using Models.Player;

namespace Models
{
    public interface IGame
    {
        void PlayerReady(string playerId);
        bool PlaceShips(string playerId, List<Ship> placements);
        void Shoot(string playerId, Coordinate coordinate);
    }
}