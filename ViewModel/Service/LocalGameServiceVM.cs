using ConsoleApp1.ServiceReference1;
using Models;
using Models.GameServiceImp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Service
{
    public class LocalGameServiceVM : AbstractGameServiceViewModel
    {
        public LocalGameService Service { set; get; }
        public LocalGameServiceVM(LocalCallback localCallback, string playerName) : base(localCallback)
        {
            Service = new LocalGameService(() => localCallback);
            Service.Login(playerName);
           
        }

        public override Task<string> AwaitLoginResult(string userName)
        {
            Debug.WriteLine("LocalGameServiceVM AwaitLogin: " + userName);
            var task = new Task<string>(() => { Debug.WriteLine("Running local login task "); return Service.Login(userName); });
            task.Start();
            return task;
        }

        public override Task<List<GameInformation>> AwaitGameList()
        {
            var task =  new Task<List<GameInformation>>(() => Service.GetAvailableGames(PlayerIdentity));
            task.Start();
            return task;


        }
        public override Task AwaitHostGame()
        {
            var task =  new Task(() => Service.HostGame(PlayerIdentity));
            task.Start();
            return task;
        }

        public override Task AwaitBotGame()
        {
            Debug.WriteLine("Local AwaitBotGame; " + PlayerIdentity);
            var task = new Task(() => Service.JoinBotGame(PlayerIdentity));
            task.Start();
            return task;
        }

        public override Task AwaitJoinGame(string gameId)
        {
            var task = new Task(() => Service.JoinGame(PlayerIdentity, gameId));
            task.Start();
            return task;
        }

        public override  Task AwaitProvideShipPlacements(List<Ship> shipPlacements)
        {
            var task =  new Task(() => Service.ProvideShipPlacements(PlayerIdentity, shipPlacements));
            task.Start();
            return task;
        }

        public override Task AwaitShotPlacement(Coordinate coordinate)
        {
            var task = new Task(() => Service.ProvideShotPlacement(PlayerIdentity, coordinate));
            task.Start();
            return task;
        }
        public override void Close()
        {
            Service.Logout(PlayerIdentity);
        }

        public override void PlayerReady()
        {
            Service.PlayerLoaded(PlayerIdentity);

        }


    }
}
