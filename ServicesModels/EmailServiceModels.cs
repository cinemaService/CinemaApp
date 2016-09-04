using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailServiceModels
{
	public static class Config
	{
		public static string Url
		{
			get { return "tcp://localhost:61616/"; }
		}
		public static string QueueName
		{
			get { return "EmailQueue"; }
		}
	}

	public static class ServerSmtpCredits
	{
		public static string ServerEmail
		{
			get { return "cinemaserviceksr@gmail.com"; }
		}

		public static string ServerEmailPassword
		{
			get { return "ksry2016"; }
		}

		public static string ServerEmailHost
		{
			get { return "smtp.gmail.com"; }
		}

		public static int ServerEmailPort
		{
			get { return 587; }
		}
	}

	[Serializable]
	public class Email
    {
		string receiver;
		string header;
		string text;

		public string Receiver
		{
			get	{return receiver;}
			set	{receiver = value;}
		}
		public string Header
		{
			get	{return header;	}
			set	{header = value;}
		}
		public string Text
		{
			get{return text;}
			set	{text = value;}
		}
	}
}
