using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractService;
using TransactionServiceModels;
using ServicesModels.db;
using System.Threading;
using System.Reactive.Linq;
using System.Reactive;

namespace TransactionService
{
    class CancellingReservationService : AService<Reservation>
    {
        DatabaseContext database;
        const string emailHeader = "CinemaApp: Anulowanie rezerwacji";
        const string emailText = "Czas na potwierdzenie i opłacenie rezerwacji minął! Twoja rezerwacja została anulowana.";

        public CancellingReservationService(string name) : base(name)
		{
            database = new DatabaseContext();
        }

        public void Run()
        {
             Observable
                .Interval(TimeSpan.FromSeconds(Config.PeriodLengthTime))
                .Timestamp()
                .Subscribe(CheckReservationDate);
        }

        private void CheckReservationDate(Timestamped<long> _)
        {
            var dbReservations = database.Reservations
                                         .Join(database.Transactions,
                                               r => r.Id,
                                               t => t.ReservationId,
                                               (r, t) => new { Reservation = r, Transaction = t })
                                         .Where(join => join.Transaction.TransactionDate == null)
                                        /// .Select(join => join.Reservation)
                                         .ToList();
 
            foreach(var res in dbReservations)
            {
                int elapsed = (int)(DateTime.Now  -res.Reservation.ReservationDate).TotalSeconds;
                if (elapsed > Config.CancelReservationTime)
                {
                    CancelReservation(res.Reservation);
                  //  database.Transactions.Remove(res.Transaction);
                    database.Reservations.Remove(res.Reservation);
                }
            }
            database.SaveChanges();
            writeToLog("All reservations checked! Everybody paid!");
        }

        void CancelReservation(Reservation res)
        {
            writeToLog(string.Format("Reservation with Id: {0} for User: {1} cancelled!", res.Id, res.UserEmail));
            EmailServiceModels.Email email = new EmailServiceModels.Email();
            email.Header = emailHeader;
            email.Text = emailText;
            email.Receiver = res.UserEmail;
            send(email, EmailServiceModels.Config.QueueName, EmailServiceModels.Config.Url, false);
        }
    }
}
