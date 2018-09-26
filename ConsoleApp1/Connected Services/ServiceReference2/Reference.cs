﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleApp1.ServiceReference2 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://fontysvenlo.org/BattleService", ConfigurationName="ServiceReference2.IGameContract", CallbackContract=typeof(ConsoleApp1.ServiceReference2.IGameContractCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IGameContract {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://fontysvenlo.org/BattleService/IGameContract/Login", ReplyAction="http://fontysvenlo.org/BattleService/IGameContract/LoginResponse")]
        bool Login(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://fontysvenlo.org/BattleService/IGameContract/Login", ReplyAction="http://fontysvenlo.org/BattleService/IGameContract/LoginResponse")]
        System.Threading.Tasks.Task<bool> LoginAsync(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://fontysvenlo.org/BattleService/IGameContract/GetAvailableGames", ReplyAction="http://fontysvenlo.org/BattleService/IGameContract/GetAvailableGamesResponse")]
        Models.GameData[] GetAvailableGames();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://fontysvenlo.org/BattleService/IGameContract/GetAvailableGames", ReplyAction="http://fontysvenlo.org/BattleService/IGameContract/GetAvailableGamesResponse")]
        System.Threading.Tasks.Task<Models.GameData[]> GetAvailableGamesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://fontysvenlo.org/BattleService/IGameContract/JoinGame", ReplyAction="http://fontysvenlo.org/BattleService/IGameContract/JoinGameResponse")]
        void JoinGame(string playerName, string gameId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://fontysvenlo.org/BattleService/IGameContract/JoinGame", ReplyAction="http://fontysvenlo.org/BattleService/IGameContract/JoinGameResponse")]
        System.Threading.Tasks.Task JoinGameAsync(string playerName, string gameId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://fontysvenlo.org/BattleService/IGameContract/HostGame", ReplyAction="http://fontysvenlo.org/BattleService/IGameContract/HostGameResponse")]
        void HostGame(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://fontysvenlo.org/BattleService/IGameContract/HostGame", ReplyAction="http://fontysvenlo.org/BattleService/IGameContract/HostGameResponse")]
        System.Threading.Tasks.Task HostGameAsync(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://fontysvenlo.org/BattleService/IGameContract/ProvideShipPlacements", ReplyAction="http://fontysvenlo.org/BattleService/IGameContract/ProvideShipPlacementsResponse")]
        void ProvideShipPlacements(string player, Models.ShipPlacement[] shipPlacements);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://fontysvenlo.org/BattleService/IGameContract/ProvideShipPlacements", ReplyAction="http://fontysvenlo.org/BattleService/IGameContract/ProvideShipPlacementsResponse")]
        System.Threading.Tasks.Task ProvideShipPlacementsAsync(string player, Models.ShipPlacement[] shipPlacements);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://fontysvenlo.org/BattleService/IGameContract/ProvideShotPlacement", ReplyAction="http://fontysvenlo.org/BattleService/IGameContract/ProvideShotPlacementResponse")]
        void ProvideShotPlacement(string player, Models.Coordinate coordinate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://fontysvenlo.org/BattleService/IGameContract/ProvideShotPlacement", ReplyAction="http://fontysvenlo.org/BattleService/IGameContract/ProvideShotPlacementResponse")]
        System.Threading.Tasks.Task ProvideShotPlacementAsync(string player, Models.Coordinate coordinate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://fontysvenlo.org/BattleService/IGameContract/JoinBotGame", ReplyAction="http://fontysvenlo.org/BattleService/IGameContract/JoinBotGameResponse")]
        void JoinBotGame(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://fontysvenlo.org/BattleService/IGameContract/JoinBotGame", ReplyAction="http://fontysvenlo.org/BattleService/IGameContract/JoinBotGameResponse")]
        System.Threading.Tasks.Task JoinBotGameAsync(string name);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGameContractCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://fontysvenlo.org/BattleService/IGameContract/StartGame")]
        void StartGame(Models.GameRuleSet gameRuleSet);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://fontysvenlo.org/BattleService/IGameContract/RequestShipPlacement", ReplyAction="http://fontysvenlo.org/BattleService/IGameContract/RequestShipPlacementResponse")]
        Models.ShipPlacement[] RequestShipPlacement();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://fontysvenlo.org/BattleService/IGameContract/Turd", ReplyAction="http://fontysvenlo.org/BattleService/IGameContract/TurdResponse")]
        string[] Turd();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://fontysvenlo.org/BattleService/IGameContract/RequestShotPlacement", ReplyAction="http://fontysvenlo.org/BattleService/IGameContract/RequestShotPlacementResponse")]
        Models.Coordinate RequestShotPlacement();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://fontysvenlo.org/BattleService/IGameContract/Shoot")]
        void Shoot();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://fontysvenlo.org/BattleService/IGameContract/PlaceShips")]
        void PlaceShips();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://fontysvenlo.org/BattleService/IGameContract/OpponentShot")]
        void OpponentShot(Models.FieldPosition[] fieldPositions);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://fontysvenlo.org/BattleService/IGameContract/ShotResult")]
        void ShotResult(Models.FieldPosition[] fieldPositions);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://fontysvenlo.org/BattleService/IGameContract/GameOver")]
        void GameOver(string winner);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://fontysvenlo.org/BattleService/IGameContract/Error")]
        void Error(string message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGameContractChannel : ConsoleApp1.ServiceReference2.IGameContract, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GameContractClient : System.ServiceModel.DuplexClientBase<ConsoleApp1.ServiceReference2.IGameContract>, ConsoleApp1.ServiceReference2.IGameContract {
        
        public GameContractClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public GameContractClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public GameContractClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public GameContractClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public GameContractClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public bool Login(string userName) {
            return base.Channel.Login(userName);
        }
        
        public System.Threading.Tasks.Task<bool> LoginAsync(string userName) {
            return base.Channel.LoginAsync(userName);
        }
        
        public Models.GameData[] GetAvailableGames() {
            return base.Channel.GetAvailableGames();
        }
        
        public System.Threading.Tasks.Task<Models.GameData[]> GetAvailableGamesAsync() {
            return base.Channel.GetAvailableGamesAsync();
        }
        
        public void JoinGame(string playerName, string gameId) {
            base.Channel.JoinGame(playerName, gameId);
        }
        
        public System.Threading.Tasks.Task JoinGameAsync(string playerName, string gameId) {
            return base.Channel.JoinGameAsync(playerName, gameId);
        }
        
        public void HostGame(string name) {
            base.Channel.HostGame(name);
        }
        
        public System.Threading.Tasks.Task HostGameAsync(string name) {
            return base.Channel.HostGameAsync(name);
        }
        
        public void ProvideShipPlacements(string player, Models.ShipPlacement[] shipPlacements) {
            base.Channel.ProvideShipPlacements(player, shipPlacements);
        }
        
        public System.Threading.Tasks.Task ProvideShipPlacementsAsync(string player, Models.ShipPlacement[] shipPlacements) {
            return base.Channel.ProvideShipPlacementsAsync(player, shipPlacements);
        }
        
        public void ProvideShotPlacement(string player, Models.Coordinate coordinate) {
            base.Channel.ProvideShotPlacement(player, coordinate);
        }
        
        public System.Threading.Tasks.Task ProvideShotPlacementAsync(string player, Models.Coordinate coordinate) {
            return base.Channel.ProvideShotPlacementAsync(player, coordinate);
        }
        
        public void JoinBotGame(string name) {
            base.Channel.JoinBotGame(name);
        }
        
        public System.Threading.Tasks.Task JoinBotGameAsync(string name) {
            return base.Channel.JoinBotGameAsync(name);
        }
    }
}
