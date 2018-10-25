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

namespace ViewModel.field
{
    public class FieldViewModel : AbstractServiceViewModel, INotifyPropertyChanged
    {



        public Field Field { set; get; }
        public GameRuleSet GameRuleSet { set; get; }
        public ObservableCollection<CoordinateViewModel> CoordinateViewModels { get; set; }



        public double FieldSize { set; get; }
        private bool _enabled = false;
        public bool Enabled { set { _enabled = value;  } get { return _enabled; } }
        public event PropertyChangedEventHandler PropertyChanged;


        public FieldViewModel(AbstractServiceViewModel vm, GameRuleSet rules) : base(vm)
        {
            GameRuleSet = rules;
            Field = new Field(rules.FieldSize);


            CoordinateViewModels = new ObservableCollection<CoordinateViewModel>(Field.GetData().Select(position => new CoordinateViewModel(position, null, null,null)));
            Enabled = false;
            //placements = new List<ShipPlacement>();

        }


        public FieldViewModel(Action<FieldPosition> click, Action<FieldPosition> hover, Action<FieldPosition> unhover, AbstractServiceViewModel vm, GameRuleSet rules) : base(vm)
        {
            GameRuleSet = rules;
            Field = new Field(rules.FieldSize);


            CoordinateViewModels = new ObservableCollection<CoordinateViewModel>(Field.GetData().Select(position => new CoordinateViewModel(position, click, hover,unhover)));
            Enabled = false;
            //placements = new List<ShipPlacement>();

        }




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

        public void HighlightCoordinate(Coordinate coordinate)
        {
            CoordinateViewModels.Where(coordinateVm => coordinateVm.Position.Coordinate.Equals(coordinate)).Single().Hover = true;
        }

        public void UnhighlightCoordinate(Coordinate coordinate)
        {
            CoordinateViewModels.Where(coordinateVm => coordinateVm.Position.Coordinate.Equals(coordinate)).Single().Hover = false;
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

