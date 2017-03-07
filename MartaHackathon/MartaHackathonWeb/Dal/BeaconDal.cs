using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MartaHackathonWeb.Models;
using MySql.Data.MySqlClient;

namespace MartaHackathonWeb.Dal
{
    public class BeaconDal
    {
        public async Task<IEnumerable<BeaconDto>> GetBeacons()
        {
            List<BeaconDto> beaconDtos = new List<BeaconDto>
            {
                new BeaconDto
                {
                    BeaconId = 1,
                    BeaconUuid = "c9407f30-f5f8-466e-aff9-25556b57fe6e",
                    BeaconString = "Checkout the View",
                    StationId = 1
                },
                new BeaconDto
                {
                    BeaconId = 2,
                    BeaconUuid = "c9407f30-f5f8-466e-aff9-25556b57fe6d",
                    BeaconString = "Presenting MARTA Beacons",
                    StationId = 1
                },
                new BeaconDto
                {
                    BeaconId = 3,
                    BeaconUuid = "e2c56db5-dffb-48d2-b060-d0f5a71096e0",
                    BeaconString = "Welcome to the Garage Loft",
                    StationId = 1
                },new BeaconDto
                {
                    BeaconId = 4,
                    BeaconUuid = "c9407f30-f5f8-466e-aff9-25556b57fe6f",
                    BeaconString = "Check out MIT App Inventor at Tech Square Labs on March 4th from 5 to 7pm",
                    StationId = 1
                },
                new BeaconDto
                {
                    BeaconId = 5,
                    BeaconUuid = "c9407f30-f5f8-466e-aff9-25556b57fe6a",
                    BeaconString = "The Weather outside is 62F",
                    StationId = 1
                },new BeaconDto
                {
                    BeaconId = 6,
                    BeaconUuid = "2f234454-cf6d-4a0f-adf2-f4911ba9ffab",
                    BeaconString = "Meeting room in use",
                    StationId = 1
                }
            };

            return beaconDtos;
            using (MySqlConnection conn = new MySqlConnection("Server=us-cdbr-azure-east-c.cloudapp.net;Database=marta;Uid=be2ed5b87da5b2;Pwd=e67e48cd;"))
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select station_id,beacon_id,beacon_uuid,beacon_string from beacons";
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            beaconDtos.Add(new BeaconDto
                            {
                                StationId = reader.GetInt32(0),
                                BeaconId = reader.GetInt32(1),
                                BeaconUuid = reader.GetString(2),
                                BeaconString = reader.GetString(3)
                            });
                        }
                    }
                }
            }
            return beaconDtos;
        }

        public async Task AddNewBeaconData(BeaconReadingDto beaconData)
        {
            using (var conn = new MySqlConnection(
                        "Server=us-cdbr-azure-east-c.cloudapp.net;Database=marta;Uid=be2ed5b87da5b2;Pwd=e67e48cd;"))
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "insert into marta.beacon_reading (beacon_id,user_id,distance) " +
                                          $"values ({beaconData.BeaconId},'{beaconData.UserId}','{beaconData.Distance}')";
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}