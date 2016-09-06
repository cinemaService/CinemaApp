using System;
using AbstractService;
using ServicesModels.db;
using ServicesModels.dto;

namespace TransactionService
{
    class ApprovedReservationListener : IMessageEventHandler<ReservationDto>
    {
        private TransactionService transactionService;

        public ApprovedReservationListener(TransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        public void onMessage(ReservationDto message)
        {
            transactionService.Consume(message);
            Console.WriteLine("Message acknowledged!");
        }
    }
}
