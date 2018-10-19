using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models.Player
{
    [DataContract]
    public class PlayerData
    {
        [DataMember]
        public string UUID { set; get; }
        /// <summary>
        /// Name of the player.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Field of the Player
        /// </summary>
        //[DataMember]
        //public Field Field { get; set; }

        //public IPlayerContract Player { set; get; }

            public PlayerData(string name)
        {
            UUID = Guid.NewGuid().ToString();
            Name = name;
        }

        public override bool Equals(object obj)
        {
            var data = obj as PlayerData;
            return data != null &&
                   UUID == data.UUID;
        }

        public override int GetHashCode()
        {
            return 2006673922 + EqualityComparer<string>.Default.GetHashCode(UUID);
        }
    }
}
