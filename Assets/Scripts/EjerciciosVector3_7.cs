using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EjerciciosVector3_7 : MonoBehaviour
{
    [SerializeField]
    private Color _color;

    [SerializeField]
    private Vector3 VectorA;

    [SerializeField]
    private Vector3 VectorB;

    private Vector3 VectorC;

    private void Start()
    {
        VectorC = Vector3.Reflect(VectorB, VectorB.normalized);
    }

    void Update()
    {
        Debug.DrawLine(new Vector3(0, 0, 0), new Vector3(VectorA.x, VectorA.y, VectorA.z), color: Color.white);
        Debug.DrawLine(new Vector3(0, 0, 0), new Vector3(VectorB.x, VectorB.y, VectorB.z), color: Color.black);
        Debug.DrawLine(new Vector3(0, 0, 0), new Vector3(VectorC.x, VectorC.y, VectorC.z), _color);
    }

    void OnDrawGizmos()
    {
        Handles.Label(new Vector3(VectorA.x + 2, VectorA.y, VectorA.z), "X = " + VectorA.x);
        Handles.Label(new Vector3(VectorA.x + 2, VectorA.y - 2, VectorA.z), "Y = " + VectorA.y);
        Handles.Label(new Vector3(VectorA.x + 2, VectorA.y - 4, VectorA.z), "Z = " + VectorA.z);

        Handles.Label(new Vector3(VectorB.x - 8, VectorB.y, VectorB.z), "X = " + VectorB.x);
        Handles.Label(new Vector3(VectorB.x - 8, VectorB.y - 2, VectorB.z), "Y = " + VectorB.y);
        Handles.Label(new Vector3(VectorB.x - 8, VectorB.y - 4, VectorB.z), "Z = " + VectorB.z);

        Handles.Label(new Vector3(VectorC.x + 2, VectorC.y, VectorC.z), "X = " + VectorC.x);
        Handles.Label(new Vector3(VectorC.x + 2, VectorC.y - 2, VectorC.z), "Y = " + VectorC.y);
        Handles.Label(new Vector3(VectorC.x + 2, VectorC.y - 4, VectorC.z), "Z = " + VectorC.z);

        Handles.Label(new Vector3(2, 0, 0), "X = " + 0);
        Handles.Label(new Vector3(2, -2, 0), "Y = " + 0);
        Handles.Label(new Vector3(2, -4, 0), "Z = " + 0);
    }
}
