using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationServiceModels
{
	public static class Config
	{
		public static string Url
		{
			get { return "tcp://localhost:61616/"; }
		}
		public static string TransQueueName
		{
			get { return "TransactionQueue"; }
		}
		public static string ReservQueueName
		{
			get { return "ReservationQueue"; }
		}

	    public static string EmailQueue
	    {
	        get { return "EmailQueue"; }
	    }

		public static string DbName
		{
			get { return "CinemaDb"; }
		}

	}
}
