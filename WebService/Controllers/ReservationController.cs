using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebService.Models;
using ServicesModels;
using WebService.Logic;

namespace WebService.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        ServicesModels.db.DatabaseContext database = new ServicesModels.db.DatabaseContext();
        
        // GET: Reservation
        [AllowAnonymous]
        public PartialViewResult Seans(ServicesModels.db.Movie movie)
        {
            return PartialView(
                            database.Seances
                                .Where(s => s.MovieId == movie.Id)
                                .OrderBy(s => s.Date)
                                .ToList());
        }

        [AllowAnonymous]
        public ActionResult Film(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            var movie = database.Movies
                .Where(m => m.Id == id)
                .Single();

            return View(movie);
        }

        public ActionResult Seats(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            ServicesModels.db.Spot[] reservedSpots = database.Reservations
                .Where(r => r.SeanceId == id)
                .SelectMany(r => r.Spots)
                .OrderBy(s => s.Id)
                .ToArray();

            List<ServicesModels.db.Spot> allSpots = database.Seances
                .Where(s => s.Id == id)
                .Select(s => s.Room)
                .SelectMany(r => r.Spots)
                .ToList();  
               
            List<Spot> webSpots = new List<Spot>();

            foreach(var spot in allSpots)
            {
                bool reserved = reservedSpots.Any(s => s.Id == spot.Id);
                webSpots.Add(new Spot()
                {
                    Id = spot.Id,
                    Number = spot.Number,
                    Reserved = reserved,
                });
            }


            var reservation = new Reservation()
            {
                Spots = new List<Spot>(webSpots),
                SeanceId = (int)id,
                Email =  User.Identity.Name
            };


            return View(reservation);
        }

        [HttpPost]
        public ActionResult Reserve()
        {
            ReservationSender sender = new ReservationSender("WebService");
            var spots = Request.Form.GetValues("spots");
            var email = Request.Form.Get("Email");
            var id = int.Parse(Request.Form.Get("SeanceId"));
            var reservation = new Reservation()
            {
                SeanceId = id,
                Email = email,
            };
            int roomId = database.Seances
                            .Where(s => s.Id == id)
                            .Single()
                            .RoomId;

            List<ServicesModels.db.Spot> dbSpots = database.Spots
                                                        .Where(s => s.RoomId == roomId)
                                                        .Where(s => spots.Contains(s.Id.ToString()))
                                                        .ToList();                              

            sender.send(dbSpots,reservation);
            return RedirectToAction("Index", "Home");
        }
    }
}