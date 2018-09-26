using ConsoleApp1;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class ShipPlacementViewModel : AbstractServiceViewModel
    {
        public AbstractCallback Client { get; set; }
        public Field PlayerField { set; get; }

        public Dictionary<ShipType, int> ShipsToPlace { set; get; }


        private ShipType _shipType;

        public ShipType SelectedShipType { set { _shipType = value; Debug.WriteLine("Current ShipPlacement:" + _shipType); } get { return _shipType; } }
        private GameRuleSet _gameRuleSet;
        public GameRuleSet GameRuleSet { set { _gameRuleSet = value; _fieldRepresentation = new Field(_gameRuleSet.FieldSize); }
            get { return _gameRuleSet; } } 
        private Field _fieldRepresentation;

        private List<Coordinate> _highlighted = new List<Coordinate>();
        public FieldViewModel FieldViewModel { set; get; }

        List<ShipPlacement> _shipPlacements;

        public ShipPlacementViewModel(AbstractCallback abstractCallback, AbstractGameService gameService, GameRuleSet rules) : base(abstractCallback, gameService)
        {

            base.Callback.GameHandler = this.Initialize;
            GameRuleSet = rules;

            FieldViewModel = new FieldViewModel(this, rules);
            ShipsToPlace = rules.ShipTypeRules;
            //CurrentShipPlacement = new ShipPlacement(new Cruiser(), true, new Coordinate(0, 0));
            //TileClick = new RelayCommand(TileClickHandler);

            FieldViewModel.TileClick = new TypedRelayCommand<FieldPosition>(Click);
            FieldViewModel.TileHover = new TypedRelayCommand<FieldPosition>(Hover);
            Debug.WriteLine(" S H I P S ");
            ShipsToPlace.ToList().ForEach(kp => Debug.WriteLine(kp.Key + " : " + kp.Value));

        }

        public void Initialize(GameRuleSet gameRuleSet)
        {
            Debug.WriteLine("ShipPlacementVM received gameRules:"+gameRuleSet);
             
            GameRuleSet = gameRuleSet;




        }

        private void Click(FieldPosition position)
        {
            if (SelectedShipType == null) { return; }
            Debug.WriteLine(" ShipPlacementViewModel Click: " + position);


            var ship = FieldViewModel.Field.PlaceShip(new ShipPlacement(SelectedShipType,true,position.Coordinate));

            //base.GameService.ProvideShipPlacements(base.Callback.)
            Debug.WriteLine(" SHIP " + ship);


        }
        /// <summary>
        /// Highlights the position of a ship if it is placeable
        /// </summary>
        /// <param name="position"></param>
        private void Hover(FieldPosition position)
        {
            if (SelectedShipType == null) { return; }

            //Debug.WriteLine("HOVER: " + position);
            _highlighted.ForEach(p => FieldViewModel.Field.Hover(p, false));

            var ship = new Ship(new ShipPlacement(SelectedShipType, true, position.Coordinate));
            if(FieldViewModel.Field.ShipPlaceable(ship))
            {
                _highlighted = ship.GetCoordinates();
                _highlighted.ForEach(p => FieldViewModel.Field.Hover(p, true));
                //FieldViewModel.Field.SetField()
            }

        }

        private void Unhover(FieldPosition position)
        {

        }
         
        /// <summary>
        /// Checks whether all ship have been placed.
        /// </summary>
        /// <returns></returns>
        public bool ValidatePlacement()
        {

            return ShipsToPlace!=null && ShipsToPlace.All(typeCount => _shipPlacements.Select(s => s.ShipType).Count() == typeCount.Value);

        }
        public void StartGame()
        {

        }

        public ICommand TileClick { get; set; }

        public void TileClickHandler(object source)
        {
            Debug.WriteLine("TURD");


        }

        private bool ShipTypeEnabled(ShipType shipType)
        {

            return false;
        }


    }
}
