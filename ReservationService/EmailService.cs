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

        public void SendRejection(Reservation reservation)
        {
            base.send(new Email()
            {
                Receiver = reservation.UserEmail,
                Header = "Reservation rejected",
                Text = PrepareBody(reservation) + "\n At least one spot is already engaged."
            }, ReservationServiceModels.Config.EmailQueue, Config.Url);
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
