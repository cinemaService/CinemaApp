using ServicesModels.dto;
using AbstractService;
using ReservationServiceModels;

namespace ReservationService
{
    public class TransactionService : AService<ReservationDto>
	{
        public TransactionService(string name) : base(name)
        {
        }

        public void Send(ReservationDto messageBody)
        {
			send(messageBody, Config.TransQueueName, Config.Url);
        }
    }
}