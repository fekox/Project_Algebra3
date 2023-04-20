using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using Unity.VisualScripting;

public class Activity_Planes 
{
    Plane toSeeTheFunctions;
    Vector3 a;
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

        //Retorna una copia del plano con sus caras en la direccion opuesta.
        public MrPlane flipped => new MrPlane(-var_Normal, 0f - var_Distance);


        //Crear un Plano: https://docs.unity3d.com/ScriptReference/Plane-ctor.html


        public MrPlane(Vec3 inNormal, Vec3 inPoint) //Crea un plano en base a una normal y un punto.
        {
            var_Normal = Vec3.Normalize(inNormal); //La normal se normaliza.
            var_Distance = 0f - Vec3.Dot(var_Normal, inPoint); //La distancia es igual al producto punto entre la normal y el punto del vec3.
        }

        public MrPlane(Vec3 inNormal, float d) //Crea un plano en base a una normal y un float.
        {
            var_Normal = Vec3.Normalize(inNormal); //La normal se normaliza.
            var_Distance = d; //La distacia es igual al float (la distancia se mide desde el Plano hasta el origen, a lo largo de la normal del Plano.). 
        }

        public MrPlane(Vec3 a, Vec3 b, Vec3 c) //Crea un plano en base a 3 vec3.
        {
            var_Normal = Vec3.Normalize(Vec3.Cross(b - a, c - a)); //La normal es igual al producto cruz de los 3 vectores normalizado.
            var_Distance = 0f - Vec3.Dot(var_Normal, a); //La distancia es igual al producto punto de la normal y a.
        }
    }
}
