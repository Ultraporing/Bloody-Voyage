using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Network.Types
{
    [Serializable]
    public class BV_Vector3
    {
        public float X = 0, Y = 0, Z = 0;

        public BV_Vector3()
        {
        }

        public BV_Vector3(float x)
        {
            X = x;
        }

        public BV_Vector3(float x, float y)
        {
            X = x;
            Y = y;
        }

        public BV_Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static implicit operator Vector3(BV_Vector3 vec)
        {
            return new Vector3(vec.X, vec.Y, vec.Z);
        }

        public static implicit operator BV_Vector3(Vector3 vec)
        {
            return new BV_Vector3(vec.x, vec.y, vec.z);
        }

        public override string ToString()
        {
            Vector3 v = this;
            return v.ToString();
        }
    }
}
