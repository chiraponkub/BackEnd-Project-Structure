using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VShow_BackEnd.Hubs
{
    public class ChatHubs : Hub
    {
        public void SendMessage(string message)
        {
            Clients.All.SendAsync("SendMessage", message);
        }
    }
}
