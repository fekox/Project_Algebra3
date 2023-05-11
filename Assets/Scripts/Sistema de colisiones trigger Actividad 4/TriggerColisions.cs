using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using static Activity_Planes;
using System.Security.Cryptography;

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

    
}
