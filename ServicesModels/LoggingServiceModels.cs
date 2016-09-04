using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingServiceModel
{
	public static class Config
	{
		public static string Url
		{
			get { return "tcp://localhost:61616/"; }
		}
		public static string QueueName
		{
			get { return "LoggerQueue"; }
		}
		public static string LogFileName
		{
			get { return "ServicesLogs.txt"; }
		}
		public static string InitialLogText
		{
			get { return "Cinema Service Log\n           Authors: Marek Tokarski, Marcin Knap\n"; }
		}
	}
	[Serializable]
	public class LogMessage
	{
		string  serviceName;
		string  message;
		LogType msgType;

	    public enum LogType { INFO, WARNING, ERROR };

	    static Dictionary<LogType, string> logMap = new Dictionary<LogType, string>
		{
			{ LogType.INFO,   "INFO" },
			{ LogType.WARNING,"WARNING" },
			{ LogType.ERROR,  "ERROR" }
		};

		public static Dictionary<LogType, string> LogTypeMap
		{
			get { return logMap;  }
		}
		public string ServiceName
		{
			get { return serviceName; }
			set { serviceName = value; }
		}
		public string Message
		{
			get { return message; }
			set { message = value; }
		}

		public LogType MessageType
		{
			get { return msgType; }
			set { msgType = value; }
		}
	}
}
