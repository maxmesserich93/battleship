using ConsoleApp1.ServiceReference2;
using DieService;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class CallbackContractImplementation : IGameContractCallback
    {
        public InstanceContext Context { set; get; }
        public GameContractClient Server { set; get; }
        public String Name { get; set; }
        public bool Complete { get; set; }
        public CallbackContractImplementation(string name)
        {
            Name = name;
            Complete = false;
            try
            {
                ///instanceContext needs a reference to the server
                Context = new InstanceContext(this);

                Server = new ServiceReference2.GameContractClient(Context);
                //server.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(0.5);
                //EndpointAddress address = new EndpointAddress("http://localhost:8000/Service/Service");
                //server.Endpoint.Address = address;



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.WriteLine("Joining!");
            Server.Login(Name);

        }

        List<Coordinate> _shot = new List<Coordinate>();
        private GameRuleSet _rules;
        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void GameOver(string winner)
        {
            Console.WriteLine("Game over. Winnner: " + winner);
            Complete = true;
        }

        public void OpponentShot(Coordinate coordinate)
        {
            throw new NotImplementedException();
        }

        public ShipPlacement[] RequestShipPlacement()
        {
            try
            {
                Console.WriteLine("PLACE SHIPS!");
                List<ShipPlacement> legalPlacement = new List<ShipPlacement>();

                legalPlacement.Add(new ShipPlacement(new Carrier(), true, new Coordinate(0, 0)));
                legalPlacement.Add(new ShipPlacement(new Cruiser(), true, new Coordinate(1, 0)));
                legalPlacement.Add(new ShipPlacement(new BattleShip(), true, new Coordinate(2, 0)));
                legalPlacement.Add(new ShipPlacement(new Destroyer(), true, new Coordinate(3, 0)));
                legalPlacement.Add(new ShipPlacement(new Destroyer(), true, new Coordinate(4, 0)));
                legalPlacement.Add(new ShipPlacement(new Submarine(), true, new Coordinate(5, 0)));
                legalPlacement.Add(new ShipPlacement(new Submarine(), true, new Coordinate(6, 0)));

                Console.WriteLine(legalPlacement);
                DataContractSerializer ser = new DataContractSerializer(typeof(ShipPlacement));


                legalPlacement.ForEach(a => Console.WriteLine(a.ToString()));

                return legalPlacement.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An exception occurred: {0}", ex.Message);

                Console.ReadLine();
                return null;
            }
        }
        Random random = new Random();
        public Coordinate RequestShotPlacement()
        {

            Coordinate c = new Coordinate(random.Next(0, _rules.FieldSize), random.Next(0, _rules.FieldSize));
            if (_shot.Contains(c))
            {
                return RequestShotPlacement();
            }
            _shot.Add(c);
            return c;

        }

        public void ShotResult(FieldPosition[] fieldPositions)
        {
            Console.WriteLine("ShotResult:");
            fieldPositions.ToList().ForEach(a => Console.WriteLine(a));
        }

        public void StartGame(GameRuleSet gameRuleSet)
        {
            Console.WriteLine("Joined a game with rules: " + gameRuleSet);
            _rules = gameRuleSet;

            var a = RequestShipPlacement();

            
        }

        public string[] Turd()
        {
            var a = new List<string>();
            a.Add("WHY IS THIS WORKING?????????????????");
            return a.ToArray();
        }

        public void Shoot()
        {
            Task.Factory.StartNew(() =>
            {
                Server.ProvideShotPlacement(Name, RequestShotPlacement());
            });

        }

        public void PlaceShips()
        {

            var a = RequestShipPlacement();
            Task.Factory.StartNew(() =>
            {
                Server.ProvideShipPlacements(Name, a);
            });
        }

        public void OpponentShot(FieldPosition[] fieldPositions)
        {
            Console.WriteLine("Opponent Shot: " + fieldPositions);
        }
    }
}
