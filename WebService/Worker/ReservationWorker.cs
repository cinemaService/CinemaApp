using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using AbstractService;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Models.dto;
using ReservationServiceModels;
using ServicesModels.dto;
using Spring.Messaging.Nms.Listener;

namespace WebService.Worker
{
    public class ReservationWorker
    {
        private static ReservationWorker instance;

        private SimpleMessageListenerContainer listenerContainer;

        private ReservationWorker()
        {
            ConnectionFactory factory = new ConnectionFactory(Config.Url);

            listenerContainer = new SimpleMessageListenerContainer
            {
                ConnectionFactory = factory,
                DestinationName = Config.ReturnMessageQueue,
                MessageListener = new GenericMessageListener<MessageDto>(new MessageHandler())
            };
            listenerContainer.AfterPropertiesSet();
        }

        private class MessageHandler : IMessageEventHandler<MessageDto>
        {
            public void onMessage(MessageDto message)
            {
                Thread.Sleep(1000);
                var users = MessageHub.GetUsers();

                if (users.ContainsKey(message.Email))
                {
                    var socket = users[message.Email];

                    socket.send(message.Message);
                }
            }
        }

        public static ReservationWorker GetInstance()
        {
            return instance ?? (instance = new ReservationWorker());
        }
    }
}