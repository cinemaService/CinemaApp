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
		ServicesModels.db.DatabaseContext db = new ServicesModels.db.DatabaseContext();

		// GET: Reservation
		public PartialViewResult Seans(ServicesModels.db.Movie movie)
		{
			return PartialView(
							db.Seances
								.Where(s => s.MovieId == movie.Id)
								.OrderBy(s => s.Date)
								.ToList());
		}

		public ActionResult Film(int? id)
		{
			if (id == null)
				return RedirectToAction("Index", "Home");

			var movie = db.Movies
				.Where(m => m.Id == id)
				.Single();

			return View(movie);
		}

        public ActionResult GetSeance(int? id)
        {
			if (id == null)
				return RedirectToAction("Index", "Home");

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