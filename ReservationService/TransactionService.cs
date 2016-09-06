using System;
using System.Linq;
using ServicesModels.dto;
using AbstractService;
using ReservationServiceModels;
using ServicesModels.db;

namespace ReservationService
{
    public class TransactionService : AService<Reservation>
	{
        public TransactionService(string name) : base(name)
        {
        }

        public void Send(Reservation messageBody)
        {
            ReservationDto reservationDto = new ReservationDto()
            {
                Id = messageBody.Id,
                Email = messageBody.UserEmail,
                SeanceId = messageBody.SeanceId,
                Spots = messageBody.Spots.Select(s => s.Id).ToArray()
            };
			send(reservationDto, Config.TransQueueName, Config.Url);
            Console.WriteLine("Information send to Transaction Service");
            writeToLog("Information send to Transaction Service");
        }
    }
}