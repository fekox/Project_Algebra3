using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EjerciciosVector3_6 : MonoBehaviour
{
    [SerializeField]
    private Color _color;

    [SerializeField]
    private Vector3 VectorA;

    [SerializeField]
    private Vector3 VectorB;

    void Update()
    {
        Debug.DrawLine(new Vector3(0, 0, 0), new Vector3(VectorA.x, VectorA.y, VectorA.z), color: Color.white);
        Debug.DrawLine(new Vector3(0, 0, 0), new Vector3(VectorB.x, VectorB.y, VectorB.z), color: Color.black);
    }

    void OnDrawGizmos()
    {
        Handles.Label(new Vector3(VectorA.x + 2, VectorA.y, VectorA.z), "X = " + VectorA.x);
        Handles.Label(new Vector3(VectorA.x + 2, VectorA.y - 2, VectorA.z), "Y = " + VectorA.y);
        Handles.Label(new Vector3(VectorA.x + 2, VectorA.y - 4, VectorA.z), "Z = " + VectorA.z);

        Handles.Label(new Vector3(VectorB.x - 8, VectorB.y, VectorB.z), "X = " + VectorB.x);
        Handles.Label(new Vector3(VectorB.x - 8, VectorB.y - 2, VectorB.z), "Y = " + VectorB.y);
        Handles.Label(new Vector3(VectorB.x - 8, VectorB.y - 4, VectorB.z), "Z = " + VectorB.z);

        Handles.Label(new Vector3(2, 0, 0), "X = " + 0);
        Handles.Label(new Vector3(2, -2, 0), "Y = " + 0);
        Handles.Label(new Vector3(2, -4, 0), "Z = " + 0);
    }
}
