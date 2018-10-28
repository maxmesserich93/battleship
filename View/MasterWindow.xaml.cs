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
    /// Interaction logic for MasterWindow.xaml
    /// </summary>
    public partial class MasterWindow : Window
    {
        public MasterViewModel ViewModel { set; get; }
        public Page CurrentPage { set; get; }
        public MasterWindow(MasterViewModel vm)
        {
            this.ViewModel = vm;
            this.DataContext = ViewModel;
            ViewModel.PropertyChanged += Change;
            InitializeComponent();
            //Content.Navigate()
            var lobbyVm = ViewModel.CurrentViewModel as LobbyViewModel;
            Frame.Navigate(new Lobby(lobbyVm));


        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {

            if (MessageBox.Show("Lobby: Are you sure you want to quit?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                e.Cancel = true;
                base.OnClosing(e);
            }
            else
            {
                ViewModel.GameService.Close();
                Application.Current.Shutdown();
            }
        }

        public void Change(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(ViewModel.CurrentViewModel))){
                Debug.WriteLine(" NEW VIEW MODEL " + ViewModel.CurrentViewModel);

                Application.Current.Dispatcher.Invoke(() =>
                {
                        Page page = null;
                    if(ViewModel.CurrentViewModel is LobbyViewModel)
                    {
                        var lobbyVm = ViewModel.CurrentViewModel as LobbyViewModel;
                        page = new Lobby(lobbyVm);
 
                    }
                    if (ViewModel.CurrentViewModel is ShipPlacementViewModel)
                    {
                        var placementVm = ViewModel.CurrentViewModel as ShipPlacementViewModel;
                        page = new ShipPlacementWindow(placementVm);
                    }
                    if (ViewModel.CurrentViewModel is GameViewModel)
                    {

                        var gameVm = ViewModel.CurrentViewModel as GameViewModel;
                        page = new GamePage(gameVm);
                    }
                    if (ViewModel.CurrentViewModel is GameOverViewModel)
                    {

                        var gameOverVm = ViewModel.CurrentViewModel as GameOverViewModel;
                        page = new GameOverPage(gameOverVm);
                    }

                    Frame.Navigate(page);
                });
            }

        }


    }
}
