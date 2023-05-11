using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using static Activity_Planes;
using System;
using System.Linq;

public class TriggerColisions : MonoBehaviour
{
    [Header("References")]
    private List<MrPlane> planes; //Lista donde guardo los planos.

    private List<Vec3> checkPoints; //Lista donde guardo los puntos que se chequean.
    
    public List<Vec3> pointsInside; //Lista donde guardo los puntos dentro del obj.
    
    public Vec3 nearestPoint; //Punto mas cercano de la grilla.

   struct Line //Struc de la recta.
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

        for (int i = 0; i < mesh.GetIndices(0).Length; i += 3) //Setteo los vertices de cada plano.
        {
            Vec3 vertA = new Vec3(mesh.vertices[mesh.GetIndices(0)[i]]);
            Vec3 vertB = new Vec3(mesh.vertices[mesh.GetIndices(0)[i + 1]]);
            Vec3 vertC = new Vec3(mesh.vertices[mesh.GetIndices(0)[i + 2]]);
            planes.Add(new MrPlane(vertA, vertB, vertC)); //Los agrego a la lista de planos.
        }

        for (int i = 0; i < planes.Count; i++)  //Setteo la normal de la mesh y el normal and psoition de los planos. 
        {
            Vec3 norm = new Vec3(mesh.normals[i]);

            planes[i].SetNormalAndPosition(norm, planes[i].normal * planes[i].distance);
        }
    }

    private void Update()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh; //Le setteo el componente a la mesh.
        planes.Clear();// limpio la lista de planos.

        for (int i = 0;i < mesh.GetIndices(0).Length; i+= 3)  //Setteo todos los vertices de cada plano. (Cada cara esta conformada por dos triangulos).
        {
            Vec3 VerticeA = new Vec3((transform.TransformPoint(mesh.vertices[mesh.GetIndices(0)[i]])));

            Vec3 VerticeB = new Vec3((transform.TransformPoint(mesh.vertices[mesh.GetIndices(0)[i + 1]])));

            Vec3 VerticeC = new Vec3((transform.TransformPoint(mesh.vertices[mesh.GetIndices(0)[i + 2]])));

            var plane = new MrPlane(VerticeA, VerticeB, VerticeC); //Creo los planos en base a los vertices setteados.

            plane.normal *= -1; //Paso la normal a negativo.
            
            planes.Add(plane); //Agrego el plano a la lista del plano.
        }

        for(int i = 0; i < planes.Count; i++) //Flipeo los planos para que miren en la direccion correcta.
        {
            planes[i].Flip();
        }

        SetNearestPoint(); //Obtengo y setteo el punto mas cercano de la grilla al obj.
        CheckPoints(); //Chequeo todos los puntos de la grilla.
        CounterPointsOnTheObj(); //Cuento cuantos puntos hay dentro del obj.

        Debug.Log("Points: " + pointsInside.Count); //Para saber cuantos puntos hay dentro de cada obj.
    }

    private int GetMaxGridSize(float nearestPoint, float scale, int number) //Retorno el tamaño maximo de la grilla.
    {
        return (int)(nearestPoint) + (3 + (int)scale - 1) * number;
    }

    private void CheckPoints() //Chequea todos los puntos de la grilla.
    {
        checkPoints.Clear();

        int maxGridX = GetMaxGridSize(nearestPoint.x, transform.localScale.x, 1);
        int maxGridY = GetMaxGridSize(nearestPoint.y, transform.localScale.y, 1);
        int maxGridZ = GetMaxGridSize(nearestPoint.z, transform.localScale.z, 1);

        int minGridX = GetMaxGridSize(nearestPoint.x, transform.localScale.x, -1);
        int minGridY = GetMaxGridSize(nearestPoint.y, transform.localScale.y, -1);
        int minGridZ = GetMaxGridSize(nearestPoint.z, transform.localScale.z, -1);

        var gridMaxSize = DrawGrid.maxPoints - 1;

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

    bool LinePlaneCollision(MrPlane plane, Line line, out Vec3 point) //Algoritmo de colision recta con plano.
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

    private bool IsCorrectPlane(MrPlane plane, Vec3 point) //Booleano paraa detectar si el punto se encuentra en el plano correcto. (Cara del obj).
    {
        float vertX1 = plane.verA.x;
        float vertX2 = plane.verB.x;
        float vertX3 = plane.verC.x;

        float vertY1 = plane.verA.y;
        float vertY2 = plane.verB.y;
        float vertY3 = plane.verC.y;

        float triangleAreaOrigin = Mathf.Abs((vertX2 - vertX1) * (vertY3 - vertY1) - (vertX3 - vertX1) * (vertY2 - vertY1)); //Area del triangulo.

        //Area de los 3 triangulos hechos con el punto y las esquinas.
        float triangleArea1 = Mathf.Abs((vertX1 - point.x) * (vertY2 - point.y) - (vertX2 - point.x) * (vertY1 - point.y));
        float triangleArea2 = Mathf.Abs((vertX2 - point.x) * (vertY3 - point.y) - (vertX3 - point.x) * (vertY2 - point.y));
        float triangleArea3 = Mathf.Abs((vertX3 - point.x) * (vertY1 - point.y) - (vertX1 - point.x) * (vertY3 - point.y));

        // Si la suma del area de los 3 triangulos es igual a la del original estamos adentro
        return Math.Abs(triangleArea1 + triangleArea2 + triangleArea3 - triangleAreaOrigin) < Vec3.epsilon;
    }

    void CounterPointsOnTheObj() //Cuento cuantos puntos hay dentro del obj.
    {
        pointsInside.Clear();

        foreach (var point in checkPoints) 
        {
            Line line = new Line(point, Vec3.Forward * 10f);
            var counter = 0;

            foreach (var plane in planes) 
            {
                if (LinePlaneCollision(plane, line, out Vec3 interpolate))
                {
                    if (IsCorrectPlane(plane, interpolate)) 
                    {
                        counter++;
                    }
                }
            }

            if (counter % 2 == 1) 
            {
                pointsInside.Add(point);
            }
        }
    }

    int GetNearestPositionValue(float position) //Obtengo el punto mas sercano de la grilla al obj. 
    {
        var post = position / DrawGrid.delta;

        float xPost = post - (int)post > 0.5f ? post + 1.0f : post;

        xPost = Mathf.Clamp(xPost, 0, DrawGrid.maxPoints - 1);

        return (int)xPost;
    }

    void SetNearestPoint() //Setteo el punto mas sercano de la grilla.
    {
        var near = nearestPoint;
        var x = GetNearestPositionValue(transform.position.x);
        var y = GetNearestPositionValue(transform.position.y);
        var z = GetNearestPositionValue(transform.position.z);

        nearestPoint = DrawGrid.grid[x, y, z];
    }

    public bool CheckPointsInAnotherMesh(Vec3 externalPoint) //Cheque si hay algun punto dentro del obj.
    {
        return pointsInside.Any(pointsInside => pointsInside == externalPoint);   
    }
}
