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
using UnityEngine.UI;
using System.Data;
using Unity.VisualScripting.Dependencies.Sqlite;

public struct MrQuaternion 
{
    #region Variables
    public const float kEpsilon = 1E-06f;

    public float x; //Componente X, no se toca si no sabes.
    public float y; //Componente Y, no se toca si no sabes.
    public float z; //Componente Z, no se toca si na sabes.
    public float w; //Componente W, no se toca si no sabes.

    #endregion

    #region Contructs
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MrQuaternion(float x, float y, float z, float w) //Crea un quaternion con un float para cada componente.
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MrQuaternion(Vec3 vec3Part, float rotPart) //Crea un quaternion con un vector3 para los componentes X, Y, Z, y un float para el componente W.
    {
        this.x = vec3Part.x;
        this.y = vec3Part.y;
        this.z = vec3Part.z;
        this.w = rotPart;
    }
    #endregion

    #region Properties

    public float this[int index] //Accede a los componentes del Quaternion a traves de indicies en un array.
    { 
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

    public static MrQuaternion identity //Devuelve un quaternion ¨not rotated¨, no rotado.
    {                                   //El objecto esta perfectamente alineado con el mundo o los ejes principales.
        get 
        { 
            return new MrQuaternion(0f, 0f, 0f, 1f);
        }
    }

    public Vec3 eulerAngles //Devuelve o establece la representación del ángulo de Euler de la rotación.
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return FromQuaternionToEuler(this);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            this = FromEulerToQuaternion(value);
        }
    }

    public MrQuaternion normalized //Devuelve un quaternion normalizes.
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return Normalize(this);
        }
    }

    public float magnitude //Devuelve la magnitud.
    {
        get 
        {
            return Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2) + Mathf.Pow(z, 2) + Mathf.Pow(w, 2)); //Raíz de la suma del cuadrado de cada componente.
        }
    }

    public MrQuaternion zero //Devuelve un quaternion cuyos valores son (0, 0, 0, 0).
    { 
        get 
        {
            return new MrQuaternion(0f, 0f, 0f, 0f);
        } 
    }
    #endregion

    #region Functions

    public static Vec3 FromQuaternionToEuler(MrQuaternion rotation)
    {
        float sqw = rotation.w * rotation.w;
        float sqx = rotation.x * rotation.x;
        float sqy = rotation.y * rotation.y;
        float sqz = rotation.z * rotation.z;
        float unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
        float test = rotation.x * rotation.w - rotation.y * rotation.z;
        Vec3 v;

        if (test > 0.4999f * unit)   // singularity at north pole
        {
            v.y = 2f * Mathf.Atan2(rotation.y, rotation.x);
            v.x = Mathf.PI / 2;
            v.z = 0;
            return NormalizeAngles(v * Mathf.Rad2Deg);
        }
        if (test < -0.4999f * unit)  // singularity at south pole
        {
            v.y = -2f * Mathf.Atan2(rotation.y, rotation.x);
            v.x = -Mathf.PI / 2;
            v.z = 0;
            return NormalizeAngles(v * Mathf.Rad2Deg);
        }

        MrQuaternion q = new MrQuaternion(rotation.w, rotation.z, rotation.x, rotation.y);
        v.y = Mathf.Atan2(2f * q.x * q.w + 2f * q.y * q.z, 1 - 2f * (q.z * q.z + q.w * q.w));     // Yaw
        v.x = Mathf.Asin(2f * (q.x * q.z - q.w * q.y));                                           // Pitch
        v.z = Mathf.Atan2(2f * q.x * q.y + 2f * q.z * q.w, 1 - 2f * (q.y * q.y + q.z * q.z));      // Roll
        return NormalizeAngles(v * Mathf.Rad2Deg);
    }

    static MrQuaternion FromEulerToQuaternion(Vec3 euler)
    {
        float sinAngle = 0.0f;
        float cosAngle = 0.0f;

        MrQuaternion qx = identity;
        MrQuaternion qy = identity;
        MrQuaternion qz = identity;
        MrQuaternion r = identity;

        sinAngle = Mathf.Sin(Mathf.Deg2Rad * euler.z * 0.5f);
        cosAngle = Mathf.Cos(Mathf.Deg2Rad * euler.z * 0.5f);
        qz.Set(0, 0, sinAngle, cosAngle);

        sinAngle = Mathf.Sin(Mathf.Deg2Rad * euler.x * 0.5f);
        cosAngle = Mathf.Cos(Mathf.Deg2Rad * euler.x * 0.5f);
        qx.Set(sinAngle, 0, 0, cosAngle);

        sinAngle = Mathf.Sin(Mathf.Deg2Rad * euler.y * 0.5f);
        cosAngle = Mathf.Cos(Mathf.Deg2Rad * euler.y * 0.5f);
        qy.Set(0, sinAngle, 0, cosAngle);

        r = qy * qx * qz;

        return r;
    }
    private static Vec3 NormalizeAngles(Vec3 angles)//Devuelve los angulos X, Y, Z normalizados.
    {
        angles.x = NormalizeAngle(angles.x);
        angles.y = NormalizeAngle(angles.y);
        angles.z = NormalizeAngle(angles.z);
        return angles;
    }

    private static float NormalizeAngle(float angle)//Devuelve un angulo normalizado.
    {
        while (angle > 360)
            angle -= 360;
        while (angle < 0)
            angle += 360;
        return angle;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MrQuaternion Normalize(MrQuaternion q)
    {
        float num = Mathf.Sqrt(Dot(q, q)); //Obtengo el resultado de la raiz cuadrada del producto punto del quaternion.
        
        if (num < Mathf.Epsilon) //Si el resultado es menor que epsilon. (Un pequeño valor de punto flotante).
        {
            return identity; //Devuelve MrQuaternion(0f, 0f, 0f, 1f). 
        }

        return new MrQuaternion(q.x / num, q.y / num, q.z / num, q.w / num); //En caso contrario, se devuelve un cuaternión dividido por el resultado obtenido anteriormente.
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Normalize()//Devuelve un quaternion normalizado.
    {
        this = Normalize(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Angle(MrQuaternion a, MrQuaternion b)//Devuelve el angulo entre dos Quaternions.
    {
        if (a.magnitude == 0 || b.magnitude == 0)
        {               
            return 0; //Si la magnitud de ambos da 0 devuelvo 0.
        }

        return (float)((double)Mathf.Acos(Mathf.Abs(Dot(a, b)) * Mathf.Rad2Deg / (a.magnitude * b.magnitude)));//Pasa el resultado a grados y lo returnea.
    }

    public static MrQuaternion AngleAxis(float angle, Vec3 axis)//Genera una rotacion en el quaternion usando radianes.
    {
        axis.Normalize();
        axis *= Mathf.Sin(angle * Mathf.Deg2Rad * 0.5f);
        return new MrQuaternion(axis.x, axis.y, axis.z, Mathf.Cos(angle * Mathf.Deg2Rad * 0.5f));
    }

    public static MrQuaternion AxisAngle(Vec3 axis, float angle)//Genera una rotacion en el quaternion usando grados.
    {
        return AngleAxis(Mathf.Rad2Deg * angle, axis); 
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Dot(MrQuaternion a, MrQuaternion b)//Devuelve el producto punto de dos quaternions.
    {
        return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MrQuaternion Euler(float x, float y, float z)//Retorna un quaternion con sus coordenadas x, y, z rotadas.
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
    public static MrQuaternion Euler(Vec3 euler)//Lo que el de arriba pero con un Vec3.
    {
        return Euler(euler.x, euler.y, euler.z);
    }

    public static MrQuaternion FromToRotation(Vec3 fromDirection, Vec3 toDirection)//Devuelve una rotación que rota de fromDirection a toDirection.
    {
        Vec3 axis = Vec3.Cross(fromDirection, toDirection);
        float angle = Vec3.Angle(fromDirection, toDirection);
        return AngleAxis(angle, axis.normalized);
    }

    public static MrQuaternion Inverse(MrQuaternion rotation)//Devuelve el quaternion con la rotacion invertida.
    {
        return new MrQuaternion(-rotation.x, -rotation.y, -rotation.z, rotation.w);
    }

    public static MrQuaternion Lerp(MrQuaternion a, MrQuaternion b, float time)//Interpola desde A a B.
    {
        if (time < 0f) 
        {
            time = 0f;
        }

        if (time > 1f) 
        {
            time = 1f;
        }

        return LerpUnclamped(a, b, time); //Time va a ir desde 0 a 1. Siendo 0 la posicion de A y 1 la posicion de B.
    }

    public static MrQuaternion LerpUnclamped(MrQuaternion a, MrQuaternion b, float time)//Interpola entre las rotaciones “a” y “b” según la
                                                                                         //variable “t” que no va a estar clampeada (valga
                                                                                         //laredundancia) y normaliza el resultado.

    {
        MrQuaternion result = identity;

        float timeLeft = 1f - time;

        //Si el producto punto es mayor a 0 para ver cual
        //es el camino mas corto para lerpear y dependiendo
        //de eso se hace una suma o una resta para la
        //fórmula de interpolación lineal de “a” a “b”.

        if (Dot(a, b) >= 0f) 
        {
            result.x = (a.x * timeLeft) + (time * b.x);
            result.y = (a.y * timeLeft) + (time * b.y);
            result.z = (a.z * timeLeft) + (time * b.z);
            result.w = (a.w * timeLeft) + (time * b.w);
        }

        else 
        {
            result.x = (a.x * timeLeft) - (time * b.x);
            result.y = (a.y * timeLeft) - (time * b.y);
            result.z = (a.z * timeLeft) - (time * b.z);
            result.w = (a.w * timeLeft) - (time * b.w);
        }

        result.Normalize();

        return result;
    }

    public static MrQuaternion LookRotation(Vec3 forward)//Transforma un vector director en una rotación que tenga su eje z alineado con “forward”.
    {
        return LookRotation(forward, Vec3.Up);
    }

    public static MrQuaternion LookRotation(Vec3 forward, Vec3 up)//Transforma un vector director en una rotación que tenga su eje z alineado con “forward”.
    {
        forward = Vec3.Normalize(forward);
        Vec3 right = Vec3.Normalize(Vec3.Cross(up, forward));
        up = Vec3.Cross(forward, right);

        float m00 = right.x; float m01 = right.y; float m02 = right.z;
        float m10 = up.x; float m11 = up.y; float m12 = up.z;
        float m20 = forward.x; float m21 = forward.y; float m22 = forward.z;

        float diagonals = m00 + m11 + m22;
        var quaternion = new MrQuaternion();
        
        if (diagonals > 0f)
        {
            float num = Mathf.Sqrt(diagonals + 1f);
            quaternion.w = num * 0.5f;
            num = 0.5f / num;
            quaternion.x = (m12 - m21) * num;
            quaternion.y = (m20 - m02) * num;
            quaternion.z = (m01 - m10) * num;
            return quaternion;
        }
        
        if (m00 >= m11 && m00 >= m22)
        {
            float num = Mathf.Sqrt(1f + m00 - m11 - m22);
            float num4 = 0.5f / num;
            quaternion.x = 0.5f * num;
            quaternion.y = (m01 + m10) * num4;
            quaternion.z = (m02 + m20) * num4;
            quaternion.w = (m12 - m21) * num4;
            return quaternion;
        }
        
        if (m11 > m22)
        {
            float num = Mathf.Sqrt(1f + m11 - m00 - m22);
            float num3 = 0.5f / num;
            quaternion.x = (m10 + m01) * num3;
            quaternion.y = 0.5f * num;
            quaternion.z = (m21 + m12) * num3;
            quaternion.w = (m20 - m02) * num3;
            return quaternion;
        }

        float num5 = Mathf.Sqrt(1f + m22 - m00 - m11);
        float num2 = 0.5f / num5;
        quaternion.x = (m20 + m02) * num2;
        quaternion.y = (m21 + m12) * num2;
        quaternion.z = 0.5f * num5;
        quaternion.w = (m01 - m10) * num2;

        return quaternion;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MrQuaternion RotateTowards(MrQuaternion from, MrQuaternion to, float maxDegreesDelta)//Es mover de "from" a "to", la cantidad de grados especificada.
    {
        float num = Angle(from, to);
        
        if (num == 0f)
        {
            return to;
        }

        return SlerpUnclamped(from, to, Mathf.Min(1f, maxDegreesDelta / num));
    }

    public static MrQuaternion Slerp(MrQuaternion a, MrQuaternion b, float t)//Devuelve un quaternion interpolado entre a y b.
    {                                                                         //"t" es el ratio de interpolacion.
        if(t < 0f) 
        {
            t = 0f;
        }

        if(t > 1f) 
        {
            t = 1f;
        }

        return SlerpUnclamped(a, b, t);
    }

    public static MrQuaternion SlerpUnclamped(MrQuaternion a, MrQuaternion b, float t)//Interpola esfericamente entre a y b por t.
    {                                                                                 //"t" es el ratio de interpolacion.
        //En este caso, si time no está en el rango 0-1, va a pasarse, en la misma linea (formada por a y b).
        MrQuaternion ret = identity;
        
        float dot = Dot(a, b);

        if (dot < 0)//Verifica hacia donde tiene que ir
        {
            dot = -dot;
            b = -b;
            b.w = -b.w;
        }

        float t1, t2;

        if (dot < 0.95)
        {
            float angle = Mathf.Acos(dot);
            float sinAgle = Mathf.Sin(angle);
            float inverse = 1 / sinAgle;
            t1 = Mathf.Sin((1 - t) * angle) * inverse;
            t2 = Mathf.Sin(t * angle) * inverse;
            ret = new MrQuaternion(a.x * t1 + b.x * t2, a.y * t1 + b.y * t2, a.z * t1 + a.z * t2, a.w * t1 + b.w * t2);
        }

        else
        {
            ret = Lerp(a, b, t);
        }

        return ret;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set(float newX, float newY, float newZ, float newW)//Setea los componetes X, Y, Z, W de un cuaternion existente.
    {
        x = newX;
        y = newY;
        z = newZ;
        w = newW;
    }

    public void SetFromToRotation(Vec3 fromDirection, Vec3 toDirection)//Crea una rotacion que gira desde "fromDirection" a "toDirection".
    {
        this = FromToRotation(fromDirection, toDirection);
    }

    public void SetLookRotation(Vec3 view)//Crea una rotacion con la direccion hacia delanate seteada.
    {
        this = LookRotation(view);
    }

    public void SetLookRotation(Vec3 view, Vec3 up)//Crea una rotacion con las direcciones hacia delanate y hacia arriba seteadas.
    {
        this = LookRotation(view, up);
    }

    public void ToAngleAxis(out float angle, out Vec3 axis)//Convierte una rotacion en la representacion de un angulo.
    {
        ToAxisAngle(out axis, out angle);

        angle *= Mathf.Rad2Deg;
    }

    public void ToAxisAngle(out Vec3 axis, out float angle)//Convierte una rotacion en la representacion de un angulo.
    {
        Normalize();

        angle = 2.0f * Mathf.Acos(w);

        float mag = Mathf.Sqrt(1.0f - w * w);

        if (mag > 0.0001f)
        {
            axis = new Vec3(x, y, z) / mag;
        }

        else
        {
            // si el angulo es 0 se pasa un eje arbitrario
            axis = new Vec3(1, 0, 0);
        }
    }

    public override string ToString()//Devuelve los datos del Quaternion en forma de string.
    {
        return string.Format("({0:F1}, {1:F1}, {2:F1}, {3:F1})", x, y, z, w);
    }

    public string ToString(string format)//Devuelve los datos del Quaternion en forma de string.
    {
        return string.Format("({0}, {1}, {2}, {3})", x.ToString(format), y.ToString(format), z.ToString(format), w.ToString(format));
    }
    #endregion

    #region Operators
    public static MrQuaternion operator +(MrQuaternion v1, MrQuaternion v2)//Suma componente a componente.
    {
        return new MrQuaternion(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z, v1.w + v2.w);
    }
    public static MrQuaternion operator -(MrQuaternion v1, MrQuaternion v2)//Resta componente a componente.
    {
        return new MrQuaternion(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z, v1.w - v2.w);
    }
    public static MrQuaternion operator -(MrQuaternion v1)//Quaternion con sus componentes en negativo.
    {
        return new MrQuaternion(-v1.x, -v1.y, -v1.z, v1.w);
    }

    public static MrQuaternion operator *(MrQuaternion lhs, MrQuaternion rhs)//Multiplica los componentes de dos quaterniones.
    {
        return new MrQuaternion(
            lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y,
            lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z,
            lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x,
            lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
    }

    public static MrQuaternion operator *(MrQuaternion v1, float v2)//Multiplicación componente a componente
    {
        return new MrQuaternion(v1.x * v2, v1.y * v2, v1.z * v2, v1.w * v2);
    }

    public static bool operator ==(MrQuaternion v1, MrQuaternion v2)//Devuelve si un quaternion es igual al otro.
    {
        return (v1.x == v2.x && v1.y == v2.y && v1.z == v2.z && v1.w == v2.w);
    }

    public static bool operator !=(MrQuaternion v1, MrQuaternion v2)//Devuelve si un quaternion es diferente al otro.
    {
        return (v1.x != v2.x || v1.y != v2.y || v1.z != v2.z || v1.w != v2.w);
    }
    #endregion
}