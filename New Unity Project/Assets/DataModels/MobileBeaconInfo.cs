using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.DataModels
{
    class MobileBeaconInfo
    {
        public IEnumerable<BeaconDataDTO> BeaconDataDto { get; set; }

        public string UserId { get; set; }
    }
}
