using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GameService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Service : IGameContract
    {
        public void JoinGame(string name)
        {
            
        }

        public void PlaceShips(List<int> placements)
        {

        }

        public void ShootCoordinate(int coordinate)
        {

        }
    }
}
