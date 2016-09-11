using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class Reservation
    {
        public string Email { get; set; }
        public List<Spot> Spots { get; set; }
    }

    public class Spot
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public bool Reserved { get; set; }
    }
}