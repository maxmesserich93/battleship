using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ViewModel;
using ViewModel.Lobby;

namespace View
{
    /// <summary>
    /// Interaction logic for Lobby.xaml
    /// </summary>
    public partial class Lobby : Window
    {
        LobbyViewModel ViewModel { get; }
        public Lobby(LobbyViewModel vm)
        {
            ViewModel = vm;
            this.DataContext = ViewModel;
            ViewModel.PropertyChanged += Change;
            InitializeComponent();
        }
        /// <summary>
        /// Get the game list from the server.
        /// </summary>
        private void RefreshGames(object sender, RoutedEventArgs e)
        {

            ViewModel.UpdateGameList();
        }




        public void Change(object sender, PropertyChangedEventArgs e)
        {
            Debug.WriteLine("CHANGE:" + sender + " , " + e.PropertyName);
            if (e.PropertyName.Equals(nameof(ShipPlacementViewModel)))
            {

                Application.Current.Dispatcher.Invoke(() =>
                {
                    ShipPlacementWindow next = new ShipPlacementWindow(ViewModel.ShipPlacementViewModel);
                    next.Show();
                    this.Close();
                });

            }
            if (e.PropertyName.Equals(nameof(GameViewModel)))
            {

                Application.Current.Dispatcher.Invoke(() =>
                {
                    var next = new GameWindow(ViewModel.GameViewModel);
                    next.Show();
                    this.Close();
                });

            }
        }

            private void JoinGame(object sender, RoutedEventArgs e)
        {

            ViewModel.Join();
            
        }

        private void BotMatch(object sender, RoutedEventArgs e)
        {
            ViewModel.JoinBot();

            //ShipPlacementWindow next = new ShipPlacementWindow(shipPlacementViewModel);

            //next.Show();
            ////next.Show(shipPlacementViewModel);
            //this.Close();

        }

        private void HostGame(object sender, RoutedEventArgs e)
        {
            ViewModel.HostGame();
        }

        protected void Close()
        {






        }



    }
}
