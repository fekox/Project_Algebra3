using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using Unity.VisualScripting;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using UnityEditor.Experimental.GraphView;
using static Activity_Planes;
using UnityEngine.UIElements;

public class Activity_Planes 
{
    public struct MrPlane 
    {
        #region Variables
        private Vec3 var_Normal;
        
        public Vec3 verA;
        public Vec3 verB;
        public Vec3 verC;

        private float var_Distance;
        #endregion

        #region Properties

        /// <summary>
        /// Retorna el vector3 normal de un plano.
        /// </summary>
        public Vec3 normal 
        {
            get { return var_Normal; }

            set { var_Normal = value; }
        }

        /// <summary>
        /// Retorna la distancia medida desde el plano hasta 
        /// el origen, a lo largo de la normal del Plano.
        /// </summary>
        public float distance
        {
            get { return var_Distance; }

            set { var_Distance = value; }
        }

        /// <summary>
        /// Retorna una copia del plano con sus caras en la direccion opuesta.
        /// </summary>
        public MrPlane flipped 
        {
            get 
            { 
                return new MrPlane(-var_Normal, -var_Distance); 
            }
        }
        #endregion

        #region Constructors
        //Crear un Plano: https://docs.unity3d.com/ScriptReference/Plane-ctor.html

        /// <summary>
        /// Crea un plano en base a una normal y un punto.
        /// 
        /// var_Normal se normaliza.
        /// 
        /// var_Distance es igual al negativo del producto punto entre 
        /// var_Normal y inPoint del vec3.
        /// 
        /// Se settean los vertices A,B,C del plano.
        /// 
        /// </summary>
        /// <param name="inNormal"></param>
        /// <param name="inPoint"></param>
        public MrPlane(Vec3 inNormal, Vec3 inPoint)
        {
            var_Normal = Vec3.Normalize(inNormal); 
            var_Distance = -Vec3.Dot(var_Normal, inPoint); 

            verA = inPoint;
            verB = inPoint;
            verC = inPoint;
        }

        /// <summary>
        /// Crea un plano en base a una normal y un float.
        /// 
        /// var_Normal se normaliza.
        /// 
        /// var_Distance es igual al float d.
        /// 
        /// Se settean los vertices A,B,C del plano.
        /// </summary>
        /// <param name="inNormal"></param>
        /// <param name="d"></param>
        public MrPlane(Vec3 inNormal, float d)
        {
            var_Normal = Vec3.Normalize(inNormal); 
            var_Distance = d; 

            verA = inNormal;
            verB = inNormal;
            verC = inNormal;
        }

        /// <summary>
        /// Crea un plano en base a 3 vector3.
        /// 
        /// var_Normal es igual al producto cruz normalizado de los vectores a,b,c.
        /// 
        /// var_Distance es igual al negativo del producto punto entre var_Normal
        /// y a.
        /// 
        /// Se settean los vertices A,B,C del plano.
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        public MrPlane(Vec3 a, Vec3 b, Vec3 c)
        {
            var_Normal = Vec3.Normalize(Vec3.Cross(b - a, c - a));
            var_Distance = -Vec3.Dot(var_Normal, a); 

            verA = a;
            verB = b;
            verC = c;
        }
        #endregion

        #region Functions
        /// <summary>
        /// https://docs.unity3d.com/ScriptReference/Plane.ClosestPointOnPlane.html
        /// 
        /// Retorna el punto más cercano a un punto dado que se encuentra en el plano.
        /// 
        /// num es distancia a la que se encuentra el punto del plano.
        /// 
        /// Retorna el punto dentro del plano que se encuentra mas cerca del punto que le dimos.
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Vec3 ClosestPointOnPlane(Vec3 point)
        {
            var num = Vec3.Dot(var_Normal, point) + var_Distance;
            return point - (var_Normal * num); 
        }

        /// <summary>
        /// Hace que el plano mire en la direccion opuesta.
        /// </summary>
        public void Flip()
        {
            var_Normal = -var_Normal;
            var_Distance = -var_Distance;
        }

        /// <summary>
        /// https://docs.unity3d.com/ScriptReference/Plane.GetDistanceToPoint.html
        /// 
        /// Retorna una distancia desde el plano hasta el punto dado.
        /// 
        /// point el punto del cual queremos calcular la distancia
        /// al plano.
        /// 
        /// retorna la distancia  a la que se encuentra el punto 
        /// del plano.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public float GetDistanceToPoint(Vec3 point)
        {
            return Vec3.Dot(var_Normal, point) + var_Distance;
        }

        /// <summary>
        /// https://docs.unity3d.com/ScriptReference/Plane.GetSide.html
        /// 
        /// Retorna si un punto se encuentra dentor del lado positivo 
        /// del plano o no.
        /// 
        /// Si el resultado del producto punto mas la distancia es 
        /// mayor a zero retorna true (ya que se encuentra dentro 
        /// del lado positivo), sino retorna false.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool GetSide(Vec3 point)
        {
            return Vec3.Dot(var_Normal, point) + var_Distance > 0.0f;                                               
        }

        /// <summary>
        /// https://docs.unity3d.com/ScriptReference/Plane.SameSide.html
        /// 
        /// Retorna si dos puntos se encuentran del mismo lado del plano.
        /// 
        /// Calculo la distancia al punto 0
        /// Calculo la distancia al punto 1
        /// 
        /// Si la distancia de los dos puntos es mayor o igual a zero retorno true.
        /// 
        /// Si no retorno false.
        /// </summary>
        /// <param name="inPt0"></param>
        /// <param name="inPt1"></param>
        /// <returns></returns>
        public bool SameSide(Vec3 inPt0, Vec3 inPt1)
        {
            var distanceToPoint0 = GetDistanceToPoint(inPt0);
            var distanceToPoint1 = GetDistanceToPoint(inPt1);

            if (distanceToPoint0 >= 0.0f && distanceToPoint1 >= 0.0f)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        /// <summary>
        /// https://docs.unity3d.com/ScriptReference/Plane.Set3Points.html
        /// 
        /// Setea un plano utilizando tres puntos que se encunetran dentro en su interio.
        /// 
        /// var_Normal es igual al producto cruz de los 3 vectores normalizado.
        /// 
        /// var_Distance es igual al producto punto de var_Normal y a.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        public void Set3Points(Vec3 a, Vec3 b, Vec3 c)
        {
            var_Normal = Vec3.Normalize(Vec3.Cross(b - a, c - a));
            var_Distance = 0f - Vec3.Dot(var_Normal, a);
        }

        /// <summary>
        /// https://docs.unity3d.com/ScriptReference/Plane.SetNormalAndPosition.html
        /// 
        /// Setea un plano utilizando un punto que se encuentra 
        /// dentro de el junto con una normal para orientarlo.
        /// 
        /// var_Normal se normaliza.
        /// 
        /// var_Distance es igual al producto punto entre la normal y el punto del vector3.
        /// </summary>
        /// <param name="inNormal"></param>
        /// <param name="inPoint"></param>
        public void SetNormalAndPosition(Vec3 inNormal, Vec3 inPoint)
        {
            var_Normal = Vec3.Normalize(inNormal);
            var_Distance = 0f - Vec3.Dot(var_Normal, inPoint);
        }

        /// <summary>
        /// https://docs.unity3d.com/ScriptReference/Plane.Translate.html
        /// 
        /// Retorna una copia del plano dado que se mueve en el 
        /// espacio por el translate dado.
        /// 
        /// //La nueva distancia es igual al producto punto de 
        /// var_Normal y el desplazamiento en el espacio para 
        /// mover el plano (translate).
        /// </summary>
        /// <param name="plane"></param>
        /// <param name="translate"></param>
        /// <returns></returns>
        public static MrPlane Translate(MrPlane plane, Vec3 translate)
        {
            return new MrPlane(plane.var_Normal, plane.var_Distance += Vec3.Dot(plane.var_Normal, translate));
                                                                                                               
        }

        /// <summary>
        /// Traslada el plano en el espacio.
        /// 
        /// var_Distance es igual a var_Distance mas el resultado del producto punto
        /// de var_Normal y translate.
        /// </summary>
        /// <param name="translate"></param>
        public void Translate(Vec3 translate)
        {
            var_Distance += Vec3.Dot(var_Normal, translate);
        }
        #endregion
    }
}
