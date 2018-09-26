using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BattleShip
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            Fields = new ObservableCollection<PlayingFieldElement>();
            for (int i = 0; i < 100; i++)
            {
                Fields.Add( new PlayingFieldElement() { XPos = i % 10, YPos = i / 10});
            }

            TileClick = new RelayCommand(TileClickHandler);
        }

        public ObservableCollection<PlayingFieldElement> Fields { get; set; }

        public ICommand TileClick { get; set; }

        public void TileClickHandler(object source)
        {
            PlayingFieldElement fieldClicked = (PlayingFieldElement)source;

            Debug.WriteLine("Button Clicked");
            Debug.WriteLine("XPosition is " + fieldClicked.XPos);
            Debug.WriteLine("YPosition is " + fieldClicked.YPos);

            fieldClicked.Status = FieldStatus.SHIP;
            
        }

    }
}
