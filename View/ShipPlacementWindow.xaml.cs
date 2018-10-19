using Models;
using System;
using System.Collections.Generic;
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

namespace View
{
    /// <summary>
    /// Interaction logic for ShipPlacementWindow.xaml
    /// </summary>
    public partial class ShipPlacementWindow : Window
    {
        ShipPlacementViewModel ViewModel { get; }

        public FieldPage Frame { set; get; }


        


        public ShipPlacementWindow(ShipPlacementViewModel model)
        {
            ViewModel = model;
            this.DataContext = ViewModel;

            InitializeComponent();
            Frame = new FieldPage(ViewModel.FieldViewModel,20);
            Frame.InitializeComponent();
            //Frame.Resources.Add("FieldSize", 50.0);

            Field.Navigate(Frame);
            

        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.AcceptPlacement())
            {
                var gameModel = new GameViewModel(ViewModel.FieldViewModel,ViewModel);
                GameWindow next = new GameWindow(gameModel);

                next.Show();
                this.Hide();
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Debug.WriteLine(e);


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
