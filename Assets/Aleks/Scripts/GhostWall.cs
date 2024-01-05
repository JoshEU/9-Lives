using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostWall : MonoBehaviour
{
    //script to only let the ghost pass thru walls
    public GameObject WallCollider;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "ghost")
        {
            //disable collider game object upon collision
            WallCollider.SetActive(false);
        }
        else
        {
            //if anything else is colliding enable
            WallCollider.SetActive(true);
        }
    }
}
