using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GameService

{
    /// <summary>
    /// The service requires a session representing a game.
    /// The IPlayerContract specifying the callbacks to client
    /// is the CallbackContract of the IGameContract.
    /// </summary>
    [ServiceContract(Namespace = "http://fontysvenlo.org/DieService", SessionMode = SessionMode.Required,
        CallbackContract = typeof(IPlayerContract))]
    public interface IGameContract
    {
        [OperationContract]
        void JoinGame(String name);
        [OperationContract]
        void PlaceShips(List<int> placements);
        [OperationContract]
        void ShootCoordinate(int coordinate);

    }
}
