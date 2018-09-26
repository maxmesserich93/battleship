using ConsoleApp1.ServiceReference2;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// T defines the type of the SubContract.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    
    public class GameClient<T> : IGameContractCallback where T : IGameContractCallback
    {
        public InstanceContext Context { set; get; }
        public GameContractClient Server { set; get; }
        public T SubContract { set; get; }


        public String Name { get; set; }
        public bool Complete { get; set; }
        public GameClient(string name, string addressString)
        {
            Name = name;
            Complete = false;
            try
            {
                ///instanceContext needs a reference to the server
                Context = new InstanceContext(this);

                Server = new ServiceReference2.GameContractClient(Context);
                Server.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(0.5);
                EndpointAddress address = new EndpointAddress(addressString);
                Server.Endpoint.Address = address;



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            

        }
        public bool Initialize()
        {
            try
            {
                return Server.Login(Name);
                
            }
            catch
            {
                Console.WriteLine("INIT FAILED!");
                return false;
            }
            
        }

        public void StartGame(GameRuleSet gameRuleSet)
        {
            SubContract.StartGame(gameRuleSet);
        }

        public ShipPlacement[] RequestShipPlacement()
        {
            throw new NotImplementedException();
        }

        public string[] Turd()
        {
            throw new NotImplementedException();
        }

        public Coordinate RequestShotPlacement()
        {
            throw new NotImplementedException();
        }

        public void Shoot()
        {
            SubContract.Shoot();
        }

        public void PlaceShips()
        {
            SubContract.PlaceShips();
        }

        public void OpponentShot(FieldPosition[] fieldPositions)
        {
            SubContract.OpponentShot(fieldPositions);
        }

        public void ShotResult(FieldPosition[] fieldPositions)
        {
            SubContract.ShotResult(fieldPositions);
        }

        public void GameOver(string winner)
        {
            SubContract.GameOver(winner);
        }

        public void Error(string message)
        {
            SubContract.Error(message);
        }
    }
}
