using System;
using Apache.NMS.ActiveMQ;
using Spring.Messaging.Nms.Listener;
using LoggingServiceModel;
using Spring.Messaging.Nms.Core;

namespace AbstractService
{
	public abstract class AService<T>
	{
		protected string serviceName;

		protected AService(string serviceName)
		{
			this.serviceName = serviceName;
		}

		protected void listen(IMessageEventHandler<T> handler,string url, string queueName)
		{
			try
			{
				ConnectionFactory factory = new ConnectionFactory(url);

				SimpleMessageListenerContainer listenerContainer = new SimpleMessageListenerContainer();
				listenerContainer.ConnectionFactory = factory;
				listenerContainer.DestinationName = queueName;
				listenerContainer.MessageListener = new GenericMessageListener<T>(handler);
				listenerContainer.AfterPropertiesSet();

				string info = String.Format("Service started!");
				writeToLog(info, LogMessage.LogType.INFO);
                Console.WriteLine(info);
				Console.WriteLine("Press ENTER to exit.");
				Console.ReadLine();
				writeToLog("Service shuted down!");
			}
			catch (System.Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public void writeToLog(string message, LogMessage.LogType type = LogMessage.LogType.INFO)
		{
			LogMessage msg = new LogMessage();
			msg.Message = message;
			msg.MessageType = type;
			msg.ServiceName = serviceName;
			send(msg, Config.QueueName, Config.Url,false);
		}

		public void send<MSG>(MSG message, string queueName, string url,bool toLog = true)
		{
			ConnectionFactory conFactory = new ConnectionFactory(url);
			NmsTemplate temp = new NmsTemplate(conFactory);
			temp.Send(queueName, new GenericMessageCreator<MSG>(message));

			if(toLog)
				writeToLog("Sended message to " + queueName);
		}

	}
}
