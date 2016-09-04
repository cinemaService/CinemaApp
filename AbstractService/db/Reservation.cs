using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractService.db
{
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
