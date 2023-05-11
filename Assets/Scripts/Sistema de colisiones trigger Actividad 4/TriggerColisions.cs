using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using static Activity_Planes;
using System.Security.Cryptography;
using System;

public class TriggerColisions : MonoBehaviour
{
    [Header("References")]
    private List<MrPlane> planes;
    private List<Vec3> checkPoints;
    public List<Vec3> pointsInside;
    public Vec3 nearestPoint;

   struct Line 
   {
        public Vec3 origin;
        public Vec3 direction;

        public Line(Vec3 origin, Vec3 direction) 
        {
            this.origin = origin;
            this.direction = direction;
        }
   }

    private void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh; //Inicializo las mesh.

        planes = new List<MrPlane>(); //Inicializo los planos.
        
        checkPoints = new List<Vec3>(); //Inicializo los puntos a chequear.
        
        pointsInside = new List<Vec3>(); //Inicializo los que estaran dentro de los objetos.

        for (int i = 0; i < mesh.vertices.Length; i += 3) //Setteo los vertices de cada plano.
        {
            Vec3 vertA = new Vec3(mesh.vertices[mesh.GetIndices(0)[i]]);
            Vec3 vertB = new Vec3(mesh.vertices[mesh.GetIndices(0)[i + 1]]);
            Vec3 vertC = new Vec3(mesh.vertices[mesh.GetIndices(0)[i + 2]]);
            planes.Add(new MrPlane(vertA, vertB, vertC));
        }

        for (int i = 0; i < planes.Count; i++)  //Setteo la normal de la mesh y el normal and psoition de los planos. 
        {
            Vec3 norm = new Vec3(mesh.normals[i]);

            planes[i].SetNormalAndPosition(norm, planes[i].normal * planes[i].distance);
        }
    }

    private int GetMaxGridSize(int num, float scale, float nearestPoint) 
    {
        return (int)(nearestPoint) + (3 + (int)scale - 1) * num;
    }

    private void CheckPoints() //Chequea todos los puntos de la grilla.
    {
        checkPoints.Clear();

        int maxGridX = GetMaxGridSize((int)nearestPoint.x, transform.localScale.x, 1);
        int maxGridY = GetMaxGridSize((int)nearestPoint.y, transform.localScale.y, 1);
        int maxGridZ = GetMaxGridSize((int)nearestPoint.z, transform.localScale.z, 1);

        int minGridX = GetMaxGridSize((int)nearestPoint.x, transform.localScale.x, -1);
        int minGridY = GetMaxGridSize((int)nearestPoint.y, transform.localScale.y, -1);
        int minGridZ = GetMaxGridSize((int)nearestPoint.z, transform.localScale.z, -1);

        int gridMaxSize = DrawGrid.maxPoints - 1;

        maxGridX = Mathf.Clamp(maxGridX, 0, gridMaxSize);
        maxGridY = Mathf.Clamp(maxGridY, 0, gridMaxSize);
        maxGridZ = Mathf.Clamp(maxGridZ, 0, gridMaxSize);

        minGridX = Mathf.Clamp(minGridX, 0, gridMaxSize);
        minGridY = Mathf.Clamp(minGridY, 0, gridMaxSize);
        minGridZ = Mathf.Clamp(minGridZ, 0, gridMaxSize);

        for (int i = minGridX; i < maxGridX; i++)
        {
            for (int j = minGridY; j < maxGridY; j++)
            {
                for (int k = minGridZ; k < maxGridZ; k++)
                {
                    checkPoints.Add(DrawGrid.grid[i, j, k]);
                }
            }
        }
    }
}
