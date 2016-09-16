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
            IObservable<long> observable = Observable.Interval(TimeSpan.FromSeconds(Config.PeriodLengthTime));

            // Token for cancelation
            CancellationTokenSource source = new CancellationTokenSource();

            // Create task to execute.
            Action action = (() => CheckReservationDate());

            observable.Subscribe(x => {
                Task task = new Task(action); task.Start();
            }, source.Token);
        }

        void CheckReservationDate()
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
                }
            }
            Console.WriteLine("gggg");
            database.SaveChanges();
            writeToLog("All reservations checked! Everybody paid!");
            Console.WriteLine("gggg");
        }

        void CancelReservation(Reservation res)
        {
            EmailServiceModels.Email email = new EmailServiceModels.Email();
            email.Header = emailHeader;
            email.Text = emailText;
            email.Receiver = res.UserEmail;
            send(email, EmailServiceModels.Config.QueueName, EmailServiceModels.Config.Url, false);
            writeToLog(string.Format("Reservation with Id: {0} for User: {1} cancelled!", res.Id, res.UserEmail));
            database.Reservations.Remove(res);
        }
    }
}
