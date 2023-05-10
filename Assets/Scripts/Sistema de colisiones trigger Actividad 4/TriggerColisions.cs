using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using static Activity_Planes;
using System.Security.Cryptography;

public class TriggerColisions : MonoBehaviour
{
   struct Line 
   {
        public Vec3 origin;
        public Vec3 direction;

        public Line(Vec3 origin, Vec3 direction) 
        {
            this.origin = origin;
            this.direction = direction;
        }
   }
}
