using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractService;
using Models.dto;
using ReservationServiceModels;

namespace ReservationService
{
    public class MessageService : AService<MessageDto>
    {
        public MessageService(string serviceName) : base(serviceName)
        {
        }

        public void Send(string email, string message)
        {
            MessageDto msg = new MessageDto()
            {
                Email = email,
                Message = message
            };

            send(msg, Config.ReturnMessageQueue, Config.Url);
            Console.WriteLine("Information send to Web");
            writeToLog("Information send to Web");
        }
    }
}
