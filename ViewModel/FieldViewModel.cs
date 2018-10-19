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

namespace ViewModel
{
    public class FieldViewModel : AbstractServiceViewModel, INotifyPropertyChanged
    {
        //public GameClient<ClientWrapper> GameClient { get; set; }
        public Field Field { set; get; }
        public GameRuleSet GameRuleSet { set; get; }
        //private List<ShipPlacement> placements;
        public ObservableCollection<FieldPosition> Fields { get; set; }
        public double FieldSize { set; get; }
        private bool _enabled = false;
        public bool Enabled { set { _enabled = value;  } get { return _enabled; } }
        public event PropertyChangedEventHandler PropertyChanged;
        public FieldViewModel(AbstractServiceViewModel vm, GameRuleSet rules) : base(vm)
        {
            GameRuleSet = rules;
            Field = new Field(rules.FieldSize);
            Fields = Field.GetData();
            Enabled = false;
            //placements = new List<ShipPlacement>();

        }

        public ICommand TileClick { get; set; }

        public ICommand TileHover { get; set; }

        public ICommand TileUnhover { get; set; }


        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public void HandleShotResult(FieldPosition[] fieldPositions)
        {
            fieldPositions.ToList().ForEach(p => Field.SetField(p.Coordinate, p.FieldPositionStatus));

        }

        public bool CanPlace(Ship placement)
        {
            return Field.ShipPlaceable(placement);
        }

        public bool PlaceShip(Ship placement)
        {

            if (Field.PlaceShip(placement))
            {
                
                return true;
            }
            return false;
        }

        public int GetShipCount(string shipType)
        {
            return Field.ShipTypeCount(shipType);
        }
    }
}

