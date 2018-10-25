using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [DataContract]
    public class GameInformation
    {
        [DataMember]
        public string Owner { get; set; }
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public DateTime OpenDate { get; set; }
        public override string ToString()
        {
            return ID + " : " + Owner;
        }
    }
}
