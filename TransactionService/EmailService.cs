using AbstractService;
using EmailServiceModels;
using ServicesModels.db;
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
                Text = transaction.Id.ToString()
            }, Config.EmailQueue, Config.Url);

            writeToLog("Send email with transaction link");
        }
    }
}
