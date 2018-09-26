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
        /// <summary>
        /// Name of the player.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Field of the Player
        /// </summary>
        [DataMember]
        public Field Field { get; set; }

        public IPlayerContract Player { set; get; }

        public override bool Equals(object obj)
        {
            var data = obj as PlayerData;
            return data != null &&
                   Name == data.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }
    }
}
