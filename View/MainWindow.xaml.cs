
using ConsoleApp1;
using Models;
using System;
using System.Collections.Generic;
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
using View.ServiceReference2;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ViewModel.LoginViewModel ViewModel { get; }
       
       


        public MainWindow()
        {
            Trace.WriteLine("DASDAS");
            ViewModel = new LoginViewModel();
            this.DataContext = ViewModel;
            InitializeComponent();

        }

        private void ConnectToServer(object sender, RoutedEventArgs e)
        {
            var a = ViewModel.ServerAddressString;
            var address = "http://localhost:8000/Service/Service";
            GameClient<RemoteCallback> client = new GameClient<RemoteCallback>(ViewModel.PlayerName, address);
            if (client.Initialize())
            {
                //If the client connection is complete: Go to lobby

                var lobbyVm = new LobbyViewModel(client);
                Lobby nextWindow = new Lobby(lobbyVm);

                nextWindow.Show();
                this.Close();

            }
            else
            {
                MessageBox.Show("Could not connect to the server " + address);

                var ca = new LocalCallback();
                var b = new LocalGameServiceVM();
                ViewModel.Callback = ca;
                ViewModel.GameService = b;
                b.Player = ca;
                

                ViewModel.Callback.GameHandler = _awaitGameRules;

                b.JoinBotGame("roman");
                //var model = new 




            }

            void _awaitGameRules(GameRuleSet gameRuleSet)
            {
                var ShipPlacementVm = new ShipPlacementViewModel(ViewModel.Callback, ViewModel.GameService, gameRuleSet);



                ShipPlacementWindow next = new ShipPlacementWindow(ShipPlacementVm);
                next.Show();
                this.Close();


            }

            //InstanceContext instanceContext;



            /////instanceContext needs a reference to the server
            //instanceContext = new InstanceContext(new CallbackContractImplementation());

            //_server = new ServiceReference2.GameContractClient(instanceContext);

            //_server.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(0.5);
            //EndpointAddress address = new EndpointAddress("http://localhost:8000/Service/Service");
            //_server.Endpoint.Address = address;
            //_server.Login("asd");






        }


        private void ConnectToServerA(object sender, RoutedEventArgs e)
        {


            //InstanceContext instanceContext;

            //ConsoleApp1.ServiceReference2.GameContractClient server = null;


            /////instanceContext needs a reference to the server
            //instanceContext = new InstanceContext(new ConsoleApp1.StubContract(null));





            //server = new ConsoleApp1.ServiceReference2.GameContractClient(instanceContext);

            //bool a  = server.Login("asd");

            //throw new ArgumentException(a.ToString());


            //server.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(0.5);
            //EndpointAddress address = new EndpointAddress("http://localhost:8000/Service/Service");
            //server.Endpoint.Address = address;


        }

    }
}
