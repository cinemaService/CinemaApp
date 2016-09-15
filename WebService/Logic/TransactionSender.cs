using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AbstractService;
using ServicesModels.dto;
using TransactionServiceModels;

namespace WebService.Logic
{
    public class TransactionSender : AService<ServicesModels.dto.TransactionDto>
    {
        public TransactionSender(string serviceName) : base(serviceName)
        {
        }

        public void send(int transactionId)
        {
            var transaction = new TransactionDto()
            {
                Id = transactionId,
                TransactionDate = DateTime.Now
            };

            send(transaction, Config.ConfirmTransQueueName, Config.Url);
            writeToLog($"Transaction with id: {transaction.Id} confirmed");
        }
    }
}