using Models.Player;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Models.GameServiceImp
{
    //Per session because this allows multiple game instaces to run simulataniosly.
    //Each game has its own session.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class GameService : AbstractGameService, IGameContract
    {


        static Func<IPlayerContract> playerResolver = () => OperationContext.Current.GetCallbackChannel<IPlayerContract>();

        public GameService() : base(playerResolver, null)
        {
        }
    }
}
