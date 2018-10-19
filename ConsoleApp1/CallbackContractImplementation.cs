//using ConsoleApp1.ServiceReference1;
//using Models;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Runtime.Serialization;
//using System.ServiceModel;
//using System.Text;
//using System.Threading.Tasks;

//namespace ConsoleApp1
//{
//    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
//    public class CallbackContractImplementation : IGameContractCallback
//    {
//        public string id { set; get; }
//        public InstanceContext Context { set; get; }
//        public GameContractClient Server { set; get; }
//        public String Name { get; set; }
//        public bool Complete { get; set; }
//        public CallbackContractImplementation(string name)
//        {
//            Name = name;
//            Complete = false;
//            try
//            {
//                ///instanceContext needs a reference to the server
//                Context = new InstanceContext(this);

//                Server = new ServiceReference1.GameContractClient(Context);
//                Server.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(4.5);
//                EndpointAddress address = new EndpointAddress("http://localhost:8000/Service/Service");
//                Server.Endpoint.Address = address;



//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex);
//            }
//            Console.WriteLine("Joining!");

//        }

//        List<Coordinate> _shot = new List<Coordinate>();
//        private GameRuleSet _rules;
//        public void Error(string message)
//        {
//            throw new NotImplementedException();
//        }

//        public void GameOver(string winner)
//        {
//            Console.WriteLine("Game over. Winnner: " + winner);
//            Complete = true;
//        }

//        public void OpponentShot(Coordinate coordinate, List<FieldPosition> fieldPositions)
//        {
//            throw new NotImplementedException();
//        }

//        public Ship[] RequestShipPlacement()
//        {
//            try
//            {
//                Console.WriteLine("PLACE SHIPS!");
//                var legalPlacement = new List<Ship>();
//                legalPlacement.Add(new Carrier(new Coordinate(0, 0), true));

//                legalPlacement.Add(new Cruiser(new Coordinate(1, 0), true));

//                legalPlacement.Add(new BattleShip(new Coordinate(2, 0), true));

//                legalPlacement.Add(new Destroyer(new Coordinate(3, 0), true));
//                legalPlacement.Add(new Destroyer(new Coordinate(4, 0), true));

//                legalPlacement.Add(new Submarine(new Coordinate(5, 0), true));
//                legalPlacement.Add(new Submarine(new Coordinate(6, 0), true));
//                return legalPlacement.ToArray();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("An exception occurred: {0}", ex.Message);

//                Console.ReadLine();
//                return null;
//            }
//        }
//        Random random = new Random();
//        public Coordinate RequestShotPlacement()
//        {

//            Coordinate c = new Coordinate(random.Next(0, _rules.FieldSize), random.Next(0, _rules.FieldSize));
//            if (_shot.Contains(c))
//            {
//                return RequestShotPlacement();
//            }
//            _shot.Add(c);
//            return c;

//        }

//        public void ShotResult(Coordinate coordinate, FieldPosition[] fieldPositions)
//        {
//            Console.WriteLine("ShotResult:");
//            fieldPositions.ToList().ForEach(a => Console.WriteLine(a));
//        }

//        public void StartGame(GameRuleSet gameRuleSet)
//        {
//            Console.WriteLine("Joined a game with rules: " + gameRuleSet);
//            _rules = gameRuleSet;

//            var a = RequestShipPlacement();


//        }

//        public void Shoot()
//        {
//            Task.Factory.StartNew(() =>
//            {
//                Server.ProvideShotPlacement(Name, RequestShotPlacement());
//            });

//        }

//        public void PlaceShips()
//        {

//            var a = RequestShipPlacement();
//            Task.Factory.StartNew(() =>
//            {
//                Server.ProvideShipPlacements(Name, a);
//            });
//        }

//        public void OpponentShot(FieldPosition[] fieldPositions)
//        {
//            Console.WriteLine("Opponent Shot: " + fieldPositions);
//        }

//        public void ProvideIdentity(string uuid)
//        {
//            Console.WriteLine("ID: " + uuid);
//            id = uuid;
//        }

//        public void GameRules(GameRuleSet gameRuleSet)
//        {
//            throw new NotImplementedException();
//        }

//        public void PlacementComplete()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
