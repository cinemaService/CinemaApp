using System;
using AbstractService;
using ServicesModels.db;
using ServicesModels.dto;
using TransactionServiceModels;

namespace TransactionService
{
    class TransactionService : AService<ReservationDto>
    {
        private EmailService emailService;

        public TransactionService(EmailService emailService, string serviceName) : base(serviceName)
        {
            this.emailService = emailService;
        }

        public void listen()
        {
            ApprovedReservationListener listener = new ApprovedReservationListener(this);
            base.listen(listener, Config.Url, Config.TransQueueName);
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
            TransactionService transactionService = new TransactionService(emailService, "TransactionService");

            transactionService.listen();
        }
    }
}
