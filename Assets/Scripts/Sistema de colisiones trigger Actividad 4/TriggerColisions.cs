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

        int maxGridX = GetMaxGridSize(1, transform.localScale.x, nearestPoint.x);
        int maxGridY = GetMaxGridSize(1, transform.localScale.y, nearestPoint.y);
        int maxGridZ = GetMaxGridSize(1, transform.localScale.z, nearestPoint.z);

        int minGridX = GetMaxGridSize(-1, transform.localScale.x, nearestPoint.x);
        int minGridY = GetMaxGridSize(-1, transform.localScale.y, nearestPoint.y);
        int minGridZ = GetMaxGridSize(-1, transform.localScale.z, nearestPoint.z);

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

    bool PointPlaneCollision(MrPlane plane, Line line, out Vec3 point) 
    {
        point = Vec3.Zero;

        float denom = Vec3.Dot(plane.normal, line.direction);
        
        if(Mathf.Abs(denom) > Vec3.epsilon) 
        {
            float interpolate = Vec3.Dot((plane.normal * plane.distance - line.origin), plane.normal) / denom;

            if(interpolate >= Vec3.epsilon)
            {
                point = line.origin + line.direction * interpolate;
                
                return true;
            }
        }

        return true;
    }

    private bool IsCorrectPlane(MrPlane plane, Vec3 point) 
    {
        float vertX1 = plane.verA.x;
        float vertX2 = plane.verB.x;
        float vertX3 = plane.verC.x;

        float vertY1 = plane.verA.y;
        float vertY2 = plane.verB.y;
        float vertY3 = plane.verC.y;

        float vertZ1 = plane.verA.z;
        float vertZ2 = plane.verB.z;
        float vertZ3 = plane.verC.z;

        float triangleAreaOrigin = Mathf.Abs((vertX2 - vertX1) * (vertY3 - vertY1) - (vertX3 - vertX1) * (vertY2 - vertY1)); //Area del triangulo.

        //Area de los 3 triangulos hechos con el punto y las esquinas.
        float triangleArea1 = Mathf.Abs((vertX1 - point.x) * (vertY2 - point.y) - (vertX2 - point.x) * (vertY1 - point.y));
        float triangleArea2 = Mathf.Abs((vertX2 - point.x) * (vertY3 - point.y) - (vertX3 - point.x) * (vertY2 - point.y));
        float triangleArea3 = Mathf.Abs((vertX3 - point.x) * (vertY1 - point.y) - (vertX1 - point.x) * (vertY3 - point.y));

        // Si la suma del area de los 3 triangulos es igual a la del original estamos adentro
        return Math.Abs(triangleArea1 + triangleArea2 + triangleArea3 - triangleAreaOrigin) < Vec3.epsilon;
    }
}
