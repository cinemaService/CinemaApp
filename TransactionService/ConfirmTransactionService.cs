using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractService;
using LoggingServiceModel;
using ServicesModels.db;
using ServicesModels.dto;
using Config = TransactionServiceModels.Config;

namespace TransactionService
{
    class ConfirmTransactionService : AService<TransactionDto>
    {
        private EmailService emailService;

        public ConfirmTransactionService(EmailService emailService, string serviceName) : base(serviceName)
        {
            this.emailService = emailService;
        }

        public void Consume(TransactionDto transactionDto)
        {
            if (transactionDto.TransactionDate != null)
            {
                try
                {
                    using (var db = new DatabaseContext())
                    {
                        var transaction = db.Transactions.Single(t => t.Id == transactionDto.Id);
                        transaction.TransactionDate = transactionDto.TransactionDate;

                        transactionDto.UserEmail = transaction.UserEmail;
                        transactionDto.ReservationId = transaction.ReservationId;
                        transactionDto.Price = transaction.Price;

                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    writeToLog($"Unable to confirm payment for transaction with id: {transactionDto.Id}!", LogMessage.LogType.ERROR);
                }

                emailService.SendConfirmation(transactionDto);
            }
            else
            {
                writeToLog($"Transaction confirmation (id: {transactionDto.Id}) with empty date!", LogMessage.LogType.WARNING);
            }
        }
    }
}
