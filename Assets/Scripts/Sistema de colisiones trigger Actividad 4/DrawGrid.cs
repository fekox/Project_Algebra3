using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using UnityEngine.SocialPlatforms.GameCenter;

public class DrawGrid : MonoBehaviour
{
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0.5f, 0.5f, 0.5f);
        Gizmos.DrawWireCube(transform.position, new Vec3(1, 0, 1));
    }
}
