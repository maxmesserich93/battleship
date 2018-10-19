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
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {

        public GameViewModel ViewModel { set; get; }
        public FieldPage PlayerField { set; get; }
        public FieldPage OpponentField { set; get; }

        public GameWindow(GameViewModel gameViewModel) : base()
        {
            ViewModel = gameViewModel;
            InitializeComponent();
            PlayerField = new FieldPage(ViewModel.PlayerFieldVM, 20);
            PlayerField.InitializeComponent();
            PlayerField.IsEnabled = false;
            OpponentField = new FieldPage(ViewModel.OpponentFieldVM, 20);
            OpponentField.InitializeComponent();

            OpponentFieldFrame.Navigate(OpponentField);
            PlayerFieldFrame.Navigate(PlayerField);
            ViewModel.GameService.Callback.GameOverHandler = OnGameOver;



        }
        public void OnGameOver(string winner)
        {

            Application.Current.Dispatcher.Invoke(() =>
            {
                var winnerScreen = new GameOverScreen(false, winner);
                winnerScreen.ShowDialog();
                base.Close();
            });

            

        }


        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
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

    }
}
