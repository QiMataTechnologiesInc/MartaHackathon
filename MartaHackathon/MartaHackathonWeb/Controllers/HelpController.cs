using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using MartaHackathonWeb.Hubs;
using MartaHackathonWeb.Hubs.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace MartaHackathonWeb.Controllers
{
    public class HelpController : ApiController
    {
        public IHttpActionResult Post([FromBody] bool help,[FromUri]string userId)
        {
            var context = GlobalHost.DependencyResolver
                .Resolve<IConnectionManager>()
                .GetHubContext<BeaconDataHub>();
            context.Clients.All.Help(help,userId);
            return Ok();
        }
    }
}