using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class Reservation
    {
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int SeanceId { get; set; }
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