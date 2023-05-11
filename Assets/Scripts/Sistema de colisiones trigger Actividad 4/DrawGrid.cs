using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using UnityEngine.SocialPlatforms.GameCenter;

public class DrawGrid : MonoBehaviour
{
    [Header("Setup")]
    
    public static float delta = 1; //Distancia de los puntos dentro de la grilla.
    
    public static int maxPoints = 10; //Tamaño maximo de la grilla.

    public static Vec3[,,] grid = new Vec3[maxPoints, maxPoints, maxPoints]; //Crea la grilla.

    public bool drawGrid = true; //Booleano que activa o deactiva la grilla.

    void Start() //Se posicionan todos los puntos de la grilla.
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                for (int k = 0; k < grid.GetLength(2); k++)
                {
                    grid[i, j, k] = new Vec3(i, j, k) * delta;
                }
            }
        }
    }

    private void OnDrawGizmos() //Dibuja la grilla si el bool es true.
    {
        if (drawGrid == true) 
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    for (int k = 0; k < grid.GetLength(2); k++)
                    {
                        Gizmos.color = Color.white;
                        Gizmos.DrawSphere(grid[i, j, k], 0.1f);
                    }
                }
            }
        }
    }
}
