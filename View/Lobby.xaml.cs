using System;
using System.Collections.Generic;
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
            InitializeComponent();
        }
        /// <summary>
        /// Get the game list from the server.
        /// </summary>
        private void RefreshGames(object sender, RoutedEventArgs e)
        {

            ViewModel.UpdateGameList();
        }

        private void JoinGame(object sender, RoutedEventArgs e)
        {
            
        }

        private void BotMatch(object sender, RoutedEventArgs e)
        {
            ShipPlacementViewModel shipPlacementViewModel = ViewModel.JoinBot();

            ShipPlacementWindow next = new ShipPlacementWindow(shipPlacementViewModel);

            next.Show();
            //next.Show(shipPlacementViewModel);
            this.Close();

        }





    }
}
