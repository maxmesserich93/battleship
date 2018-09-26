
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Player
{
    public interface IPlayer
    {

        PlayerData PlayerData { set; get; }
        //Field OpponentRepresentation { set; get; }


        Coordinate PlaceShot(Field field);
        void PlaceShips();




    }
}
