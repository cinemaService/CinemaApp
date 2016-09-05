using EmailServiceModels;
using AbstractService;

namespace EmailListener
{
	class EmailService : AService<Email>
	{
		public EmailService(string serviceName) : base(serviceName)
		{
		}

		public void listen()
		{
			Sender sender = new Sender(this);
			base.listen(sender, Config.Url, Config.QueueName);
		}
		static void Main(string[] args)
		{
			EmailService service = new EmailService("E-mail Service");
			service.listen();
		}
	}
}
