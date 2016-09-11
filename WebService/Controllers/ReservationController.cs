using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebService.Models;

namespace WebService.Controllers
{
    public class ReservationController : Controller
    {
        // GET: Reservation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetSeance(int id)
        {
            Spot[] spots =
            {
                new Spot()
                {
                    Id = 1,
                    Number = "1-A"
                },
                new Spot()
                {
                    Id = 2,
                    Number = "2-A"
                },
                new Spot()
                {
                    Id = 3,
                    Number = "1-B"
                },
                new Spot()
                {
                    Id = 4,
                    Number = "2-B",
                    Reserved = true
                }
            };
            var reservation = new Reservation()
            {
                Spots = new List<Spot>(spots)
                
            };

            return View(reservation);
        }

        [HttpPost]
        public ActionResult Reserve()
        {
            var spots = Request.Form.GetValues("spots");
            var email = Request.Form.Get("Email");

            var reervation = new Reservation()
            {
                Email = email,
                Spots = spots.Select(s => new Spot()
                {
                    Id = int.Parse(s)
                }).ToList()
            };

            return RedirectToAction("Index", "Home");
        }
    }
}