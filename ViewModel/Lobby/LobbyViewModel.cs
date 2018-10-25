using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Service;

namespace ViewModel.Lobby
{
    public class LobbyViewModel : AbstractServiceViewModel,INotifyPropertyChanged
    {

        /// <summary>
        /// Contains all joinable games on the server.
        /// </summary>
        List<GameInformation> _games;
        public ShipPlacementViewModel ShipPlacementViewModel { set; get; }

        public Command RefreshGameListCommand { get; }

        public Command JoinGameCommand { get; }

        public Command HostGameCommand { get; }
       
        public Command BotGameCommand { get; }


        public List<GameInformation> Games
        {
            get { return _games; }
            private set
            {
                _games = value;
                OnPropertyChanged(nameof(Games));
            }
        }
            /// <summary>
            /// Selected Game
            /// </summary>
            GameInformation _data;

        public LobbyViewModel(AbstractGameServiceViewModel gameService) : base(gameService)
        {
            GameService.GameListHandler = (data) => { Games = data; Debug.WriteLine("Received Games: " + data); };
            RefreshGameListCommand = new Command(() => base.GameService.GetAvailableGames());
            JoinGameCommand = new Command(() => SelectedGame != null, () => base.GameService.JoinGame(SelectedGame.ID));
            HostGameCommand = new Command(() => base.GameService.HostGame());
            BotGameCommand = new Command(() => base.GameService.JoinBotGame());
        }

        public event PropertyChangedEventHandler PropertyChanged;
       
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GameInformation SelectedGame {
            get {
                return _data;
            }
            set {
                _data = value;
                OnPropertyChanged(nameof(SelectedGame));
                JoinGameCommand.RaiseCanExecuteChanged(); } }
        
        public bool GameSelected { get { return SelectedGame != null; } }

    }
}
