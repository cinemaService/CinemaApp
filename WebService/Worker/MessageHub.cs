using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WebService.Worker
{
    public class MessageHub : Hub
    {
        private static ConcurrentDictionary<string, dynamic> users = new ConcurrentDictionary<string, dynamic>();
        public void Register()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string username = HttpContext.Current.User.Identity.Name;
                dynamic userSocket = Clients.Caller;

                users[username] = userSocket;
            }
        }

        public static ConcurrentDictionary<string, dynamic> GetUsers()
        {
            return users;
        }
    }
}