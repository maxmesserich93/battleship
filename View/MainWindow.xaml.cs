
using ConsoleApp1;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using ViewModel;
using ViewModel.Lobby;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ViewModel.LoginViewModel ViewModel { get; }
       
       //public MasterViewModel Master { get; }


        public MainWindow()
        {
            Trace.WriteLine("DASDAS");
            ViewModel = new LoginViewModel();
            this.DataContext = ViewModel;
            ViewModel.PropertyChanged += Change;
            InitializeComponent();

        }


        //public void Change(object sender, PropertyChangedEventArgs e)
        //{
        //    Debug.WriteLine("CHANGE:" + sender + " , " + e.PropertyName);
        //    if(e.PropertyName.Equals(nameof()))

        //}

        public void Change(object sender, PropertyChangedEventArgs e)
        {
            Debug.WriteLine("CHANGE:" + sender + " , " + e.PropertyName);
            Application.Current.Dispatcher.Invoke(() =>
            {
                var next = new MasterWindow(ViewModel.MasterViewModel);

                next.Show();
                this.Close();
            });



            //if (e.PropertyName.Equals(nameof(ShipPlacementViewModel)))
            //{


            //    ShipPlacementWindow next = new ShipPlacementWindow(ViewModel.ShipPlacementViewModel);
            //    next.Show();
            //    this.Close();
            //}
            //if (e.PropertyName.Equals(nameof(LobbyViewModel)))
            //{
            //    Application.Current.Dispatcher.Invoke(() =>
            //    {
            //        var next = new Lobby(ViewModel.LobbyViewModel);
            //        next.Show();
            //        this.Close();
            //    });

            //}

        }




        private void ConnectToServer(object sender, RoutedEventArgs e)
        {

            ViewModel.Connect();




        }
        private void ConnectToLocalServer(object sender, RoutedEventArgs e)
        {

            ViewModel.LocalConnect();




        }

    }
}
