using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ViewModel.Service
{
    /// <summary>
    /// Abstract class for wrapping a GameService which may be defined by implementing the service reference Contract or the IGameContract if local.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractGameServiceViewModel
    {

        public AbstractCallback Callback { get; set; }
        protected String PlayerIdentity { get; set; }

        public AbstractGameServiceViewModel(AbstractCallback callback)
        {

            Callback = callback;
            Callback.ProvidedIdentityHandler = (string a) => { PlayerIdentity = a; Debug.WriteLine("Received IDENTITY: " + a); };
        }

        public abstract Task<string> AwaitLoginResult(string userName);


        public delegate void HandleIdentity(string id);
        public HandleIdentity IdentityHandler { set; get; }


        public async void Login(string userName)
        {
            await AwaitLoginResult(userName).ContinueWith((task) =>
            {
                Debug.WriteLine("Received Identity: " + task.Result);
                PlayerIdentity = task.Result;
                IdentityHandler?.Invoke(task.Result);

            });
        }




        public IPlayerContract Player { set; get; }


        public delegate void HandleGameList(List<GameInformation> gameData);
        public HandleGameList GameListHandler { set; get; }
        public abstract Task<List<GameInformation>> AwaitGameList();
        public async void GetAvailableGames()
        {
            await AwaitGameList().ContinueWith((list) =>
            {
                Debug.WriteLine("Received avaiable games: ");
                list.Result.Select(game => game.OpenDate).ToList().ForEach(date => Debug.WriteLine(date));
                GameListHandler?.Invoke(list.Result);
            });


        }

        public abstract Task AwaitHostGame();
        public async void HostGame() {
            await AwaitHostGame();

        }

        public abstract Task AwaitBotGame();


        public async void JoinBotGame()
        {
            await AwaitBotGame();

        }

        public abstract Task AwaitJoinGame(string gameId);

        public async void JoinGame(string gameId)
        {
            await AwaitJoinGame(gameId);
        }

        public abstract Task AwaitProvideShipPlacements(List<Ship> shipPlacements);

        public delegate void HandleShipPlacement(bool response);
        public HandleShipPlacement ShipPlacementHandler { set;get;}

        public async void ProvideShipPlacements(List<Ship> shipPlacements)
        {
            var task = AwaitProvideShipPlacements(shipPlacements);
            await task;
            task.Dispose();
            //throw new Exception("DONE");


        }


        public abstract Task AwaitShotPlacement(Coordinate coordinate);

        public async void ProvideShotPlacement(Coordinate coordinate)
        {

            await AwaitShotPlacement(coordinate);
        }
        public abstract void Close();
    }
}
