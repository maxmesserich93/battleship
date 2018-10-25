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
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {

        public GameViewModel ViewModel { set; get; }
        public FieldPage PlayerField { set; get; }
        public FieldPage OpponentField { set; get; }

        public GamePage(GameViewModel gameViewModel) : base()
        {
            ViewModel = gameViewModel;
            DataContext = ViewModel;

            InitializeComponent();


            PlayerField = new FieldPage(ViewModel.PlayerFieldVM, 20);

            OpponentField = new FieldPage(ViewModel.OpponentFieldVM, 20);


            OpponentFieldFrame.Navigate(OpponentField);
            PlayerFieldFrame.Navigate(PlayerField);



        }


    }
}
