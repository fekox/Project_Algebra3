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
    public static MrQuaternion Identity //Returns a quaternuin that corresponds to ¨not rotated¨.
    {                                   //The object is perfectly aligned with the world or parent axes.
        get 
        { 
            return new MrQuaternion(0f, 0f, 0f, 1f); 
        }
    }

    public bool IsIdentity //Returns true if whether the given quaternion is equals to the quaternion identity.
    {
        get 
        {
            return x == 0f && y == 0f && z == 0f && w == 1f;
        }
    }

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
            switch(index) 
            {
                case 0: x = value; break;

                case 1: y = value; break;

                case 2: z = value; break;

                case 3: w = value; break;

                default: throw new IndexOutOfRangeException("Invalid Quaternion index!");
            }
        }
    }

    public MrQuaternion Zero //Return a quaternion whose values are (0, 0, 0, 0).
    { 
        get 
        {
            return new MrQuaternion(0f, 0f, 0f, 0f);
        } 
    }
    #endregion


}