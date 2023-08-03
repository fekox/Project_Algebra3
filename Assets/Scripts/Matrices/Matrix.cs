using System;
using CustomMath;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Matrix : MonoBehaviour
{
    public struct MrMatrix
    {
        #region Variables

        public float m00;

        public float m10;

        public float m20;

        public float m30;

        public float m01;

        public float m11;

        public float m21;

        public float m31;

        public float m02;

        public float m12;

        public float m22;

        public float m32;

        public float m03;

        public float m13;

        public float m23;

        public float m33;

        #endregion

        #region Contruct
        public MrMatrix(Vector4 column0, Vector4 column1, Vector4 column2, Vector4 column3) //Crea una matrix4x4 en base a 4 vector4.
        {
            m00 = column0.x;
            m01 = column1.x;
            m02 = column2.x;
            m03 = column3.x;

            m10 = column0.y;
            m11 = column1.y;
            m12 = column2.y;
            m13 = column3.y;

            m20 = column0.z;
            m21 = column1.z;
            m22 = column2.z;
            m23 = column3.z;

            m30 = column0.w;
            m31 = column1.w;
            m32 = column2.w;
            m33 = column3.w;
        }

        public MrMatrix(float m00, float m01, float m02, float m03,
                        float m10, float m11, float m12, float m13,
                        float m20, float m21, float m22, float m23,
                        float m30, float m31, float m32, float m33) //Crea una matrix4x4 agregando componente por componete.
        {
            this.m00 = m00;
            this.m01 = m01;
            this.m02 = m02;
            this.m03 = m03;

            this.m10 = m10;
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;

            this.m20 = m20;
            this.m21 = m21;
            this.m22 = m22;
            this.m23 = m23;

            this.m30 = m30;
            this.m31 = m31;
            this.m32 = m32;
            this.m33 = m33;
        }
        
        

        #endregion

        #region Properties
        public float this[int index]//Accede a los componentes de la matrix4x4 a travez de indices en un array.
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return m00;

                    case 1:
                        return m10;

                    case 2:
                        return m20;

                    case 3:
                        return m30;

                    case 4:
                        return m01;

                    case 5:
                        return m11;

                    case 6:
                        return m21;

                    case 7:
                        return m31;

                    case 8:
                        return m02;

                    case 9:
                        return m12;

                    case 10:
                        return m22;

                    case 11:
                        return m32;

                    case 12:
                        return m03;

                    case 13:
                        return m13;

                    case 14:
                        return m23;

                    case 15:
                        return m33;

                    default:
                        throw new IndexOutOfRangeException("Index out of Range!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        m00 = value;
                        break;
                    case 1:
                        m10 = value;
                        break;
                    case 2:
                        m20 = value;
                        break;
                    case 3:
                        m30 = value;
                        break;
                    case 4:
                        m01 = value;
                        break;
                    case 5:
                        m11 = value;
                        break;
                    case 6:
                        m21 = value;
                        break;
                    case 7:
                        m31 = value;
                        break;
                    case 8:
                        m02 = value;
                        break;
                    case 9:
                        m12 = value;
                        break;
                    case 10:
                        m22 = value;
                        break;
                    case 11:
                        m32 = value;
                        break;
                    case 12:
                        m03 = value;
                        break;
                    case 13:
                        m13 = value;
                        break;
                    case 14:
                        m23 = value;
                        break;
                    case 15:
                        m33 = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Index out of Range!");
                }
            }
        }

        public float this[int row, int column]//Accede a las columnas y filas de la matrix4x4 a travez de indices en un array.
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return this[row + column * 4];
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                this[row + column * 4] = value;
            }
        }

        public static MrMatrix zero //Devuelve una matrix4x4 cuyos valores son 0.
        {
            get
            {
                return new MrMatrix()
                {
                    m00 = 0.0f,
                    m01 = 0.0f,
                    m02 = 0.0f,
                    m03 = 0.0f,
                    m10 = 0.0f,
                    m11 = 0.0f,
                    m12 = 0.0f,
                    m13 = 0.0f,
                    m20 = 0.0f,
                    m21 = 0.0f,
                    m22 = 0.0f,
                    m23 = 0.0f,
                    m30 = 0.0f,
                    m31 = 0.0f,
                    m32 = 0.0f,
                    m33 = 0.0f
                };
            }
        }

        public static MrMatrix identity //Devuelve una matrix4x4 con sus valores en 0, exepto por (m00, m11, m22, 33 = la diagonal) cuyos valores son 1.
        {
            get
            {
                MrMatrix matrix = zero;
                matrix.m00 = 1.0f;
                matrix.m11 = 1.0f;
                matrix.m22 = 1.0f;
                matrix.m33 = 1.0f;
                return matrix;
            }
        }

        public MrQuaternion rotation  //Genera una matriz 4x4 de rotaci�n en base a un quaternion.
        {
            get 
            {
                MrMatrix matrix = this;

                MrQuaternion quaternion = new MrQuaternion();

                quaternion.w = Mathf.Sqrt(Mathf.Max(0, 1 + matrix[0, 0] + matrix[1, 1] + matrix[2, 2])) / 2; //Devuelve la raiz de un n�mero que debe ser al menos 0.
                quaternion.x = Mathf.Sqrt(Mathf.Max(0, 1 + matrix[0, 0] - matrix[1, 1] - matrix[2, 2])) / 2; //Por eso hace un min entre las posiciones de las diagonales.
                quaternion.y = Mathf.Sqrt(Mathf.Max(0, 1 - matrix[0, 0] + matrix[1, 1] - matrix[2, 2])) / 2;
                quaternion.z = Mathf.Sqrt(Mathf.Max(0, 1 - matrix[0, 0] - matrix[1, 1] + matrix[2, 2])) / 2;

                quaternion.x *= Mathf.Sign(quaternion.x * (matrix[2, 1] - matrix[1, 2]));
                quaternion.y *= Mathf.Sign(quaternion.y * (matrix[0, 2] - matrix[2, 0])); //Son los valores de la matriz que se van a modificar.
                quaternion.z *= Mathf.Sign(quaternion.z * (matrix[1, 0] - matrix[0, 1]));

                return quaternion;
            }  
        }

        public Vec3 lossyScale //Devuelve la escala real del objeto. Esto es en caso de que se apliquen rotaciones y otros c�lculos, donde se pierde la escala.
        {
            get 
            {
                return new Vec3(GetColumn(0).magnitude, GetColumn(1).magnitude, GetColumn(2).magnitude);
            }    
        }

        public bool IsIdentity //Devuelve si la matrix es identity o no.
        {
            get 
            {
                return m00 == 1f && m11 == 1f && m22 == 1f && m33 == 1f &&
                       m01 == 0f && m02 == 0f && m03 == 0f &&
                       m10 == 0f && m12 == 0f && m13 == 0f &&
                       m20 == 0f && m21 == 0f && m23 == 0f &&
                       m30 == 0f && m31 == 0f && m32 == 0f;
                            
            }
        }

        public float determinant //Devuelve la determinandte de una matriz. (Determinante = es un valor escalar de la matriz)
        {
            get 
            { 
                return Determinant(this); 
            }
        }
        
        public MrMatrix transpose //Devuelve una matriz en la cual se reemplazan las columnas por las filas y viceversa.
        {
            get 
            {
                return Transpose(this);
            }
        }
        
        public MrMatrix inverse //Devuelve la matriz inversa.
        {
            get 
            {
                return Inverse(this);
            }
        }
        #endregion

        #region Function
        public static float Determinant(MrMatrix matrix)//Devuelve la determinante de una matriz.
        {
            //Teorema de Laplace.

            //El determinante de una matriz es igual a la suma de los productos (resultado de la multiplicacion) de cada elemento
            //de una fila o columna por el determinante de la fila o columna que se encuentra al lado. 

            return
            matrix[0, 3] * matrix[1, 2] * matrix[2, 1] * matrix[3, 0] - matrix[0, 2] * matrix[1, 3] * matrix[2, 1] * matrix[3, 0] -
            matrix[0, 3] * matrix[1, 1] * matrix[2, 2] * matrix[3, 0] + matrix[0, 1] * matrix[1, 3] * matrix[2, 2] * matrix[3, 0] +
            matrix[0, 2] * matrix[1, 1] * matrix[2, 3] * matrix[3, 0] - matrix[0, 1] * matrix[1, 2] * matrix[2, 3] * matrix[3, 0] -
            matrix[0, 3] * matrix[1, 2] * matrix[2, 0] * matrix[3, 1] + matrix[0, 2] * matrix[1, 3] * matrix[2, 0] * matrix[3, 1] +
            matrix[0, 3] * matrix[1, 0] * matrix[2, 2] * matrix[3, 1] - matrix[0, 0] * matrix[1, 3] * matrix[2, 2] * matrix[3, 1] -
            matrix[0, 2] * matrix[1, 0] * matrix[2, 3] * matrix[3, 1] + matrix[0, 0] * matrix[1, 2] * matrix[2, 3] * matrix[3, 1] +
            matrix[0, 3] * matrix[1, 1] * matrix[2, 0] * matrix[3, 2] - matrix[0, 1] * matrix[1, 3] * matrix[2, 0] * matrix[3, 2] -
            matrix[0, 3] * matrix[1, 0] * matrix[2, 1] * matrix[3, 2] + matrix[0, 0] * matrix[1, 3] * matrix[2, 1] * matrix[3, 2] +
            matrix[0, 1] * matrix[1, 0] * matrix[2, 3] * matrix[3, 2] - matrix[0, 0] * matrix[1, 1] * matrix[2, 3] * matrix[3, 2] -
            matrix[0, 2] * matrix[1, 1] * matrix[2, 0] * matrix[3, 3] + matrix[0, 1] * matrix[1, 2] * matrix[2, 0] * matrix[3, 3] +
            matrix[0, 2] * matrix[1, 0] * matrix[2, 1] * matrix[3, 3] - matrix[0, 0] * matrix[1, 2] * matrix[2, 1] * matrix[3, 3] -
            matrix[0, 1] * matrix[1, 0] * matrix[2, 2] * matrix[3, 3] + matrix[0, 0] * matrix[1, 1] * matrix[2, 2] * matrix[3, 3];
        }
        
        public static MrMatrix Inverse(MrMatrix matrix) //Devuelve la matriz inversa.
        {
            float detA = Determinant(matrix); //Obtengo la determinante de la matriz.
            
            if (detA == 0) //Si da 0 lo igualo a 0.
            {
                return zero;
            }

            MrMatrix aux = new MrMatrix() //Creo una matriz auxiliar en la cual guardo la determinante de cada una de esas posiciones.
            {
                //------0---------
                m00 = matrix.m11 * matrix.m22 * matrix.m33 + matrix.m12 * matrix.m23 * matrix.m31 + matrix.m13 * matrix.m21 * matrix.m32 - matrix.m11 * matrix.m23 * matrix.m32 - matrix.m12 * matrix.m21 * matrix.m33 - matrix.m13 * matrix.m22 * matrix.m31,
                m01 = matrix.m01 * matrix.m23 * matrix.m32 + matrix.m02 * matrix.m21 * matrix.m33 + matrix.m03 * matrix.m22 * matrix.m31 - matrix.m01 * matrix.m22 * matrix.m33 - matrix.m02 * matrix.m23 * matrix.m31 - matrix.m03 * matrix.m21 * matrix.m32,
                m02 = matrix.m01 * matrix.m12 * matrix.m33 + matrix.m02 * matrix.m13 * matrix.m32 + matrix.m03 * matrix.m11 * matrix.m32 - matrix.m01 * matrix.m13 * matrix.m32 - matrix.m02 * matrix.m11 * matrix.m33 - matrix.m03 * matrix.m12 * matrix.m31,
                m03 = matrix.m01 * matrix.m13 * matrix.m22 + matrix.m02 * matrix.m11 * matrix.m23 + matrix.m03 * matrix.m12 * matrix.m21 - matrix.m01 * matrix.m12 * matrix.m23 - matrix.m02 * matrix.m13 * matrix.m21 - matrix.m03 * matrix.m11 * matrix.m22,
                //-------1--------					     								    
                m10 = matrix.m10 * matrix.m23 * matrix.m32 + matrix.m12 * matrix.m20 * matrix.m33 + matrix.m13 * matrix.m22 * matrix.m30 - matrix.m10 * matrix.m22 * matrix.m33 - matrix.m12 * matrix.m23 * matrix.m30 - matrix.m13 * matrix.m20 * matrix.m32,
                m11 = matrix.m00 * matrix.m22 * matrix.m33 + matrix.m02 * matrix.m23 * matrix.m30 + matrix.m03 * matrix.m20 * matrix.m32 - matrix.m00 * matrix.m23 * matrix.m32 - matrix.m02 * matrix.m20 * matrix.m33 - matrix.m03 * matrix.m22 * matrix.m30,
                m12 = matrix.m00 * matrix.m13 * matrix.m32 + matrix.m02 * matrix.m10 * matrix.m33 + matrix.m03 * matrix.m12 * matrix.m30 - matrix.m00 * matrix.m12 * matrix.m33 - matrix.m02 * matrix.m13 * matrix.m30 - matrix.m03 * matrix.m10 * matrix.m32,
                m13 = matrix.m00 * matrix.m12 * matrix.m23 + matrix.m02 * matrix.m13 * matrix.m20 + matrix.m03 * matrix.m10 * matrix.m22 - matrix.m00 * matrix.m13 * matrix.m22 - matrix.m02 * matrix.m10 * matrix.m23 - matrix.m03 * matrix.m12 * matrix.m20,
                //-------2--------					     								    
                m20 = matrix.m10 * matrix.m21 * matrix.m33 + matrix.m11 * matrix.m23 * matrix.m30 + matrix.m13 * matrix.m20 * matrix.m31 - matrix.m10 * matrix.m23 * matrix.m31 - matrix.m11 * matrix.m20 * matrix.m33 - matrix.m13 * matrix.m31 * matrix.m30,
                m21 = matrix.m00 * matrix.m23 * matrix.m31 + matrix.m01 * matrix.m20 * matrix.m33 + matrix.m03 * matrix.m21 * matrix.m30 - matrix.m00 * matrix.m21 * matrix.m33 - matrix.m01 * matrix.m23 * matrix.m30 - matrix.m03 * matrix.m20 * matrix.m31,
                m22 = matrix.m00 * matrix.m11 * matrix.m33 + matrix.m01 * matrix.m13 * matrix.m31 + matrix.m03 * matrix.m10 * matrix.m31 - matrix.m00 * matrix.m13 * matrix.m31 - matrix.m01 * matrix.m10 * matrix.m33 - matrix.m03 * matrix.m11 * matrix.m30,
                m23 = matrix.m00 * matrix.m13 * matrix.m21 + matrix.m01 * matrix.m10 * matrix.m23 + matrix.m03 * matrix.m11 * matrix.m31 - matrix.m00 * matrix.m11 * matrix.m23 - matrix.m01 * matrix.m13 * matrix.m20 - matrix.m03 * matrix.m10 * matrix.m21,
                //------3---------					     								    
                m30 = matrix.m10 * matrix.m22 * matrix.m31 + matrix.m11 * matrix.m20 * matrix.m32 + matrix.m12 * matrix.m21 * matrix.m30 - matrix.m00 * matrix.m21 * matrix.m32 - matrix.m11 * matrix.m22 * matrix.m30 - matrix.m12 * matrix.m20 * matrix.m31,
                m31 = matrix.m00 * matrix.m21 * matrix.m32 + matrix.m01 * matrix.m22 * matrix.m30 + matrix.m02 * matrix.m20 * matrix.m31 - matrix.m00 * matrix.m22 * matrix.m31 - matrix.m01 * matrix.m20 * matrix.m32 - matrix.m02 * matrix.m21 * matrix.m30,
                m32 = matrix.m00 * matrix.m12 * matrix.m31 + matrix.m01 * matrix.m10 * matrix.m32 + matrix.m02 * matrix.m11 * matrix.m30 - matrix.m00 * matrix.m11 * matrix.m32 - matrix.m01 * matrix.m12 * matrix.m30 - matrix.m02 * matrix.m10 * matrix.m31,
                m33 = matrix.m00 * matrix.m11 * matrix.m22 + matrix.m01 * matrix.m12 * matrix.m20 + matrix.m02 * matrix.m10 * matrix.m21 - matrix.m00 * matrix.m12 * matrix.m21 - matrix.m01 * matrix.m10 * matrix.m22 - matrix.m02 * matrix.m11 * matrix.m20
            };

            MrMatrix ret = new MrMatrix() //Divido las posiciones por el determinante para invertirlos.
            {
                m00 = aux.m00 / detA,
                m01 = aux.m01 / detA,
                m02 = aux.m02 / detA,
                m03 = aux.m03 / detA,
                m10 = aux.m10 / detA,
                m11 = aux.m11 / detA,
                m12 = aux.m12 / detA,
                m13 = aux.m13 / detA,
                m20 = aux.m20 / detA,
                m21 = aux.m21 / detA,
                m22 = aux.m22 / detA,
                m23 = aux.m23 / detA,
                m30 = aux.m30 / detA,
                m31 = aux.m31 / detA,
                m32 = aux.m32 / detA,
                m33 = aux.m33 / detA

            };
            
            return ret; //Devuelvo la matriz invertida.
        }

        public static MrMatrix Rotate(MrQuaternion quaternion) //Genera una matriz4x4 de rotaci�n en base a un quaternion.
        {
            //Obtengo la rotacion con un quaternion.
            //Se hace asi para evitar numeros negativos.
            double num1 = quaternion.x * 2f;
            double num2 = quaternion.y * 2f;
            double num3 = quaternion.z * 2f;

            double num4 = quaternion.x * num1;
            double num5 = quaternion.y * num2;
            double num6 = quaternion.z * num3;
            double num7 = quaternion.x * num2;
            double num8 = quaternion.x * num3;
            double num9 = quaternion.y * num3;

            double num10 = quaternion.w * num1;
            double num11 = quaternion.w * num2;
            double num12 = quaternion.w * num3;
            
            //Genero la rotacion de la matrix.
            MrMatrix matrix;
            matrix.m00 = (float)(1.0 - num5 + num6);
            matrix.m10 = (float)(num7 + num12);
            matrix.m20 = (float)(num8 - num11);
            matrix.m30 = 0.0f;
            matrix.m01 = (float)(num7 - num12);
            matrix.m11 = (float)(1.0 - num4 + num6);
            matrix.m21 = (float)(num9 + num10);
            matrix.m31 = 0.0f;
            matrix.m02 = (float)(num8 + num11);
            matrix.m12 = (float)(num9 - num10);
            matrix.m22 = (float)(1.0 - num4 + num5);
            matrix.m32 = 0.0f;
            matrix.m03 = 0.0f;
            matrix.m13 = 0.0f;
            matrix.m23 = 0.0f;
            matrix.m33 = 1f;
            
            return matrix;//Devuelvo la matriz.
        }

        public static MrMatrix Scale(Vec3 vector3) //Escala la matrix utilizando un vector3.
        {
            //Se le asignas los valores X, Y, Z del vector a la diagonal (m00, m11, m22).
            //La varianle m33 se le asigna 1 ya que tambien es parte de la diagonal.
            MrMatrix matrix;

            matrix.m00 = vector3.x;
            matrix.m01 = 0.0f;
            matrix.m02 = 0.0f;
            matrix.m03 = 0.0f;
            matrix.m10 = 0.0f;

            matrix.m11 = vector3.y;
            matrix.m12 = 0.0f;
            matrix.m13 = 0.0f;
            matrix.m20 = 0.0f;
            matrix.m21 = 0.0f;

            matrix.m22 = vector3.z;
            matrix.m23 = 0.0f;
            matrix.m30 = 0.0f;
            matrix.m31 = 0.0f;
            matrix.m32 = 0.0f;
            matrix.m33 = 1f;

            return matrix; //Devuelvo la matrix escalada.
        }

        public static MrMatrix Traslate(Vec3 vector3) //Crea una matriz de translacion a partir de un vetor3.
        {
            //Se le asignan los valores XYZ a los valores del vector a las variables (m03, m13, m23).
            //A la diagonal se la setea en 1.
            MrMatrix matrix;

            matrix.m00 = 1f;
            matrix.m01 = 0.0f;
            matrix.m02 = 0.0f;

            matrix.m03 = vector3.x;
            matrix.m10 = 0.0f;
            matrix.m11 = 1f;
            matrix.m12 = 0.0f;

            matrix.m13 = vector3.y;
            matrix.m20 = 0.0f;
            matrix.m21 = 0.0f;
            matrix.m22 = 1f;

            matrix.m23 = vector3.z;
            matrix.m30 = 0.0f;
            matrix.m31 = 0.0f;
            matrix.m32 = 0.0f;
            matrix.m33 = 1f;

            return matrix;
        }

        public static MrMatrix Transpose(MrMatrix matrix)//Devuelve una matriz en la cual se reemplazan las columnas por las filas y viceversa.
        {
            return new MrMatrix()
            {
                m01 = matrix.m10,
                m02 = matrix.m20,
                m03 = matrix.m30,
                m10 = matrix.m01,
                m12 = matrix.m21,
                m13 = matrix.m31,
                m20 = matrix.m02,
                m21 = matrix.m12,
                m23 = matrix.m32,
                m30 = matrix.m03,
                m31 = matrix.m13,
                m32 = matrix.m23,
            };
        }

        public static MrMatrix TRS(Vec3 pos, MrQuaternion q, Vec3 s) //Crea una matrix de trasformacion con las funciones Traslate, Roatate, Scale.
        {
            //Multiplica la posicion, la rotacion y la escala para obtener la matrix TRS.
            return (Traslate(pos) * Rotate(q) * Scale(s));
        }

        public Vector4 GetColumn(int index)//Devuelve una columna utilizando los indices del array.
        {
            return new Vector4(this[0, index], this[1, index], this[2, index], this[3, index]);
        }

        public Vec3 GetPosition()//Devuelve la posicion en forma de vector3.
        {
            return new Vec3(m03, m13, m23);
        }

        public Vector4 GetRow(int index) //Devuelve la fila a travez de los indices en un array.
        {
            switch (index)
            {
                case 0:
                    return new Vector4(m00, m01, m02, m03);

                case 1:
                    return new Vector4(m10, m11, m12, m13);

                case 2:
                    return new Vector4(m20, m21, m22, m23);

                case 3:
                    return new Vector4(m30, m31, m32, m33);

                default:
                    throw new IndexOutOfRangeException("Index out of Range!");
            }
        }

        public Vec3 MultiplyPoint(Vec3 point) //Multiplica una matriz4x4 por un vector3.
        {
            Vec3 vector3;

            vector3.x = (float)((double)m00 * (double)point.x + (double)m01 * (double)point.y + (double)m02 * (double)point.z) + m03;
            vector3.y = (float)((double)m10 * (double)point.x + (double)m11 * (double)point.y + (double)m12 * (double)point.z) + m13;
            vector3.z = (float)((double)m20 * (double)point.x + (double)m21 * (double)point.y + (double)m22 * (double)point.z) + m23;

            float num = 1f / ((float)((double)m30 * (double)point.x + (double)m31 * (double)point.y + (double)m32 * (double)point.z) + m33);

            vector3.x *= num;
            vector3.y *= num;
            vector3.z *= num;

            return vector3;
        }

        public Vec3 MultiplyPoint3x4(Vec3 point) //Multiplica una matriz3x4 con un vector3.
        {
            Vec3 vector3;

            vector3.x = (float)((double)m00 * (double)point.x + (double)m01 * (double)point.y + (double)m02 * (double)point.z) + m03;
            vector3.y = (float)((double)m10 * (double)point.x + (double)m11 * (double)point.y + (double)m12 * (double)point.z) + m13;
            vector3.z = (float)((double)m20 * (double)point.x + (double)m21 * (double)point.y + (double)m22 * (double)point.z) + m23;

            return vector3; //Representacion de una matrix en un espacion tridimencional.
        }

        public Vec3 MultiplyPointVector(Vec3 vector) //Multiplica un vector3 por una matrix.
        {
            Vec3 vector3;

            vector3.x = (float)((double)m00 * (double)vector.x + (double)m01 * (double)vector.y + (double)m02 * (double)vector.z);
            vector3.y = (float)((double)m10 * (double)vector.x + (double)m11 * (double)vector.y + (double)m12 * (double)vector.z);
            vector3.z = (float)((double)m20 * (double)vector.x + (double)m21 * (double)vector.y + (double)m22 * (double)vector.z);

            return vector3; //Representacion de una matrix en un espacion tridimencional.
        }

        public void SetColumn(int index, Vector4 column)  //Setea una columna con los indices de un array.
        {
            this[0, index] = column.x;
            this[1, index] = column.y;
            this[2, index] = column.z;
            this[3, index] = column.w;
        }

        public void SetRow(int index, Vector4 row) //Setea una fila con los indices de un array.
        {
            this[index, 0] = row.x;
            this[index, 1] = row.y;
            this[index, 2] = row.z;
            this[index, 3] = row.w;
        }

        public void SetTRS(Vec3 pos, MrQuaternion q, Vec3 s) //Setea la TRS.
        {
            this = TRS(pos, q, s);
        }

        public bool ValidTRS() //Esta funci�n sirve para verificar si la matriz de transformaci�n es realmente v�lida.

        {
            if (lossyScale == Vec3.Zero) 
            {
                return false;
            }

            else if (m00 == double.NaN && m10 == double.NaN && m20 == double.NaN && m30 == double.NaN &&
                     m01 == double.NaN && m11 == double.NaN && m21 == double.NaN && m31 == double.NaN &&
                     m02 == double.NaN && m12 == double.NaN && m22 == double.NaN && m32 == double.NaN &&
                     m03 == double.NaN && m13 == double.NaN && m23 == double.NaN && m33 == double.NaN) 
            {
                return false;
            }
            
            else if (rotation.x > 1 && rotation.x < -1 && rotation.y > 1 && rotation.y < -1 && rotation.z > 1 && rotation.z < -1 && rotation.w > 1 && rotation.w < -1) 
            {
                return false;

            }

            else 
            {
                return true;
            }
        }

        #endregion

        #region Operators
        public static Vector4 operator *(MrMatrix lhs, Vector4 vector) //Multiplica una matriz por un vector4.
        {

            Vector4 ret;

            ret.x = (float)((double)lhs.m00 * (double)vector.x + (double)lhs.m01 * (double)vector.y + (double)lhs.m02 * (double)vector.z + (double)lhs.m03 * (double)vector.w);
            ret.y = (float)((double)lhs.m10 * (double)vector.x + (double)lhs.m11 * (double)vector.y + (double)lhs.m12 * (double)vector.z + (double)lhs.m13 * (double)vector.w);
            ret.z = (float)((double)lhs.m20 * (double)vector.x + (double)lhs.m21 * (double)vector.y + (double)lhs.m22 * (double)vector.z + (double)lhs.m23 * (double)vector.w);
            ret.w = (float)((double)lhs.m30 * (double)vector.x + (double)lhs.m31 * (double)vector.y + (double)lhs.m32 * (double)vector.z + (double)lhs.m33 * (double)vector.w);

            return ret;
        }

        public static MrMatrix operator *(MrMatrix lhs, MrMatrix rhs) //Multiplica una matriz por otro matriz.
        {
            MrMatrix ret = zero;

            for (int i = 0; i < 4; i++)
            {
                ret.SetColumn(i, lhs * rhs.GetColumn(i));
            }

            return ret;
        }

        public static bool operator ==(MrMatrix lhs, MrMatrix rhs) //Devuelve si una matriz es igual a otra.
        {
            return lhs.GetColumn(0) == rhs.GetColumn(0) && lhs.GetColumn(1) == rhs.GetColumn(1) && lhs.GetColumn(2) == rhs.GetColumn(2) && lhs.GetColumn(3) == rhs.GetColumn(3);
        }

        public static bool operator !=(MrMatrix lhs, MrMatrix rhs) //Devuelve si una matriz no es igual a otra. 
        {
            return !(lhs == rhs);
        }
        #endregion
    }
}