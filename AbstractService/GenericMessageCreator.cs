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
	public class GenericMessageCreator<T> : IMessageCreator
	{
		public T msg { get; set; }

		public GenericMessageCreator(T msg)
		{
			this.msg = msg;
		}

		IMessage IMessageCreator.CreateMessage(ISession session)
		{
			ActiveMQObjectMessage msg = new ActiveMQObjectMessage();
			msg.Body = this.msg;
			return msg;
		}
	}
}
