using System;
using System.Collections.Generic;

namespace ServicesModels.db
{
    [Serializable]
    public class Reservation
    {
        public Reservation()
        {
            this.Spots = new HashSet<Spot>();
        }

        public int Id { get; set; }
        public string UserEmail { get; set; }
        public int SeanceId { get; set; }
        
        public virtual ICollection<Spot> Spots { get; set; }
        public virtual Seance Seance { get; set; }
    }
}
