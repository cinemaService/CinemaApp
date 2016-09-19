using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spring.Messaging.Nms.Core;
using Apache.NMS.ActiveMQ.Commands;
using Apache.NMS;

namespace AbstractService
{
	public interface IMessageEventHandler<T>
	{
		void onMessage(T message);
	}
	public class GenericMessageListener<T> : IMessageListener
	{
		IMessageEventHandler<T> eventHandler;

		public GenericMessageListener(IMessageEventHandler<T> e)
		{
			this.eventHandler = e;
		}
		public void OnMessage(IMessage message)
		{
			try
			{
				ActiveMQObjectMessage msg = message as ActiveMQObjectMessage;
				if (msg != null)
				{
					object genMsg = msg.Body;
					T finalMsg;
					if (genMsg is T)
					{
						finalMsg = (T)genMsg;
					}
					else
					{
						try
						{
							finalMsg = (T)Convert.ChangeType(genMsg, typeof(T));
						}
						catch (InvalidCastException)
						{
							finalMsg = default(T);
							Console.WriteLine("Wrong message type!");
						}
					}
					eventHandler.onMessage(finalMsg);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
		}
	}
}
