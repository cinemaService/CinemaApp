﻿using System;
using System.Collections.Generic;

namespace ServicesModels.db
{
    [Serializable]
    public class Seance
    {
        public Seance()
        {
            this.Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public int MovieId { get; set; }
        public int RoomId { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Room Room { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}