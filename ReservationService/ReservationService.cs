using System;
using System.Linq;
using ServicesModels.db;
using ServicesModels.dto;
using AbstractService;
using ReservationServiceModels;

namespace ReservationService
{
    public class ReservationService : AService<ReservationDto>
    {
        private TransactionService transactionService;

		public ReservationService(TransactionService transactionService, string name) : base(name)
		{
			this.transactionService = transactionService;
		}

		public void listen()
		{
			ReservationListener listener = new ReservationListener(this);
			base.listen(listener, Config.Url, Config.ReservQueueName);
		}

		public void Consume(ReservationDto reservationDto)
        {
            Reservation res;
            var success = false;
            using (var db = new DatabaseContext())
            {
                var reservation =
                    db.Reservations.Where(r => r.SeanceId == reservationDto.SeanceId)
                        .SelectMany(r => r.Spots)
                        .Any(s => reservationDto.Spots.Contains(s.Id));

                if (!reservation)
                {
                    var spots = db.Spots.Where(spot => reservationDto.Spots.Contains(spot.Id)).ToList();

                    res = new Reservation()
                    {
                        SeanceId = reservationDto.SeanceId,
                        Spots = spots,
                        UserEmail = reservationDto.Email
                    };
                    db.Reservations.Add(res);
                    success = true;
                    Console.WriteLine("Reservation succeeded.");
					writeToLog("Reservation succeeded.");
                }
                else
                {
                    Console.WriteLine("At least one spot is already engaged.");
					writeToLog("At least one spot is already engaged.",LoggingServiceModel.LogMessage.LogType.WARNING);
				}
                
                db.SaveChanges();
            }

            if (success)
            {
                transactionService.Send(reservationDto);
            }
        }

		static void Main(string[] args)
		{
			TransactionService tranService = new TransactionService("Transaction Service");
			ReservationService service = new ReservationService(tranService, "Reservation Service");
			service.listen();
		}
	}
}