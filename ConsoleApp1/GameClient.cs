//using Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.ServiceModel;
//using System.Text;
//using System.Threading.Tasks;

//namespace ConsoleApp1
//{
//    /// <summary>
//    /// T defines the type of the SubContract.
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    
//    public class GameClient<T> : ServiceReference1.IGameContractCallback where T : ServiceReference1.IGameContractCallback
//    {
//        public InstanceContext Context { set; get; }
//        public ServiceReference1.GameContractClient Server { set; get; }
//        public T SubContract { set; get; }


//        public String Name { get; set; }
//        public bool Complete { get; set; }
//        public GameClient(string name, string addressString)
//        {
//            Name = name;
//            Complete = false;
//            //try
//            //{
//                ///instanceContext needs a reference to the server
//                Context = new InstanceContext(this);

//                Server = new ServiceReference1.GameContractClient(Context);
//                //server.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(0.5);
//                //EndpointAddress address = new EndpointAddress("http://localhost:8000/Service/Service");
//                //server.Endpoint.Address = address;



//            //}
//            //catch (Exception ex)
//            //{
//            //    Console.WriteLine(ex);
//            //}
//            Console.WriteLine(Server.Endpoint.ToString());

//        }
//        public void Initialize()
//        {
//               Server.Login(Name);
                
            
//        }

//        public void StartGame(GameRuleSet gameRuleSet)
//        {
//            //SubContract.StartGame(gameRuleSet);
//        }



//        public void Shoot()
//        {
//            SubContract.Shoot();
//        }

//        public void PlaceShips()
//        {
//            SubContract.PlaceShips();
//        }

//        public void OpponentShot(FieldPosition[] fieldPositions)
//        {
//            SubContract.OpponentShot(fieldPositions);
//        }

//        public void ShotResult(FieldPosition[] fieldPositions)
//        {
//            SubContract.ShotResult(fieldPositions);
//        }

//        public void GameOver(string winner)
//        {
//            SubContract.GameOver(winner);
//        }

//        public void Error(string message)
//        {
//            SubContract.Error(message);
//        }

//        public void ProvideIdentity(string uuid)
//        {
//            SubContract.ProvideIdentity(uuid);
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
