using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using static Activity_Planes;
using System.Security.Cryptography;

public class TriggerColisions : MonoBehaviour
{
    [SerializeField] private GameObject plane;

    //First Plane
    static Vec3 vecA1 = new Vec3(0, 1, 0);
    static Vec3 vecA2 = new Vec3(1, 2, 0);
    static Vec3 vecA3 = new Vec3(-1, 2, 0);

    private MrPlane Plane1 = new MrPlane(vecA1, vecA2, vecA3);

    void OnDrawGizmosSelected()
    {
        //First Plane
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(vecA1, 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(vecA3, vecA1);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(vecA2, vecA1);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(vecA2, 0.1f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(vecA2, vecA3);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(vecA3, 0.1f);
    }

}
