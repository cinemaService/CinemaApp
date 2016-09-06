using AbstractService;
using EmailServiceModels;
using ServicesModels.db;
using ServicesModels.dto;

namespace ReservationService
{
    public class EmailService : AService<Email>
    {
        public EmailService(string serviceName) : base(serviceName)
        {
        }

        public void Send(Reservation reservation)
        {
            base.send(new Email()
            {
                Receiver = reservation.UserEmail,
                Header = "Reservation success",
                Text = PrepareBody(reservation)
            }, ReservationServiceModels.Config.EmailQueue, Config.Url);

            writeToLog("Prepare an email with reservation");
        }

        private static string PrepareBody(Reservation reservation)
        {
            string body = reservation.SeanceId.ToString() + " spots: ";

            foreach (var spot in reservation.Spots)
            {
                body += spot.Number + ", ";
            }

            return body;
        }
    }
}
