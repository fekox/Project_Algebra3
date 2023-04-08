using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CustomMath
{
    [Serializable]

    public struct Vec3 : IEquatable<Vec3>
    {
        #region Variables
        public float x;
        public float y;
        public float z;
        public float sqrMagnitude { get { return (x * x + y * y + z * z); } } //Devuelve la longitud del vector al cuadrado.
        public Vec3 normalized { get { return new Vec3(x / magnitude, y / magnitude, z / magnitude); } } //Devuelve el vector normalizado. 
        public float magnitude { get { return Mathf.Pow(x, 2) + Mathf.Pow(y, 2) + Mathf.Pow(z, 2); } } //Devuelve la longitud del vector.
        #endregion

        #region constants
        public const float epsilon = 1e-05f;
        #endregion

        #region Default Values
        public static Vec3 Zero { get { return new Vec3(0.0f, 0.0f, 0.0f); } } 
        public static Vec3 One { get { return new Vec3(1.0f, 1.0f, 1.0f); } }
        public static Vec3 Forward { get { return new Vec3(0.0f, 0.0f, 1.0f); } }
        public static Vec3 Back { get { return new Vec3(0.0f, 0.0f, -1.0f); } }
        public static Vec3 Right { get { return new Vec3(1.0f, 0.0f, 0.0f); } }
        public static Vec3 Left { get { return new Vec3(-1.0f, 0.0f, 0.0f); } }
        public static Vec3 Up { get { return new Vec3(0.0f, 1.0f, 0.0f); } }
        public static Vec3 Down { get { return new Vec3(0.0f, -1.0f, 0.0f); } }
        public static Vec3 PositiveInfinity { get { return new Vec3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity); } }
        public static Vec3 NegativeInfinity { get { return new Vec3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity); } }
        #endregion                                                                                                                                                                               

        #region Constructors
        public Vec3(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0.0f;
        }

        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vec3(Vec3 v3)
        {
            this.x = v3.x;
            this.y = v3.y;
            this.z = v3.z;
        }

        public Vec3(Vector3 v3)
        {
            this.x = v3.x;
            this.y = v3.y;
            this.z = v3.z;
        }

        public Vec3(Vector2 v2)
        {
            this.x = v2.x;
            this.y = v2.y;
            this.z = 0.0f;
        }
        #endregion

        #region Operators
        public static bool operator ==(Vec3 left, Vec3 right)
        {
            float diff_x = left.x - right.x;
            float diff_y = left.y - right.y;
            float diff_z = left.z - right.z;
            float sqrmag = diff_x * diff_x + diff_y * diff_y + diff_z * diff_z;
            return sqrmag < epsilon * epsilon;
        }
        public static bool operator !=(Vec3 left, Vec3 right)
        {
            return !(left == right);
        }

        public static Vec3 operator +(Vec3 leftV3, Vec3 rightV3) //Suma dos vectores.
        {
            return new Vec3(leftV3.x + rightV3.x, leftV3.y + rightV3.y, leftV3.z + rightV3.z);
        }

        public static Vec3 operator -(Vec3 leftV3, Vec3 rightV3) //Resta dos vectores.
        {
            //https://docs.unity3d.com/ScriptReference/Vector3-operator_subtract.html

            return new Vec3(leftV3.x - rightV3.x, leftV3.y - rightV3.y, leftV3.z - rightV3.z);
        }

        public static Vec3 operator -(Vec3 v3) //Vuelve negativo un vector.
        {
            return new Vec3(-v3.x, -v3.y, -v3.z);
        }

        public static Vec3 operator *(Vec3 v3, float scalar) //Multiplica un vector por un numero.
        {
            //https://docs.unity3d.com/ScriptReference/Vector3-operator_multiply.html

            return new Vec3(v3.x * scalar, v3.y * scalar, v3.z * scalar);
        }
        public static Vec3 operator *(float scalar, Vec3 v3) //Multiplica un vector por un numero.
        {
            return new Vec3(scalar * v3.x, scalar * v3.y, scalar * v3.z);
        }
        public static Vec3 operator /(Vec3 v3, float scalar) //Divide un vector por un numero.
        {
            //https://docs.unity3d.com/ScriptReference/Vector3-operator_divide.html

            return new Vec3(v3.x / scalar, v3.y / scalar, v3.z / scalar);
        }

        public static implicit operator Vector3(Vec3 v3) //Devuelve un vector 3.
        {
            return new Vector3(v3.x, v3.y, v3.z);
        }

        public static implicit operator Vector2(Vec3 v2) //Devuelve un vector 2.
        {
            return new Vector2(v2.x, v2.y);
        }
        #endregion

        #region Functions
        public override string ToString()
        {
            return "X = " + x.ToString() + "   Y = " + y.ToString() + "   Z = " + z.ToString();
        }
        public static float Angle(Vec3 from, Vec3 to) //Calcula el angulo entre dos vectores.
        {
            //https://docs.unity3d.com/ScriptReference/Vector3.Angle.html

            return Mathf.Acos(Mathf.Clamp(Dot(from.normalized, to.normalized), -1f, 1f)) * 180 / MathF.PI;
        }
        public static Vec3 ClampMagnitude(Vec3 vector, float maxLength) //Devuelve una copia del vector con su magnitud sujeta a maxima longitud.
        {
            //https://docs.unity3d.com/ScriptReference/Vector3.ClampMagnitude.html

            if (vector.magnitude > maxLength) { return vector.normalized * maxLength; }

            else { return vector; }
        }
        public static float Magnitude(Vec3 vector) //Devuelve la magnitud del vector.
        {
            //https://docs.unity3d.com/ScriptReference/Vector3-magnitude.html

            float magnitude = Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2) + Mathf.Pow(vector.z, 2);

            return magnitude;
        }
        public static Vec3 Cross(Vec3 a, Vec3 b) //Hace el producto cruz entre dos vectores.
        {
            //https://docs.unity3d.com/ScriptReference/Vector3.Cross.html

            Vec3 c = new Vec3();

            c.x = ((a.y * b.z) - (a.z * b.y));
            c.y = ((a.z * b.x) - (a.x * b.z));
            c.z = ((a.x * b.y) - (a.y * b.x));

            return c;
        }
        public static float Distance(Vec3 a, Vec3 b) //Calcula la distancia entre dos vectores.
        {
            float distance = 0;

            const float squaredNumber = 2;

           //https://docs.unity3d.com/es/530/ScriptReference/Mathf.Pow.html

            distance = Mathf.Pow(a.x - b.x, squaredNumber) + Mathf.Pow(a.y - b.y, squaredNumber) + Mathf.Pow(a.z - b.z, squaredNumber);

            //https://docs.unity3d.com/es/530/ScriptReference/Mathf.Sqrt.html

            return Mathf.Sqrt(distance); ;
        }
        public static float Dot(Vec3 a, Vec3 b) //Devuelve un vector escalado.
        {
            //https://docs.unity3d.com/ScriptReference/Vector3.Dot.html

            return a.x * b.x + a.y * b.y + a.z * b.z;
        }
        public static Vec3 Lerp(Vec3 a, Vec3 b, float t) //Interpola linealmente entre dos puntos (Calcular el valor aproximado de una magnitud entre dos puntos).
        {
            //https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html

            Vec3 direction = (b - a);
            if (t < 0) t = 0;
            if (t > 1) t = 1;
            return a + (t * direction);
        }
        public static Vec3 LerpUnclamped(Vec3 a, Vec3 b, float t) //Interpola linealmente entre dos vectores (Calcular el valor aproximado de una magnitud entre dos vectores).
        {
            //https://docs.unity3d.com/ScriptReference/Vector3.LerpUnclamped.html

            Vec3 direction = (b - a);
            return a + (t * direction);
        }
        public static Vec3 Max(Vec3 a, Vec3 b) //Devuelve los valores mas altos entre dos vectores.
        {
            //https://docs.unity3d.com/ScriptReference/Vector3.Max.html

            Vec3 vector = new Vec3();

            if (a.x > b.x) { vector.x = a.x; }
            else {vector.x = b.x; }

            if (a.y > b.y) { vector.y = a.y; }
            else { vector.y = b.y; }

            if (a.z > b.z) { vector.z = a.z; }
            else { vector.z = b.z; }

            return vector;
        }
        public static Vec3 Min(Vec3 a, Vec3 b) // devuelve los valores mas bajos entre dos vectores.
        {
            //https://docs.unity3d.com/ScriptReference/Vector3.Min.html

            Vec3 vector = new Vec3();

            if (a.x < b.x) vector.x = a.x;
            else vector.x = b.x;

            if (a.y < b.y) vector.y = a.y;
            else vector.y = b.y;

            if (a.z < b.z) vector.z = a.z;
            else vector.z = b.z;

            return vector;
        }
        public static float SqrMagnitude(Vec3 vector) //Devuelve la longitud del vector al cuadrado.
        {
            //https://docs.unity3d.com/ScriptReference/Vector3-sqrMagnitude.html

            float sqrMagnitude = vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;

            return sqrMagnitude;
        }
        public static Vec3 Project(Vec3 vector, Vec3 onNormal) //Proyecta un vector sobre otro.
        {
            //https://docs.unity3d.com/ScriptReference/Vector3.Project.html

            float sqrMag = Dot(onNormal, onNormal);
            if (sqrMag < epsilon)
            {
                return Zero;
            }
            else
            {
                float dot = Dot(vector, onNormal);
                return onNormal * dot / sqrMag;
            }
        }
        public static Vec3 Reflect(Vec3 inDirection, Vec3 inNormal) //Refleja un vector del plano definido por una normal.
        {
            //https://docs.unity3d.com/ScriptReference/Vector3.Reflect.html

            return inDirection - 2f * (Dot(inDirection, inNormal)) * inNormal;
        }
        public void Set(float newX, float newY, float newZ) //Setea los valores de un vector.
        {
            x = newX;
            y = newY;
            z = newZ;
        }
        public void Scale(Vec3 scale) //Scala un vector. 
        {
            //https://docs.unity3d.com/ScriptReference/Vector3.Scale.html

            x = x * scale.x;
            y = y * scale.y;
            z = z * scale.z;
        }
        public void Normalize() //Hace que este vector tenga una magnitud de 1.
        {
            //https://docs.unity3d.com/ScriptReference/Vector3-normalized.html
            //https://www.khanacademy.org/computing/computer-programming/programming-natural-simulations/programming-vectors/a/vector-magnitude-normalization#:~:text=To%20normalize%20a%20vector%2C%20therefore,the%20unit%20vector%20readily%20accessible.

        float magnitude = Magnitude(this);
            
            x = x / magnitude;
            y = y / magnitude;
            z = z / magnitude;
        }
        #endregion

        #region Internals
        public override bool Equals(object other)
        {
            if (!(other is Vec3)) return false;
            return Equals((Vec3)other);
        }

        public bool Equals(Vec3 other)
        {
            return x == other.x && y == other.y && z == other.z;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2);
        }
        #endregion
    }
}