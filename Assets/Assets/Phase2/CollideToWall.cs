using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideToWall : MonoBehaviour
{
    [SerializeField] private Rigidbody rg; 
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Door") || collision.gameObject.CompareTag("Cel"))  // lub  if (collision.gameObject.name == "Door")
        {
            rg.velocity = Vector3.zero;
        }

        //rb.velocity = Vector3.zero;
    }
}
