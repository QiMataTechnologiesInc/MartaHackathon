using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MartaHackathonWeb.Models
{
    public class BeaconDto
    {
        public int StationId { get; set; }

        public string BeaconUuid { get; set; }

        public string BeaconString { get; set; }

        public int BeaconId { get; set; }
    }
}