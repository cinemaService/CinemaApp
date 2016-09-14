using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractService;
using ReservationServiceModels;
using WebService.Models;

namespace WebService.Controllers
{
    class ReservationSender : AService<ServicesModels.db.Reservation>
    {
        public ReservationSender(string name) : base(name)
        {
        }

        public void send(List<ServicesModels.db.Spot> dbSpots,Reservation request)
        {
            ServicesModels.db.Reservation reservationDb = new ServicesModels.db.Reservation()
            {
                SeanceId = request.SeanceId,
                Spots = dbSpots,
                UserEmail = request.Email
            };

            send(reservationDb, Config.ReservQueueName, Config.Url, false);
            writeToLog("Reservation request sended");
        }

    }
}
