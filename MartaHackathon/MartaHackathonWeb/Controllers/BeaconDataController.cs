using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using MartaHackathonWeb.Dal;
using MartaHackathonWeb.Hubs;
using MartaHackathonWeb.Hubs.Models;
using MartaHackathonWeb.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MartaHackathonWeb.Controllers
{
    public class BeaconDataController: ApiController
    {
        private BeaconDal _beaconDal;
        private IEnumerable<BeaconDto> _beaconData;
        private DateTime _lastRead = DateTime.MinValue;

        public BeaconDataController()
        {
            _beaconDal = new BeaconDal();
        }

        public async Task<IHttpActionResult> Post([FromBody] IEnumerable<BeaconDataDTO> beaconData,
            [FromUri]string userId,CancellationToken cancellationToken)
        {
            var beacons = await GetBeaconData();

            var context = GlobalHost.DependencyResolver.Resolve<IConnectionManager>().GetHubContext<BeaconDataHub>();
            context.Clients.All.IncomingData(new MobileBeaconInfo
            {
                BeaconDataDto = beaconData,
                UserId = userId
            });
            //foreach (var beaconDataDto in beaconData)
            //{
            //    var singleOrDefault = beacons.SingleOrDefault(x =>
            //        String.Compare(x.BeaconUuid, beaconDataDto.Uuid, StringComparison.OrdinalIgnoreCase) ==
            //        0);

            //    if (singleOrDefault != null)
            //    {
            //        await _beaconDal.AddNewBeaconData(new BeaconReadingDto
            //        {
            //            BeaconId = singleOrDefault.BeaconId,
            //            Distance = beaconDataDto.Proximity,
            //            UserId = userId
            //        });
            //    }
            //}
            foreach (BeaconDataDTO beaconDataDto in beaconData)
            {
                if (beaconDataDto.Proximity == "Immediate" || beaconDataDto.Proximity == "Near")
                {
                    return
                        Ok(beacons.SingleOrDefault(x =>
                                    String.Compare(x.BeaconUuid, beaconDataDto.Uuid, StringComparison.OrdinalIgnoreCase) ==
                                    0)?.BeaconString ?? "");
                }
            }

            return Ok("");
        }

        public async Task<IEnumerable<BeaconDto>> GetBeaconData()
        {
            if (DateTime.Now - _lastRead > TimeSpan.FromSeconds(30))
            {
                _beaconData = await _beaconDal.GetBeacons();
            }
            return _beaconData;
        }
    }
}