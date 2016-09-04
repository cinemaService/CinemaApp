using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractService;
using LoggingServiceModel;


namespace LoggingService 
{
	class LoggingService : AService<LogMessage>
	{
		
        public LoggingService(string name) : base(name)
		{
		}

		public void listen()
		{
			LogListener listener = new LogListener();
			listen(listener, Config.Url, Config.QueueName);
			listener.close();
		}
		static void Main(string[] args)
		{
			LoggingService logger = new LoggingService("Logging Service");
			logger.listen();
		}
	}
}
