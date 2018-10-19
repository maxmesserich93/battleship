using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.ServiceReference1;
using Models;

namespace ViewModel.Service
{

    public class RemoteGameService : AbstractGameServiceViewModel
    {
       ConsoleApp1.ServiceReference1.IGameContract Service;


        public static void CreateRemoteGameService(string playerId)
        {
            RemoteCallback remoteCallback = new RemoteCallback();
            InstanceContext context = new InstanceContext(remoteCallback);
            GameContractClient a = new GameContractClient(context);




        }



        public RemoteGameService(string playerId) : base(new RemoteCallback())
        {

            InstanceContext context = new InstanceContext(Callback);
            try
            {
                ///instanceContext needs a reference to the server

                Service = new GameContractClient(context);

                //server.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(0.5);
                //EndpointAddress address = new EndpointAddress("http://localhost:8000/Service/Service");
                //server.Endpoint.Address = address;



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        public override Task<string> AwaitLoginResult(string userName)
        {
            return Service.LoginAsync(userName);
        }

        public override Task<List<GameInformation>> AwaitGameList()
        {
            return Service.GetAvailableGamesAsync(PlayerIdentity).ContinueWith(arrayResult => arrayResult.Result.ToList());
        }

        public override Task AwaitHostGame()
        {
            return Service.HostGameAsync(PlayerIdentity);
        }

        public override Task AwaitBotGame()
        {
            return Service.JoinBotGameAsync(PlayerIdentity);
        }

        public override Task AwaitJoinGame(string gameId)
        {
            return Service.JoinGameAsync(PlayerIdentity, gameId);
        }

        public override async Task AwaitProvideShipPlacements(List<Ship> shipPlacements)
        {
 

            await Service.ProvideShipPlacementsAsync(PlayerIdentity, shipPlacements.ToArray());
        }

        public override Task AwaitShotPlacement(Coordinate coordinate)
        {
            return Service.ProvideShotPlacementAsync(PlayerIdentity, coordinate);
        }

        public override void Close()
        {
            Service.Logout(PlayerIdentity);
        }



        //public async override void GetAvailableGames()
        //{
        //    var list = await Service.GetAvailableGamesAsync();
        //    base.GameListHandler.Invoke(list.ToList());

        //}

        //public override void HostGame()
        //{
        //    Service.HostGame(PlayerIdentity);
        //}

        //public override void JoinBotGame()
        //{
        //    Service.JoinBotGameAsync(PlayerIdentity);
        //}

        //public override void JoinGame(string gameId)
        //{
        //    Service.JoinGame(PlayerIdentity, gameId);
        //}



        //public override void ProvideShipPlacements(List<ShipPlacement> shipPlacements)
        //{
        //    Service.ProvideShipPlacementsAsync(PlayerIdentity, shipPlacements.ToArray());
        //}

        //public override void ProvideShotPlacement(Coordinate coordinate)
        //{
        //    Service.ProvideShotPlacementAsync(PlayerIdentity, coordinate);
        //}


    }
}
