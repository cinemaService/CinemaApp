using System;
using AbstractService;
using ServicesModels.db;
using ServicesModels.dto;

namespace TransactionService
{
    class ApprovedReservationListener : IMessageEventHandler<object>
    {
        private CreateTransactionService _createTransactionService;
        private ConfirmTransactionService confirmTransactionService;

        public ApprovedReservationListener(CreateTransactionService _createTransactionService, ConfirmTransactionService confirmTransactionService)
        {
            this._createTransactionService = _createTransactionService;
            this.confirmTransactionService = confirmTransactionService;
        }

        public void onMessage(object message)
        {
            if (message is ReservationDto)
            {
                _createTransactionService.Consume((ReservationDto) message);
            }
            else if (message is TransactionDto)
            {
                confirmTransactionService.Consume((TransactionDto) message);
            }
            Console.WriteLine("Message acknowledged!");
        }
    }
}
