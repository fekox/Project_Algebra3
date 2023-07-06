using System;
using System.Collections;
using System.Collections.Generic;
using CustomMath;
using System.Globalization;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine;

UnityEngine.Matrix4x4

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
        public MrMatrix(Vector4 column0, Vector4 column1, Vector4 column2, Vector4 column3)
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
        #endregion

        #region Properties
        public float this[int index]
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

        public float this[int row, int column]
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

        public static MrMatrix zero
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

        public static MrMatrix identity
        {
            get
            {
                MrMatrix m = zero;
                m.m00 = 1.0f;
                m.m11 = 1.0f;
                m.m22 = 1.0f;
                m.m33 = 1.0f;
                return m;
            }
        }

        #endregion


    }

}
