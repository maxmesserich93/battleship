using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;

namespace Models
{


    [DataContract]
    public enum FieldPositionStatus
    {
        [EnumMember]
        Default,
        [EnumMember]
        Ship,
        [EnumMember]
        ShotMiss,
        [EnumMember]
        ShotHit,
        [EnumMember]
        ShotKill
        
    }
    [DataContract]
    public class FieldPosition : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The coordinate of this FieldPosition.
        /// </summary>
        private Coordinate _coordinate;
        [DataMember]
        public Coordinate Coordinate
        {
            get
            {
                return _coordinate;
            }
            set
            {
                _coordinate = value;
            }
        }
        /// <summary>
        /// Status of this FieldPosition.
        /// </summary>
        private FieldPositionStatus _status;
        [DataMember]
        public FieldPositionStatus FieldPositionStatus
        {
            set
            {
                _status = value;
                OnPropertyChanged(nameof(FieldPositionStatus));

            }
            get
            {
                return _status;
            }
        }
        private bool _hover = false;
        public bool Hover
        {
            set
            {
                _hover = value;
                OnPropertyChanged(nameof(Hover));
     
            }
            get
            {
                return _hover;
            }
        }



        /// <summary>
        /// Constructor for the FieldPosition. Initializes the status and the coordinate.
        /// </summary>
        /// <param name="status"></param>
        /// <param name="coordinate"></param>
        public FieldPosition(FieldPositionStatus status, Coordinate coordinate) {

            _status = status;
            _coordinate = coordinate;
        }


        /// <summary>
        /// Invoke the listeners of the FieldPosition upon changes to the fields status.
        /// </summary>
        /// <param name="name"></param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public override string ToString()
        {
            return Coordinate.ToString() + " : " + FieldPositionStatus.ToString();
        }
    }
}
