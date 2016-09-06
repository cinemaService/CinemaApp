using System;
using System.Linq;
using ServicesModels.db;
using ServicesModels.dto;
using AbstractService;
using LoggingServiceModel;
using Config = ReservationServiceModels.Config;

namespace ReservationService
{
    public class ReservationService : AService<Reservation>
    {
        private TransactionService transactionService;
        private EmailService emailService;

		public ReservationService(TransactionService transactionService, EmailService emailService, string name) : base(name)
		{
			this.transactionService = transactionService;
		    this.emailService = emailService;
		}

		public void listen()
		{
			ReservationListener listener = new ReservationListener(this);
			base.listen(listener, Config.Url, Config.ReservQueueName);
		}

		public void Consume(Reservation reservationDto)
        {
            Reservation res = null;
            var success = false;
            using (var db = new DatabaseContext())
            {
                var reservedSpots = db.Reservations
                    .Where(r => r.SeanceId == reservationDto.SeanceId)
                    .SelectMany(r => r.Spots)
                    .ToList();

                var reservation = reservedSpots.Any(s => reservationDto.Spots.Any(sDto => s.Id == sDto.Id));

                if (!reservation)
                {
                    var spotsId = reservationDto.Spots.Select(s => s.Id).ToArray();
                    reservationDto.Spots = db.Spots.Where(s => spotsId.Contains(s.Id)).ToList();
                    db.Reservations.Add(reservationDto);
                    success = true;
                    Console.WriteLine("Reservation succeeded.");
					writeToLog("Reservation succeeded.");
                }
                else
                {
                    Console.WriteLine("At least one spot is already engaged.");
					writeToLog("At least one spot is already engaged.", LogMessage.LogType.WARNING);
				}
                
                db.SaveChanges();
            }

            if (success)
            {
                transactionService.Send(reservationDto);
                emailService.Send(reservationDto);
                Console.WriteLine("Reservation performed correctly");
                writeToLog("Reservation performed correctly");
            }
        }

		static void Main(string[] args)
		{
			TransactionService tranService = new TransactionService("Transaction Service - RS");
            EmailService emailService = new EmailService("Email Service - RS");
			ReservationService service = new ReservationService(tranService, emailService, "Reservation Service - RS");
			service.listen();
		}
	}
}