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

        /// <summary>
        /// Devuelve la longitud del vector al cuadrado.
        /// </summary>
        public float sqrMagnitude { get { return (x * x + y * y + z * z); } }

        /// <summary>
        /// Devuelve el vector normalizado. 
        /// 
        /// Devuelve un nuevo vector con la X dividida por la magnitud, Y dividida por la magnitud, Z dividida por la magnitud.
        /// </summary>
        public Vec3 normalized { get { return new Vec3(x / magnitude, y / magnitude, z / magnitude); } }

        /// <summary>
        /// Devuelve la longitud del vector.
        /// 
        /// Calcula la raiz cuadrada de X al cuadrado, Y al cuadrado, Z al cuadrado. 
        /// </summary>
        public float magnitude { get { return MathF.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2) + Mathf.Pow(z, 2)); } }
        #endregion

        #region constants
        public const float epsilon = 1e-05f;
        #endregion

        #region Default Values
        /// <summary>
        /// Devuelve un vec3(0,0,0);
        /// </summary>
        public static Vec3 Zero { get { return new Vec3(0.0f, 0.0f, 0.0f); } } 

        /// <summary>
        /// Devuelve un vec3(1,1,1);
        /// </summary>
        public static Vec3 One { get { return new Vec3(1.0f, 1.0f, 1.0f); } }

        /// <summary>
        /// Devuelve un vec3(0,0,1) para que mire al forward.
        /// </summary>
        public static Vec3 Forward { get { return new Vec3(0.0f, 0.0f, 1.0f); } }

        /// <summary>
        /// Devuelve un vec3(0,0,-1) para que mira al back.
        /// </summary>
        public static Vec3 Back { get { return new Vec3(0.0f, 0.0f, -1.0f); } }

        /// <summary>
        /// Devuelve un vec3(1,0,0) para que mire al right.
        /// </summary>
        public static Vec3 Right { get { return new Vec3(1.0f, 0.0f, 0.0f); } }

        /// <summary>
        /// Devuelve un vec3(-1,0,0) para que mire a left.
        /// </summary>
        public static Vec3 Left { get { return new Vec3(-1.0f, 0.0f, 0.0f); } }

        /// <summary>
        /// Devuelve un vec3(0,1,0) para que mire a up.
        /// </summary>
        public static Vec3 Up { get { return new Vec3(0.0f, 1.0f, 0.0f); } }

        /// <summary>
        /// Devuelve un vec3(0,-1,0) para que mire a down.
        /// </summary>
        public static Vec3 Down { get { return new Vec3(0.0f, -1.0f, 0.0f); } }

        /// <summary>
        /// Devuelve un vec3 siempre positivo. 
        /// </summary>
        public static Vec3 PositiveInfinity { get { return new Vec3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity); } }

        /// <summary>
        /// Devuelve un vec3 siempre negativo.
        /// </summary>
        public static Vec3 NegativeInfinity { get { return new Vec3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity); } }
        #endregion                                                                                                                                                                               

        #region Constructors

        /// <summary>
        /// Crea un vec3 con los valores X e Y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vec3(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0.0f;
        }

        /// <summary>
        /// Crea un vec3 con los valores X,Y,Z.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Crea un vec3 con un vec3.
        /// </summary>
        /// <param name="v3"></param>
        public Vec3(Vec3 v3)
        {
            this.x = v3.x;
            this.y = v3.y;
            this.z = v3.z;
        }

        /// <summary>
        /// Crea un vec3 con un Vector3.
        /// </summary>
        /// <param name="v3"></param>
        public Vec3(Vector3 v3)
        {
            this.x = v3.x;
            this.y = v3.y;
            this.z = v3.z;
        }

        /// <summary>
        /// Crea un vec3 con un Vector2.
        /// </summary>
        /// <param name="v2"></param>
        public Vec3(Vector2 v2)
        {
            this.x = v2.x;
            this.y = v2.y;
            this.z = 0.0f;
        }
        #endregion

        #region Operators

        /// <summary>
        /// Devuelve si un vec3 es igual a otro.
        /// 
        /// Los floats no son confiables cuando igualamos por 
        /// lo tanto hacemos esa operacion para quedarnos 
        /// completamente fuera de duda que ambos vec3 son
        /// iguales.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Vec3 left, Vec3 right)
        {
            float diff_x = left.x - right.x;
            float diff_y = left.y - right.y;
            float diff_z = left.z - right.z;
            float sqrmag = diff_x * diff_x + diff_y * diff_y + diff_z * diff_z;
            return sqrmag < epsilon * epsilon;
        }

        /// <summary>
        /// Devuelve si ambos vec3 son iguales.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Vec3 left, Vec3 right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Suma dos vec3.
        /// </summary>
        /// <param name="leftV3"></param>
        /// <param name="rightV3"></param>
        /// <returns></returns>
        public static Vec3 operator +(Vec3 leftV3, Vec3 rightV3)
        {
            return new Vec3(leftV3.x + rightV3.x, leftV3.y + rightV3.y, leftV3.z + rightV3.z);
        }

        /// <summary>
        /// Resta dos vec3.
        /// </summary>
        /// <param name="leftV3"></param>
        /// <param name="rightV3"></param>
        /// <returns></returns>
        public static Vec3 operator -(Vec3 leftV3, Vec3 rightV3)
        {
            //https://docs.unity3d.com/ScriptReference/Vector3-operator_subtract.html

            return new Vec3(leftV3.x - rightV3.x, leftV3.y - rightV3.y, leftV3.z - rightV3.z);
        }

        /// <summary>
        /// Vuelve negatico un vec3.
        /// </summary>
        /// <param name="v3"></param>
        /// <returns></returns>
        public static Vec3 operator -(Vec3 v3)
        {
            return new Vec3(-v3.x, -v3.y, -v3.z);
        }

        /// <summary>
        /// Multiplica un vec3 por un numero.
        /// </summary>
        /// <param name="v3"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Vec3 operator *(Vec3 v3, float scalar)
        {
            //https://docs.unity3d.com/ScriptReference/Vector3-operator_multiply.html

            return new Vec3(v3.x * scalar, v3.y * scalar, v3.z * scalar);
        }

        /// <summary>
        /// Multiplica un numero por un vec3.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="v3"></param>
        /// <returns></returns>
        public static Vec3 operator *(float scalar, Vec3 v3)
        {
            return new Vec3(scalar * v3.x, scalar * v3.y, scalar * v3.z);
        }

        /// <summary>
        /// Divide un vector por un numero.
        /// </summary>
        /// <param name="v3"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Vec3 operator /(Vec3 v3, float scalar)
        {
            //https://docs.unity3d.com/ScriptReference/Vector3-operator_divide.html

            return new Vec3(v3.x / scalar, v3.y / scalar, v3.z / scalar);
        }

        /// <summary>
        /// Devuelve un vec3.
        /// </summary>
        /// <param name="v3"></param>
        public static implicit operator Vector3(Vec3 v3)
        {
            return new Vector3(v3.x, v3.y, v3.z);
        }

        /// <summary>
        /// Devuelve un vec2.
        /// </summary>
        /// <param name="v2"></param>
        public static implicit operator Vector2(Vec3 v2)
        {
            return new Vector2(v2.x, v2.y);
        }
        #endregion

        #region Functions

        /// <summary>
        /// Te devuelve los valores transformados en string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "X = " + x.ToString() + "   Y = " + y.ToString() + "   Z = " + z.ToString();
        }

        /// <summary>
        /// Calcula el angulo en grados entre dos vectores.
        /// 
        /// From es el vector desde donde se calcula el angulo.
        /// To es el vector hacia el cual se calcula el angulo.
        /// 
        /// Te devuelve un float que estara en el rango de 0 
        /// y 180, siempre sera positivo.
        /// 
        /// Primero realiza el canculo para obtener el angulo y 
        /// luego convierte el angulo de radianes a grados con 
        /// "* 180 / MathF.PI".
        /// </summary>
        /// <returns></returns>
        public static float Angle(Vec3 from, Vec3 to)
        {
            //https://docs.unity3d.com/ScriptReference/Vector3.Angle.html

            return Mathf.Acos(Mathf.Clamp(Dot(from.normalized, to.normalized), -1f, 1f)) * 180 / MathF.PI;
        }

        /// <summary>
        /// Devuelve una copia del vector con su magnitud sujeta a 
        /// una maxima longitud (Limita la magnitud del vector a un 
        /// valor especifico).
        /// 
        /// Si la magnitud del vector es mayor al valor maximo se 
        /// normaliza y se multiplica por el valor maximo.
        /// 
        /// En caso contrario devuelve el vector tal y como esta.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static Vec3 ClampMagnitude(Vec3 vector, float maxLength) 
        {
            //https://docs.unity3d.com/ScriptReference/Vector3.ClampMagnitude.html

            if (vector.magnitude > maxLength) 
            { 
                return vector.normalized * maxLength; 
            }

            else 
            { 
                return vector; 
            }
        }

        /// <summary>
        /// Devuelve la magnitud del vector.
        /// 
        /// La magnitud del vector es igual a la suma del valor X,Y,Z al cuadrado.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static float Magnitude(Vec3 vector)
        {
            //https://docs.unity3d.com/ScriptReference/Vector3-magnitude.html

            float magnitude = Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2) + Mathf.Pow(vector.z, 2);

            return magnitude;
        }

        /// <summary>
        /// Hace el producto cruz entre dos vectores.
        /// 
        /// Devuelve el resultado del producto cruz.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vec3 Cross(Vec3 a, Vec3 b) 
        {
            //https://docs.unity3d.com/ScriptReference/Vector3.Cross.html

            Vec3 c = new Vec3();

            c.x = ((a.y * b.z) - (a.z * b.y));
            c.y = ((a.z * b.x) - (a.x * b.z));
            c.z = ((a.x * b.y) - (a.y * b.x));

            return c;
        }

        /// <summary>
        /// Calcula la distancia entre dos vectores y 
        /// la devuelve en un valor de tipo float.
        /// 
        /// Utilizo pitagoras para sacar la distancia y
        /// finalmente devuelvo la raiz cuadrada del
        /// resultado.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static float Distance(Vec3 a, Vec3 b)
        {
            float distance = 0;

            const float squaredNumber = 2;

           //https://docs.unity3d.com/es/530/ScriptReference/Mathf.Pow.html

            distance = Mathf.Pow(a.x - b.x, squaredNumber) + Mathf.Pow(a.y - b.y, squaredNumber) + Mathf.Pow(a.z - b.z, squaredNumber);

            //https://docs.unity3d.com/es/530/ScriptReference/Mathf.Sqrt.html

            return Mathf.Sqrt(distance); ;
        }

        /// <summary>
        /// Devuelve el producto punto de dos vectores.
        /// 
        /// Es la suma de la multiplicacion de los valores
        /// de dos vectores: X1 * X2 + Y1 * Y2 + Z1 * Z2.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static float Dot(Vec3 a, Vec3 b)
        {
            //https://docs.unity3d.com/ScriptReference/Vector3.Dot.html

            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        /// <summary>
        /// Interpola linealmente entre dos puntos basados 
        /// en un valor de interpolacion (Calcular el 
        /// valor aproximado de una magnitud entre 
        /// dos puntos).
        /// 
        /// a es el vector de inicio.
        /// b es el vector de destino.
        /// t es el valor de interpolacion que varia entre 0 y 1.
        /// 
        /// La funcion devuelve un vec3 que representa el punto
        /// interpolado entre a y b segun el valor de t.
        /// 
        /// Primero restamos el vector a del vector b para
        /// obtener el vector de direccion que apunta desde
        /// a hacia b.
        /// 
        /// Luego se multiplica el vector direcion por t 
        /// por el valor de interpolacion y luego se le 
        /// suma el vector a, lo que nos da la distancia
        /// entre a y b determinado por t.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Vec3 Lerp(Vec3 a, Vec3 b, float t) 
        {
            //https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html

            Vec3 direction = (b - a);
            if (t < 0) t = 0;
            if (t > 1) t = 1;
            return a + (direction * t);
        }

        /// <summary>
        /// Es similar a Lerp pero no esta limitado entre 0 y 1.
        /// 
        /// Interpola linealmente entre dos puntos basados 
        /// en un valor de interpolacion (Calcular el 
        /// valor aproximado de una magnitud entre 
        /// dos puntos).
        /// 
        /// a es el vector de inicio.
        /// b es el vector de destino.
        /// t es el valor de interpolación (puede tener cualquier valor).
        /// 
        /// La formula que se aplica es la misma manera que en la funcion lerp.
        /// 
        /// Primero restamos el vector a del vector b para
        /// obtener el vector de direccion que apunta desde
        /// a hacia b.
        /// 
        /// Luego se multiplica el vector direcion por t 
        /// por el valor de interpolacion y luego se le 
        /// suma el vector a, lo que nos da la distancia
        /// entre a y b determinado por t.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Vec3 LerpUnclamped(Vec3 a, Vec3 b, float t) 
        {
            //https://docs.unity3d.com/ScriptReference/Vector3.LerpUnclamped.html

            Vec3 direction = (b - a);
            return a + (direction * t);
        }

        /// <summary>
        /// Devuelve un nuevo vector con los valores mas grandes
        /// de los vectores a y b.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vec3 Max(Vec3 a, Vec3 b)
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

        /// <summary>
        /// Devuelve un nuevo vector con los valores mas pequeños
        /// de los vectores a y b.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vec3 Min(Vec3 a, Vec3 b) 
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

        /// <summary>
        /// Calcula la magnitud de un vector3 al cuadrado.
        /// 
        /// Devuelve un float sqrMagnitude que es la suma de 
        /// los valores X,Y,Z al cuadrado.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static float SqrMagnitude(Vec3 vector) 
        {
            //https://docs.unity3d.com/ScriptReference/Vector3-sqrMagnitude.html

            float sqrMagnitude = vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;

            return sqrMagnitude;
        }

        /// <summary>
        /// Projecta un vector sobre otro.
        /// 
        /// vector es el vector que deseas proyectar.
        /// onNormal es el vector en cuya direcion deseas projectar el primer vector.
        /// 
        /// Guardo en sqrMag el valor de onNormal al cuadrado, si es menor a epsilon 
        /// devuelve un vector 0.
        /// 
        /// Si no, calculo el producto punto de vector y onNormal, finalmente
        /// retorno un vector3 proyectado que es el factor escalar (dot / sqrMag)
        /// multiplicado por el vector onNormal.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="onNormal"></param>
        /// <returns></returns>
        public static Vec3 Project(Vec3 vector, Vec3 onNormal) 
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

        /// <summary>
        /// Devuelve un vector3 que es la reflexion de un vector
        /// respecto a una superficie normal dada.
        /// 
        /// inDirection representa la direccion del objeto que se reflejara.
        /// inNormal es el vector normal de la superficie donde se realizara la reflexion.
        /// 
        /// El vector reflejado se calcula restando el doble del producto punto de ambos
        /// vectores multiplicado por inNormal.
        /// 
        /// El -2 se utiliza para tener el valor presiso del vector reflejado. (Direccion opuesta).
        /// </summary>
        /// <param name="inDirection"></param>
        /// <param name="inNormal"></param>
        /// <returns></returns>
        public static Vec3 Reflect(Vec3 inDirection, Vec3 inNormal)
        {
            //https://docs.unity3d.com/ScriptReference/Vector3.Reflect.html

            return inDirection - 2 * (Dot(inDirection, inNormal)) * inNormal;
        }

        /// <summary>
        /// Setea los valores de un vector3 X,Y,Z.
        /// </summary>
        /// <param name="newX"></param>
        /// <param name="newY"></param>
        /// <param name="newZ"></param>
        public void Set(float newX, float newY, float newZ) 
        {
            x = newX;
            y = newY;
            z = newZ;
        }

        /// <summary>
        /// Scala un vector3.
        /// </summary>
        /// <param name="scale"></param>
        public void Scale(Vec3 scale)  
        {
            //https://docs.unity3d.com/ScriptReference/Vector3.Scale.html

            x = x * scale.x;
            y = y * scale.y;
            z = z * scale.z;
        }

        /// <summary>
        /// Hace que este vector tenga una magnitud de 1.
        /// 
        /// Para normalizarlo dividimos cada uno de sus 
        /// componentes por la magnitud.
        /// </summary>
        public void Normalize()
        {
            //https://docs.unity3d.com/ScriptReference/Vector3-normalized.html
            //https://www.khanacademy.org/computing/computer-programming/programming-natural-simulations/programming-vectors/a/vector-magnitude-normalization#:~:text=To%20normalize%20a%20vector%2C%20therefore,the%20unit%20vector%20readily%20accessible.

            float magnitude = Magnitude(this);
            
            x = x / magnitude;
            y = y / magnitude;
            z = z / magnitude;
        }

        /// <summary>
        /// Hace que este vector tenga una magnitud de 1.
        /// 
        /// Para normalizarlo dividimos cada uno de sus 
        /// componentes por la magnitud.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vec3 Normalize(Vec3 value)
        {
            float num = Magnitude(value);
            if (num > 1E-05f)
            {
                return value / num;
            }

            return Zero;
        }
        #endregion

        #region Internals
        /// <summary>
        /// Se utiliza para comparar dos vectores y determinar si 
        /// son iguales.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            if (!(other is Vec3)) return false;
            return Equals((Vec3)other);
        }

        /// <summary>
        /// Se utiliza para comparar dos vectores y determinar si 
        /// son iguales.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Vec3 other)
        {
            return x == other.x && y == other.y && z == other.z;
        }

        /// <summary>
        /// Proporciona una implementacion espacifica que genera
        /// un codigo hash basado en los componentes X,Y,Z del
        /// vector.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2);
        }
        #endregion
    }
}