using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] private TriggerColisions[] meshes; //Meshes de los objetos.

    [SerializeField] private bool collide; //Boleano si esta colisionando o no.

    [SerializeField] private Material red; //Material rojo.

    [SerializeField] private Material purple; //Material purpura.

    // Update is called once per frame
    void Update()
    {
        collide = false;

        foreach (var point in meshes[1].pointsInside) 
        {
            if (meshes[0].CheckPointsInAnotherMesh(point)) //Chequeo la colision de cada mesh.
            {
                collide = true;
            }
        }

        if (collide == false) //Si el boleano es false pinto de rojo los obj.
        {
            meshes[0].GetComponent<MeshRenderer>().material = red;
            meshes[1].GetComponent<MeshRenderer>().material = red;
        }

        else //Si no pinto de purpura los obj.
        {
            meshes[0].GetComponent<MeshRenderer>().material = purple;
            meshes[1].GetComponent<MeshRenderer>().material = purple;
        }
    }
}
