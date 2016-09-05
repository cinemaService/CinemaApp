using System;
using ServicesModels.dto;
using AbstractService;

namespace ReservationService
{
	public class ReservationListener : IMessageEventHandler<ReservationDto>
	{
        private ReservationService reservationService;

        public ReservationListener(ReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

		public void onMessage(ReservationDto message)
		{
            reservationService.Consume(message);
            Console.WriteLine("Message acknowledged!");
        }
    }
}