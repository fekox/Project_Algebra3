using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using Unity.VisualScripting;

public class Activity_Planes 
{
    Plane toSeeTheFunctions;
    public struct MrPlane 
    {
        private Vec3 var_Normal;

        private float var_Distance;

        public Vec3 normal //Retorna el vector normal de un plano.
        {
            get { return var_Normal; }

            set { var_Normal = value; }
        }

        public float distance //Retorna la distancia medida desde el plano hasta el origen, a lo largo de la normal del Plano.
        {
            get { return var_Distance; }

            set { var_Distance = value; }
        }
    }
}
