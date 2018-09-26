using ConsoleApp1;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class FieldViewModel : AbstractServiceViewModel
    {
        //public GameClient<ClientWrapper> GameClient { get; set; }
        public Field Field { set; get; }
        public GameRuleSet GameRuleSet { set; get; }
        private List<ShipPlacement> placements;
        public ObservableCollection<FieldPosition> Fields { get; set; }
        public double FieldSize { set; get; }


        public FieldViewModel(AbstractServiceViewModel vm, GameRuleSet rules) : base(vm)
        {
            Field = new Field(rules.FieldSize);
            Fields = Field.GetData();

        }



        public ICommand TileClick { get; set; }

        public ICommand TileHover { get; set; }

        public ICommand TileUnhover { get; set; }




        public void TileClickHandler(object source)
        {
            //FieldPosition fieldPosition = (FieldPosition)source;
            //Debug.WriteLine("Click: " + fieldPosition.Coordinate);
            Debug.WriteLine("CLICK");
        }

        private void OnFieldClick(FieldPosition sneder)
        {
             Debug.WriteLine("CLICK");
        } 



        public void HandleShotResult(FieldPosition[] fieldPositions)
        {
            fieldPositions.ToList().ForEach(p => Field.SetField(p.Coordinate, p.FieldPositionStatus));

        }

        public bool CanPlace(ShipPlacement placement)
        {
            return Field.ShipPlaceable(new Ship(placement));
        }

        public bool PlaceShip(ShipPlacement placement)
        {

            if (Field.PlaceShip(placement) != null)
            {
                placements.Add(placement);
                return true;
            }
            return false;
        }
    }
}

