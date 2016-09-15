using System;
using AbstractService;
using ServicesModels.db;
using ServicesModels.dto;
using TransactionServiceModels;

namespace TransactionService
{
    class CreateTransactionService : AService<object>
    {
        private EmailService emailService;

        public CreateTransactionService(EmailService emailService, string serviceName) : base(serviceName)
        {
            this.emailService = emailService;
        }

        public void listen()
        {
            ConfirmTransactionService confirmTransactionService = new ConfirmTransactionService(emailService, "ConfirmTransactionService");
            ApprovedReservationListener listener = new ApprovedReservationListener(this, confirmTransactionService);
            base.listen(listener, Config.Url, Config.TransactionQueueName);
        }

        public void Consume(ReservationDto reservationDto)
        {
            Transaction transaction;
            using (DatabaseContext db = new DatabaseContext())
            {
                transaction = new Transaction()
                {
                    ReservationId = reservationDto.Id,
                    UserEmail = reservationDto.Email,
                    Price = reservationDto.Spots.Length * 20
                };

                db.Transactions.Add(transaction);

                db.SaveChanges();
            }

            emailService.SendEmail(transaction);
            Console.WriteLine("Transaction created correctly");
            writeToLog("Transaction created correctly");
        }

        static void Main(string[] args)
        {
            EmailService emailService = new EmailService("EmailService");
            CreateTransactionService _createTransactionService = new CreateTransactionService(emailService, "CreateTransactionService");

            _createTransactionService.listen();
        }
    }
}
