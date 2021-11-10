using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helper
{
    public class MathHelper
    {
        public static Vector3 GetLinearVector3(float t, Vector3 p0, Vector3 p1)
        {
            return (1f - t) * p0 + (t * p1);
        }

        public static Vector3 GetQuadraticVector3(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            float u = 1f - t;
            float uu = Mathf.Pow(u, 2);
            float uuu = 2 * u * t;
            float tt = Mathf.Pow(t, 2);

            return uu * p0 + uuu * p1 + tt * p2;
        }
    }
}