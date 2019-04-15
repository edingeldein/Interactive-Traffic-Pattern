using System;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class Vector3Extensions
    {
        public static Vector3 TurnTowards(this Vector3 currentHeading, Vector3 targetHeading, float angularVelocity)
        {
            Vector3 diff = targetHeading - currentHeading;

            if (diff.magnitude < 0.01f)
                return targetHeading / targetHeading.magnitude;

            Vector3 diffVeloc = diff * angularVelocity;
            Vector3 newHeading = currentHeading + diffVeloc;
            return newHeading / newHeading.magnitude;
        }
    }
}
