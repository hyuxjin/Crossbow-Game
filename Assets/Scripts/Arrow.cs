using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;  

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obects"))
        {
            print("Hit " + collision.gameObject.name + " !");
            
            // Stick the arrow at the collision point
            transform.position = collision.contacts[0].point;
            transform.SetParent(collision.transform); // Parent to target

            // Disable physics
            rb.isKinematic = true;  // Makes the Rigidbody stop moving
            
            // No need to set angularVelocity (it causes the error)
        }
    }
}
