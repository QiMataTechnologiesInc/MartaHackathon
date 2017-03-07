using System;
using System.Collections.Generic;
using System.Linq;
using Assets.DataModels;
using Assets.Helpers;
using SignalR.Client._20.Hubs;
using UnityEngine;

class HelpData
{
    public bool NeedsHelp { get; set; }
    public string UserId { get; set; }
}

class BeaconDataContainer
{
    public BeaconDataDTO BeaconDataDto1 { get; set; }
    public BeaconDataDTO BeaconDataDto2 { get; set; }
}
public class DataConnector : MonoBehaviour {
    HubConnection connection;
    private IHubProxy proxy;
    private Dictionary<string, Beacon> _beacons;
    private Triangulater _triangulater;

    private GameObject _user1;
    private GameObject _user2;

    private BeaconDataContainer _user1Action;
    private BeaconDataContainer _user2Action;

    private GameObject _user1Sphere;
    private GameObject _user2Sphere;

    private HelpData _helpData;

    public DataConnector()
    {
        _triangulater = new Triangulater();   
    }

    // Use this for initialization
    void Start () {
		connection = new HubConnection("http://martahackathon.azurewebsites.net/");
        proxy = connection.CreateProxy("BeaconDataHub");

        _beacons = new Dictionary<string,Beacon>
        {
            {"c9407f30-f5f8-466e-aff9-25556b57fe6e", new Beacon("c9407f30-f5f8-466e-aff9-25556b57fe6e")},
            {"c9407f30-f5f8-466e-aff9-25556b57fe6d", new Beacon("c9407f30-f5f8-466e-aff9-25556b57fe6d")},
            {"c9407f30-f5f8-466e-aff9-25556b57fe6f", new Beacon("c9407f30-f5f8-466e-aff9-25556b57fe6f")},
            {"c9407f30-f5f8-466e-aff9-25556b57fe6a", new Beacon("c9407f30-f5f8-466e-aff9-25556b57fe6a")},
            {"e20a39f4-73f5-4bc4-a12f-17d1ad07a961", new Beacon("e20a39f4-73f5-4bc4-a12f-17d1ad07a961")},
            {"2f234454-cf6d-4a0f-adf2-f4911ba9ffab", new Beacon("2f234454-cf6d-4a0f-adf2-f4911ba9ffab")}
        };

        _user1 = GameObject.Find("User1");
        _user2 = GameObject.Find("User2");
        _user1Sphere = GameObject.Find("User1Sphere");
        _user2Sphere = GameObject.Find("User2Sphere");

        proxy.Subscribe("IncomingData").Data += data =>
        {
            var json = data[0].ToString();
            var mobileBeaconInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<MobileBeaconInfo>(json);

            if (mobileBeaconInfo.BeaconDataDto.Count() < 2)
            {
                return;
            }

            if (!_beacons.All(x => mobileBeaconInfo.BeaconDataDto.Select(y => y.Uuid).Contains(x.Key)))
            {
                return;
            }

            var orderedPoisitons =
                mobileBeaconInfo.BeaconDataDto.OrderBy(x => (Proximity) Enum.Parse(typeof(Proximity), x.Proximity))
                .ToList();

            if (mobileBeaconInfo.UserId == "1")
            {
                _user1Action = new BeaconDataContainer
                {
                    BeaconDataDto1 = orderedPoisitons[0],
                    BeaconDataDto2 = orderedPoisitons[1]
                };
            }
            else
            {
                _user2Action = new BeaconDataContainer
                {
                    BeaconDataDto1 = orderedPoisitons[0],
                    BeaconDataDto2 = orderedPoisitons[1]
                };
            }
        };

        proxy.Subscribe("Help").Data += data =>
        {
            var help = Convert.ToBoolean(data[0].ToString());
            var userId = data[1].ToString();

            _helpData = new HelpData
            {
                NeedsHelp =  help,
                UserId =  userId
            };
        };

        connection.Start();
        
    }

    private float CalculateZDistance(BeaconDataDTO orderedPoisiton, BeaconDataDTO p1)
    {
        return _triangulater.Calculate(new DistanceProximity
        {
            Distance = GetBeacon(orderedPoisiton.Uuid).Z,
            Proximity = (Proximity)Enum.Parse(typeof(Proximity), orderedPoisiton.Proximity)
        }, new DistanceProximity
        {
            Distance = GetBeacon(p1.Uuid).Z,
            Proximity = (Proximity)Enum.Parse(typeof(Proximity), p1.Proximity)
        });
    }

    private float CalculateXDistance(BeaconDataDTO orderedPoisiton, BeaconDataDTO p1)
    {
        try
        {
            if (!_beacons.ContainsKey(orderedPoisiton.Uuid)
                || !_beacons.ContainsKey(p1.Uuid))
            {
                return 0;
            }
            return _triangulater.Calculate(new DistanceProximity
            {
                Distance = GetBeacon(orderedPoisiton.Uuid).X,
                Proximity = (Proximity)Enum.Parse(typeof(Proximity), orderedPoisiton.Proximity)
            }, new DistanceProximity
            {
                Distance = GetBeacon(p1.Uuid).X,
                Proximity = (Proximity)Enum.Parse(typeof(Proximity), p1.Proximity)
            });
        }
        catch (Exception exception)
        {
            return 0;
        }
    }

    private Beacon GetBeacon(string id)
    {
            return _beacons[id.ToUpper()];
    }

    // Update is called once per frame
	void Update ()
    {
        if (_user1Action != null)
        {
            var calculateXDistance = CalculateXDistance(_user1Action.BeaconDataDto1,
               _user1Action.BeaconDataDto2);
            Debug.Log("x:" + calculateXDistance);
            var calculateZDistance = CalculateZDistance(_user1Action.BeaconDataDto1,
               _user1Action.BeaconDataDto2);
            Debug.Log("z:" + calculateZDistance);
            if (calculateZDistance != 0 || calculateXDistance != 0)
            {
                _user1.transform.position = new Vector3(calculateXDistance, _user1.transform.position.y, calculateZDistance);
                _user1Sphere.transform.position = new Vector3(calculateXDistance, _user1Sphere.transform.position.y, calculateZDistance);
                _user1Action = null;
            }
            
        }
        if (_user2Action != null)
        {
            var calculateXDistance = CalculateXDistance(_user2Action.BeaconDataDto1,
               _user2Action.BeaconDataDto2);
            Debug.Log("x:" + calculateXDistance);
            var calculateZDistance = CalculateZDistance(_user2Action.BeaconDataDto1,
               _user2Action.BeaconDataDto2);
            Debug.Log("z:" + calculateZDistance);
            if (calculateZDistance != 0 || calculateXDistance != 0)
            {
                _user2.transform.position = new Vector3(calculateXDistance, _user2.transform.position.y, calculateZDistance);
                _user2Sphere.transform.position = new Vector3(calculateXDistance, _user2Sphere.transform.position.y, calculateZDistance);
                _user2Action = null;
            }
            
        }
        if (_helpData != null)
        {
            if (_helpData.UserId == "1")
            {
                if (_helpData.NeedsHelp)
                {
                    _user1Sphere.GetComponent<Renderer>().material.color = Color.blue;
                }
                else
                {
                    _user1Sphere.GetComponent<Renderer>().material.color = Color.white;
                }
            }
            else
            {
                if (_helpData.NeedsHelp)
                {
                    _user2Sphere.GetComponent<Renderer>().material.color = Color.blue;
                }
                else
                {
                    _user2Sphere.GetComponent<Renderer>().material.color = Color.white;
                }
            }
            _helpData = null;
        }
    }

    void OnDestroy()
    {
        if (connection != null)
        {
            connection.Stop();
        }
    }
}
