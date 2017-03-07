using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MartaHackathonWeb.Hubs.Models;

namespace MartaHackathonWeb.Hubs.I
{
    public interface IIncomingMobileBeaconHandler
    {
        void IncomingData(MobileBeaconInfo mobileBeaconInfo);

        void Help(bool needsHelp, string userId);
    }
}