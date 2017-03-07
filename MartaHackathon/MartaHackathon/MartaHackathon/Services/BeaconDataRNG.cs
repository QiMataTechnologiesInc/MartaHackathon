using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartaHackathon.Services
{
    class BeaconDataRNG
    {
        private Random _random = new Random();

        public IEnumerable<BeaconDataDTO> GetRandomData()
        {
            List<BeaconDataDTO> beaconData = new List<BeaconDataDTO>();

            for (int i = 0; i < _random.Next(5); i++)
            {
                beaconData.Add(new BeaconDataDTO
                {
                    Major = _random.Next(),
                    Minor = _random.Next(),
                    Uuid = Guids()[_random.Next(4)].ToString(),
                    Proximity = Proximities[_random.Next(2)]
                });
            }

            return beaconData;
        }

        private List<string> Proximities = new List<string>
        {
            "Immediate",
            "Near",
            "Far"
        };

        private List<Guid> Guids()
        {
            List<Guid> guids = new List<Guid>();

            guids.Add(Guid.Parse("1FA78199-067F-4846-B19A-582378662EBD"));
            guids.Add(Guid.Parse("E4ADF3D1-3E5F-4667-B6C3-EE7E1D24F8AE"));
            guids.Add(Guid.Parse("5A209F44-2F2D-45CB-8CAB-31FCA70AE4F1"));
            guids.Add(Guid.Parse("1E24C78B-CBBA-4B8B-BC98-65BA4568AFEC"));
            guids.Add(Guid.Parse("98ECB6FE-7787-4A76-82D8-B93246106BF1"));

            return guids;
        }
    }
}
