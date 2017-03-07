using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MartaHackathonWeb.Hubs.I;
using MartaHackathonWeb.Hubs.Models;
using Microsoft.AspNet.SignalR;

namespace MartaHackathonWeb.Hubs
{
    public class BeaconDataHub : Hub<IIncomingMobileBeaconHandler>
    {
    }
}