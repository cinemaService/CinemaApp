using System.Text.RegularExpressions;
using AbstractService;
using EmailServiceModels;
using ServicesModels.db;
using ServicesModels.dto;
using Config = TransactionServiceModels.Config;

namespace TransactionService
{
    class EmailService : AService<Email>
    {
        public EmailService(string serviceName) : base(serviceName)
        {
        }

        public void SendEmail(Transaction transaction)
        {
            send(new Email()
            {
                Receiver = transaction.UserEmail,
                Header = "Your pay time!",
                Text = PrepareEmail(transaction)
            }, Config.EmailQueue, Config.Url);

            writeToLog("Send email with transaction link");
        }

        public void SendConfirmation(TransactionDto transactionDto)
        {
            send(new Email()
            {
                Receiver = transactionDto.UserEmail,
                Header = "Payment received",
                Text = PrepareConfirmation(transactionDto)
            }, Config.EmailQueue, Config.Url);
        }

        private string PrepareEmail(Transaction transaction)
        {
            var link = $"http://localhost:50864/Transaction?id={transaction.Id}";
            return $"You can pay now for your reservation by clicking this link: {link}.";
        }

        private string PrepareConfirmation(TransactionDto transactionDto)
        {
            return $"Thank you for your payment for transaction {transactionDto.Id} at {transactionDto.TransactionDate}. Your reservation number is {transactionDto.ReservationId}. You have to remeber this number!";
        }
    }
}
