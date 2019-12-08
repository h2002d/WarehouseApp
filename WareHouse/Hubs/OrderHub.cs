using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WareHouse.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;

namespace WareHouse.Hubs
{
    public class OrderHub : Hub
    {
        public void OrderMade(string productName, decimal quantity)
        {
            // Clients.AllExcept(Context.ConnectionId).newOrder(productName, quantity);
            var hubContext = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<OrderHub>();
            hubContext.Clients.All.newOrder(productName, quantity);

        }

    }
}