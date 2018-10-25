using ConsoleApp1;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModel.Service;

namespace ViewModel
{
    public class ShipPlacementViewModel : AbstractServiceViewModel, INotifyPropertyChanged
    {


        public class ShipPlacementDetails : INotifyPropertyChanged
        {
            public string ShipType {get;set;}
            public int Length { set; get; }
            public ShipPlacementDetails(string type, int length, int requiredNumber)
            {
                Length = length;
                ShipType = type;
                NumberRequired = requiredNumber;
            }

            private int _numberPlaced;

            public event PropertyChangedEventHandler PropertyChanged;




            public int NumberPlaced { get { return _numberPlaced; } set { _numberPlaced = value; NotifyPropertyChanged(); } }

            public int NumberRequired { get; set; }
            private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

        }



        public Dictionary<string, int> ShipsToPlace { set; get; }

        /// <summary>
        /// ShipPlacementDetails displayed in the Views datagrid. Notifies on Change for TwoWay-Binding.
        /// </summary>
        private ShipPlacementDetails _s;
        public ShipPlacementDetails SelectedShipType { set { _s = value; NotifyPropertyChanged(nameof(SelectedShipType)); } get { return _s; } }


        private GameRuleSet _gameRuleSet;
        public GameRuleSet GameRuleSet { set {
                _gameRuleSet = value;
                
            }
            get {
                return _gameRuleSet;
            }
        } 

        public string OpponentName { set; get; }



        public ObservableCollection<ShipPlacementDetails> ShipPlacements { set; get; }

        private List<Coordinate> _highlighted = new List<Coordinate>();
        public FieldViewModel FieldViewModel { set; get; }
        List<Ship> _placedShips = new List<Ship>();


        /// <summary>
        /// Boolean for vertical and horizontal placement
        /// </summary>
        private bool _verticalPlacement;
        public bool VerticalPlacement {
            set {
                _verticalPlacement = value;
                Debug.WriteLine(value);
                
                NotifyPropertyChanged(nameof(VerticalPlacement));
                if (_verticalPlacement)
                {
                    ButtonText = "Vertical";
                }
                else
                {
                    ButtonText = "Horizontal";
                }

            }
            get { return _verticalPlacement; }
        }
        /// <summary>
        /// Text for vertical and horizontal for the the togglebutton
        /// </summary>
        private string _buttonText = "Vertical";
        public string ButtonText { set { _buttonText = value; NotifyPropertyChanged(nameof(ButtonText)); } get { return _buttonText; } }



        private bool _placementValid;
        public bool PlacementValid { set { _placementValid = value; AcceptPlacementCommand.RaiseCanExecuteChanged(); } get { return _placementValid; } }





        public event PropertyChangedEventHandler PropertyChanged;



        private Command _onShipSelection;
        public Command OnShipTypeSelectionCommand { get { return _onShipSelection; } }


        public Command AcceptPlacementCommand { get; }




        public ShipPlacementViewModel(AbstractGameServiceViewModel gameService, GameRuleSet rules) : base(gameService)
        {

            //base.GameService.Callback.GameHandler = this.Initialize;
            GameRuleSet = rules;

            FieldViewModel = new FieldViewModel(this, rules);

            //Creates ShipPlacementDetails for each ShipType to be placed.


            ShipPlacements = new ObservableCollection<ShipPlacementDetails>(rules.ShipTypeRules.ToList().Select(keyValue => new ShipPlacementDetails(keyValue.Key, ShipFactory.CREATE_SHIP(keyValue.Key).Length, keyValue.Value)).ToList());
            SelectedShipType = ShipPlacements.First();

            VerticalPlacement = true;

            FieldViewModel.TileClick = new TypedCommand<FieldPosition>((field) => Click(field));
            FieldViewModel.TileHover = new TypedCommand<FieldPosition>((field) => Hover(field));

            AcceptPlacementCommand = new Command(() => PlacementValid, () => AcceptPlacement());

            _onShipSelection = new Command(() =>  UpdateSelection());



        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        //public void Initialize(GameRuleSet gameRuleSet, string opponentName)
        //{
        //    Debug.WriteLine("ShipPlacementVM received gameRules:" + gameRuleSet);

        //    GameRuleSet = gameRuleSet;
        //    OpponentName = opponentName;



        //}


        /// <summary>
        /// Disables the selection of shipstype if the required number of ships of that type have been placed.
        ///
        /// </summary>
        public void UpdateSelection()
        {
            //Get all shiptypes which have the required amount of ships placed.

            Debug.WriteLine("UPDATING SELECTION");
    
            if(_placedShips.Count == 0 || SelectedShipType == null)
            {
                return;
            }

            var ShipTypesToDisable = GameRuleSet.ShipTypeRules.ToList()
                .Where(kp => _placedShips.Count(ship => ship.Type.Equals(kp.Key)) == kp.Value)
                .Select(a => a.Key).ToList();



            var ShipTypesToEnable = GameRuleSet.ShipTypeRules.ToList()
                .Where(kp => _placedShips.Count(ship => ship.Type.Equals(kp.Key)) < kp.Value)
                .Select(a => a.Key).ToList();


            if (ShipTypesToDisable.Contains(SelectedShipType.ShipType))
            {
                //If complte done
                if (ShipTypesToDisable.Count().Equals(GameRuleSet.ShipTypeRules.Count())){
                    //Enable the accept button 
                    PlacementValid = true;
                    SelectedShipType = null;
                }
                else
                {
                    //ShipPlacements.GetEnumerator().MoveNext();
                    SelectedShipType = ShipPlacements.First(a => a.ShipType == ShipTypesToEnable.First());
                    Debug.WriteLine("SETTING SELECTION TO: " + SelectedShipType.ShipType);
                }
            }
        }

        private void Click(FieldPosition position)
        {
            if (SelectedShipType == null) { return; }
            Debug.WriteLine(" ShipPlacementViewModel Click: " + position);
            
            var placement = ShipFactory.CREATE_SHIP(SelectedShipType.ShipType, position.Coordinate, VerticalPlacement);



            if (!GameRuleSet.CanPlaceShip(FieldViewModel.Field, placement)){
                return;
            }

            var canPlace = FieldViewModel.Field.PlaceShip(placement);

            if(canPlace)
            {
                SelectedShipType.NumberPlaced += 1;
                Debug.WriteLine(SelectedShipType.ShipType + " : " + SelectedShipType.NumberPlaced);
                _placedShips.Add(placement);

                _placedShips.ForEach(p => Debug.WriteLine(" PLACED: " + p));
            }
            

            UpdateSelection();
            Unhover();

        }
        
        

        /// <summary>
        /// Highlights the position of a ship if it is placeable
        /// </summary>
        /// <param name="position"></param>
        private void Hover(FieldPosition position)
        {
            if (SelectedShipType == null || position == null) { return; }

            _highlighted.ForEach(p => FieldViewModel.UnhighlightCoordinate(p));
            var ship = ShipFactory.CREATE_SHIP(SelectedShipType.ShipType, position.Coordinate, VerticalPlacement);

            if (FieldViewModel.Field.ShipPlaceable(ship))
            {
                _highlighted = ship.GetCoordinates();
                _highlighted.ForEach(p => FieldViewModel.HighlightCoordinate(p));

            }

        }

        private void Unhover()
        {
            _highlighted.ForEach(p => FieldViewModel.UnhighlightCoordinate(p));
            _highlighted.Clear();
        }
         
        /// <summary>
        /// Checks whether all ship have been placed.
        /// </summary>
        /// <returns></returns>
        public bool ValidatePlacement()
        {
            return GameRuleUtils.ValidateRules(GameRuleSet, _placedShips);
            //return ShipsToPlace!=null && ShipsToPlace.All(typeCount => _shipPlacements.Select(s => s.ShipType).Count() == typeCount.Value);

        }
        
        public bool AcceptPlacement()
        {
         
            if (PlacementValid)
            {
                base.GameService.ProvideShipPlacements(_placedShips);
                return true;
            }
            return false;
        }


    }
}
