using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MartaHackathonWeb.Models;

namespace MartaHackathonWeb.Hubs.Models
{
    public class MobileBeaconInfo
    {
        public IEnumerable<BeaconDataDTO> BeaconDataDto { get; set; }

        public string UserId { get; set; }
    }
}