using System;
using ServicesModels.dto;
using AbstractService;
using ServicesModels.db;

namespace ReservationService
{
	public class ReservationListener : IMessageEventHandler<Reservation>
	{
        private ReservationService reservationService;

        public ReservationListener(ReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

		public void onMessage(Reservation message)
		{
            reservationService.Consume(message);
            Console.WriteLine("Message acknowledged!");
        }
    }
}