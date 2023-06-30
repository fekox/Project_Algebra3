using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;
using UnityEngine;
using CustomMath;
using Unity.VisualScripting;
using System.Security.Principal;
using System.Numerics;
using UnityEngine.UIElements;

UnityEngine.Quaternion

Quaternion q;

public struct MrQuaternion 
{
    #region Variables
    public const float kEpsilon = 1E-06f;

    public float x; //X component of Quaternions. Don't touch if you don't understand X_X.
    public float y; //Y component of Quaternions. Don't touch if you don't understand X_X.
    public float z; //Z component of Quaternions. Don't touch if you don't understand X_X.
    public float w; //W component of Quaternions. Don't touch if you don't understand X_X.

    #endregion

    #region Contructs
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MrQuaternion(float x, float y, float z, float w) //Constructs a quaternion from the specified components.
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MrQuaternion(Vec3 vec3Part, float rotPart) //Creates a quaternion from the specified vector and rotation parts.
    {
        this.x = vec3Part.x;
        this.y = vec3Part.y;
        this.z = vec3Part.z;
        this.w = rotPart;
    }
    #endregion

    #region Properties

    public float this[int index] //Access the x, y, z, w components using [0], [1], [2], [3] respectively.
    {                            //Example: ( p[3] = 0.5f; the same as p.w = 0.5 ).

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            switch (index)
            {
                case 0: return x;

                case 1: return y;

                case 2: return z;

                case 3: return w;

                default: throw new IndexOutOfRangeException("Invalid Quaternion index!");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            switch (index)
            {
                case 0: x = value; break;

                case 1: y = value; break;

                case 2: z = value; break;

                case 3: w = value; break;

                default: throw new IndexOutOfRangeException("Invalid Quaternion index!");
            }
        }
    }

    public static MrQuaternion identity //Returns a quaternion that corresponds to ¨not rotated¨.
    {                                   //The object is perfectly aligned with the world or parent axes.
        get 
        { 
            return new MrQuaternion(0f, 0f, 0f, 1f); 
        }
    }

    public Vec3 eulerAngles //Falta completar.
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set 
        {
        
        }
    }

    public MrQuaternion normalized //Returns a quaternion normalizes.
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return Normalize(this);
        }
    }

    public float magnitude //Returns the magnitude.
    {
        get 
        {
            return Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2) + Mathf.Pow(z, 2) + Mathf.Pow(w, 2)); //Root of the sum of the square of each component.
                                                                                                      //Raíz de la suma del cuadrado de cada componente.
        }
    }

    public bool isIdentity //Returns true if whether the given quaternion is equals to the quaternion identity.
    {
        get 
        {
            return x == 0f && y == 0f && z == 0f && w == 1f;
        }
    }

    public MrQuaternion zero //Return a quaternion whose values are (0, 0, 0, 0).
    { 
        get 
        {
            return new MrQuaternion(0f, 0f, 0f, 0f);
        } 
    }
    #endregion

    #region Functions

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MrQuaternion Normalize(MrQuaternion q)
    {
        float num = Mathf.Sqrt(Dot(q, q)); //Obtain the result of the square root of the dot product of the quaternion.
        
        if (num < Mathf.Epsilon) //If the result is less than epsilon. (A tiny floating point value).
        {
            return identity; //Return MrQuaternion(0f, 0f, 0f, 1f). 
        }

        return new MrQuaternion(q.x / num, q.y / num, q.z / num, q.w / num); //If not, a quaternion divided by the previously obtained result is returned.
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Normalize() //Returns a quaternion normalizes.
    {
        this = Normalize(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Angle(MrQuaternion a, MrQuaternion b) //Devuelve el angulo entre dos Quaternions.
    {
        if (a.magnitude == 0 || b.magnitude == 0)
        {               
            return 0; //If the magnitude of both gives 0 return 0.
                      //Si la magnitud de ambos da 0 retorno 0.
        }

        return (float)((double)Mathf.Acos(Mathf.Abs(Dot(a, b)) * Mathf.Rad2Deg / (a.magnitude * b.magnitude)));//Pasa el resultado a grados y lo returnea.
    }

    public static MrQuaternion AngleAxis(float angle, Vec3 axis) //Genera una rotacion en el quaternion usando radianes.
    {
        MrQuaternion q = identity;

        axis.Normalize();

        axis *= (float)System.Math.Sin((angle / 2) * Mathf.Rad2Deg); //Calculo de la parte imaginaria.

        q.x = axis.x;
        q.y = axis.y;
        q.z = axis.z;

        q.w = (float)System.Math.Cos((angle / 2) * Mathf.Rad2Deg); //Calculo de la parte real.

        return Normalize(q); //Devuelve el quaternion normalizado.
    }

    public static MrQuaternion AxisAngle(Vec3 axis, float angle) //Genera una rotacion en el quaternion usando grados.
    {
        MrQuaternion q = identity;

        axis.Normalize();

        axis *= (float)System.Math.Sin(angle / 2); //Calculo de la parte imaginaria.

        q.x = axis.x;
        q.y = axis.y;
        q.z = axis.z;

        q.w = (float)System.Math.Cos(angle / 2); //Calculo de la parte real.

        return Normalize(q); //Devuelve el quaternion normalizado.
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Dot(MrQuaternion a, MrQuaternion b)
    {
        return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;//Return dot product by two quaternions.
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MrQuaternion Euler(float x, float y, float z) //Retorna un quaternion con sus coordenadas x, y, z rotadas.
    {
        float cos;
        float sin;
        MrQuaternion qx, qy, qz;
        MrQuaternion q = identity;

        cos = Mathf.Cos(Mathf.Deg2Rad * x * 0.5f); //Para calcular la parte real de la coordenada x.
        sin = Mathf.Sin(Mathf.Deg2Rad * x * 0.5f); //Para calcular la parte imaginaria de la coordenada x.
        qx = new MrQuaternion(sin, 0f, 0f, cos);

        cos = Mathf.Cos(Mathf.Deg2Rad * y * 0.5f); //Para calcular la parte real de la coordenada y.
        sin = Mathf.Sin(Mathf.Deg2Rad * y * 0.5f); //Para calcular la parte imaginaria de la coordenada y.
        qy = new MrQuaternion(0f, sin, 0f, cos);

        cos = Mathf.Cos(Mathf.Deg2Rad * z * 0.5f); //Para calcular la parte real de la coordenada z.
        sin = Mathf.Sin(Mathf.Deg2Rad * z * 0.5f); //Para calcular la parte imaginaria de la coordenada z.
        qz = new MrQuaternion(0f, 0f, sin, cos);

        q = qx * qy * qz;

        return q;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MrQuaternion Euler(Vec3 euler) //Lo que el de arriba pero con un Vec3.
    {
        return Euler(euler.x, euler.y, euler.z);
    }

    #endregion




}