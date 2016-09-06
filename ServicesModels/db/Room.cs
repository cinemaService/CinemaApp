using System;
using System.Collections.Generic;

namespace ServicesModels.db
{
    [Serializable]
    public class Room
    {
        public Room()
        {
            this.Spots = new HashSet<Spot>();
        }

        public int Id { get; set; }
        public string Number { get; set; }

        public virtual ICollection<Spot> Spots { get; set; }
    }
}