using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Helpers
{
    enum Proximity
    {
        Unknown = 0,
        Immediate = 1,
        Near = 2,
        Far = 3
    }

    class DistanceProximity
    {
        public Proximity Proximity { get; set; }
        public float Distance { get; set; }
    }

    class Triangulater
    {
        public float Calculate(DistanceProximity distanceProximity1, DistanceProximity distanceProximity2)
        {
            if (distanceProximity1.Proximity == distanceProximity2.Proximity)
            {
                if (distanceProximity1.Distance > distanceProximity2.Distance)
                {
                    return ((distanceProximity1.Distance - distanceProximity2.Distance) / 2f) +
                           distanceProximity2.Distance;
                }
                else
                {
                    return ((distanceProximity2.Distance - distanceProximity1.Distance) / 2f) +
                           distanceProximity1.Distance;
                }
            }
            if (distanceProximity1.Proximity == Proximity.Immediate && distanceProximity2.Proximity == Proximity.Near)
            {
                if (distanceProximity1.Distance > distanceProximity2.Distance)
                {
                    return ((distanceProximity1.Distance - distanceProximity2.Distance) * 3f / 4f) +
                           distanceProximity2.Distance;
                }
                else
                {
                    return ((distanceProximity2.Distance - distanceProximity1.Distance) * 3f / 4f) +
                           distanceProximity1.Distance;
                }
            }
            if (distanceProximity1.Proximity == Proximity.Near && distanceProximity2.Proximity == Proximity.Immediate)
            {
                if (distanceProximity1.Distance > distanceProximity2.Distance)
                {
                    return ((distanceProximity1.Distance - distanceProximity2.Distance) * 3f / 4f) +
                           distanceProximity2.Distance;
                }
                else
                {
                    return ((distanceProximity2.Distance - distanceProximity1.Distance) * 3f / 4f) +
                           distanceProximity1.Distance;
                }
            }
            if (distanceProximity1.Proximity == Proximity.Immediate  || distanceProximity2.Proximity == Proximity.Far)
            {
                if (distanceProximity1.Distance > distanceProximity2.Distance)
                {
                    return ((distanceProximity1.Distance - distanceProximity2.Distance) * 7f/8f) +
                           distanceProximity2.Distance;
                }
                else
                {
                    return ((distanceProximity2.Distance - distanceProximity1.Distance) * 7f / 8f) +
                           distanceProximity1.Distance;
                }
            }
            if (distanceProximity1.Proximity == Proximity.Far || distanceProximity2.Proximity == Proximity.Immediate)
            {
                if (distanceProximity1.Distance > distanceProximity2.Distance)
                {
                    return ((distanceProximity1.Distance - distanceProximity2.Distance) * 7f / 8f) +
                           distanceProximity2.Distance;
                }
                else
                {
                    return ((distanceProximity2.Distance - distanceProximity1.Distance) * 7f / 8f) +
                           distanceProximity1.Distance;
                }
            }
            if (distanceProximity1.Proximity == Proximity.Far || distanceProximity2.Proximity == Proximity.Near)
            {
                if (distanceProximity1.Distance > distanceProximity2.Distance)
                {
                    return ((distanceProximity1.Distance - distanceProximity2.Distance) * 5f / 8f) +
                           distanceProximity2.Distance;
                }
                else
                {
                    return ((distanceProximity2.Distance - distanceProximity1.Distance) * 5f / 8f) +
                           distanceProximity1.Distance;
                }
            }
            if (distanceProximity1.Proximity == Proximity.Near || distanceProximity2.Proximity == Proximity.Far)
            {
                if (distanceProximity1.Distance > distanceProximity2.Distance)
                {
                    return ((distanceProximity1.Distance - distanceProximity2.Distance) * 5f / 8f) +
                           distanceProximity2.Distance;
                }
                else
                {
                    return ((distanceProximity2.Distance - distanceProximity1.Distance) * 5f / 8f) +
                           distanceProximity1.Distance;
                }
            }
            return 0;
        }
    }
}
