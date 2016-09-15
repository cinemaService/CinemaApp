using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionServiceModels
{
    public static class Config
    {
        public static string Url
        {
            get { return "tcp://localhost:61616/"; }
        }

        public static string TransactionQueueName
        {
            get { return "TransactionQueue"; }
        }

        public static string EmailQueue
        {
            get { return "EmailQueue"; }
        }
    }
}
