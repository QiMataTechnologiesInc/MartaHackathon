using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Helpers
{
    class Beacon
    {
        private GameObject _gameObject;
        public string Uuid { get; private set; }

        public Beacon(string uuid)
        {
            Uuid = uuid;
            _gameObject = GameObject.Find(uuid);
        }

        public float X
        {
            get
            {
                return _gameObject.transform.position.x;
            }
        }

        public float Z
        {
            get
            {
                return _gameObject.transform.position.z;
            }
        }
    }
}
