using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;
using UnityEngine;
using CustomMath;

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

    private static readonly MrQuaternion identityQuaternion = new MrQuaternion(0f, 0f, 0f, 1f);
}