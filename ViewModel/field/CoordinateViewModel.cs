using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel.field
{
    public class CoordinateViewModel : INotifyPropertyChanged
    {
        public ICommand ClickCommand { get; }
        public ICommand HoverCommand { get; }


        public ICommand UnhoverCommand { get; }


        public CoordinateViewModel(FieldPosition position, Action<FieldPosition> clickAction, Action<FieldPosition> hoverCommand, Action<FieldPosition> unhoverAction)
        {
            Position = position;
            ClickCommand = new Command(() => Position.FieldPositionStatus == FieldPositionStatus.Default, () => clickAction?.Invoke(Position));

            HoverCommand = new Command(() => Position.FieldPositionStatus == FieldPositionStatus.Default, () => hoverCommand?.Invoke(Position));

            UnhoverCommand = new Command(() => unhoverAction?.Invoke(Position));

            Position.PropertyChanged += FieldStatusChange;
        }
        /// <summary>
        /// Unhovers the button when the status of the position changes.
        /// This removes the highlight effect when the player shoots a coordinate or places a ship upon it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FieldStatusChange(object sender, PropertyChangedEventArgs e)
        {

            if (e.PropertyName.Equals(nameof(Position.FieldPositionStatus)))
            {
                Hover = false;
            }

        }


        public FieldPosition Position { set; get; }

        private bool _hover = false;
        public bool Hover
        {
            set { _hover = value; NotifyPropertyChanged(nameof(Hover)); }
            get { return _hover; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
