using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public abstract class AbstractGameServiceViewModel : INotifyPropertyChanged
    {


        protected interface ITypedTaskUtil<out R>{}

        protected class TypedTaskUtil<R> : ITypedTaskUtil<R> {

            public Task CreateTask(Task<R> inputTask, Action<R> onComplete, Action<Exception> onException)
            {
                return inputTask
                    .ContinueWith(task => onComplete(task.Result), TaskContinuationOptions.NotOnFaulted)
                    .ContinueWith(exceptionTask => onException(exceptionTask.Exception), TaskContinuationOptions.OnlyOnFaulted)
                    .ContinueWith(canceledTask => { Debug.WriteLine("CANCELED: " + canceledTask); }, TaskContinuationOptions.OnlyOnCanceled);
                    

            }

            public Task CreateTask(Func<R> input, Action<R> onComplete, Action<Exception> onException)
            {
                return Task<R>.Factory.StartNew(() => input())
                    .ContinueWith(completedTask => onComplete(completedTask.Result), TaskContinuationOptions.OnlyOnRanToCompletion)
                    .ContinueWith(exceptionTask => onException(exceptionTask.Exception), TaskContinuationOptions.OnlyOnFaulted)
                    .ContinueWith(canceledTask => { Debug.WriteLine("CANCELED: " + canceledTask); }, TaskContinuationOptions.OnlyOnCanceled);
            }

        }




        public AbstractCallback Callback { get; set; }
        protected String PlayerIdentity { get; set; }
        private Exception _serverException;

        public event PropertyChangedEventHandler PropertyChanged;

        public Exception ServerException { get { return _serverException; } set { _serverException = value; NotifyPropertyChanged(nameof(ServerException)); } }
        public AbstractGameServiceViewModel(AbstractCallback callback)
        {

            Callback = callback;
            Callback.ProvidedIdentityHandler = (string a) => { PlayerIdentity = a; Debug.WriteLine("Received IDENTITY: " + a); };
        }

        public abstract Task<string> AwaitLoginResult(string userName);

        //public abstract Func<string> LoginFunc(string userName);


        //public abstract async Task<string> Bla { set; get; }

        public delegate void HandleIdentity(string id);
        public HandleIdentity IdentityHandler { set; get; }


        public async void Login(string userName)
        {



            var t = new TypedTaskUtil<string>();
            await t.CreateTask(AwaitLoginResult(userName), (id) =>
            {
                Debug.WriteLine("Received Identity: " + id);
                PlayerIdentity = id;
                IdentityHandler?.Invoke(id);
            }, (e) => { Debug.Write("EXCEPTION: " + e); });




            //await AwaitLoginResult(userName).ContinueWith((task) =>
            //{
            //    Debug.WriteLine("Received Identity: " + task.Result);
            //    PlayerIdentity = task.Result;
            //    IdentityHandler?.Invoke(task.Result);

            //}, TaskContinuationOptions.OnlyOnRanToCompletion)
            //.ContinueWith(exceptionTask =>
            //{
            //    ServerException = exceptionTask.Exception;
            //    Debug.WriteLine("ERROR: " + ServerException);
            //}, TaskContinuationOptions.OnlyOnFaulted)
            //.ContinueWith(canceledTask => Debug.WriteLine(canceledTask), TaskContinuationOptions.OnlyOnCanceled);

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
            }).ContinueWith(task =>
            {
                ServerException = task.Exception;
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




        public abstract void PlayerReady();



        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
