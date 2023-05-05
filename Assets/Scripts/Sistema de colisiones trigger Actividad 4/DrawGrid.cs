using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using UnityEngine.SocialPlatforms.GameCenter;

public class DrawGrid : MonoBehaviour
{
    [Header("Setup")]
    
    [SerializeField] private float delta = 0.2f;
    
    private const int maxPoints = 10;

    public Vec3[,,] grid = new Vec3[maxPoints, maxPoints, maxPoints];

    public bool drawGrid = true;

    void Start()
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

    private void OnDrawGizmos()
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
                        Gizmos.DrawSphere(grid[i, j, k], 0.02f);
                    }
                }
            }
        }
    }
}
