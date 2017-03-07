using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MartaHackathonWeb.Models
{
    public class BeaconReadingDto
    {
        public int BeaconId { get; set; }

        public string UserId { get; set; }

        public string Distance { get; set; }
    }
}