﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.DataModels
{
    public class BeaconDataDTO
    {
        public string Proximity { get; set; }

        public int Major { get; set; }

        public int Minor { get; set; }

        public string Uuid { get; set; }
    }
}
