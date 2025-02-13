using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    public Camera cam;
    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;
    public float shootForce = 20f;
    public Animator bowAnim;
    
    private bool canShoot = true;  // Cooldown flag

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            bowAnim.SetBool("Fire", true);
        }

        if (Input.GetMouseButtonUp(0) && canShoot)
        {
            StartCoroutine(FireArrow());
        }
    }

    IEnumerator FireArrow()
    {
        canShoot = false;  // Prevent firing immediately again
        // Instantiate the arrow
        GameObject go = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.identity);
        
        // Get Rigidbody from the new arrow object
        Rigidbody rb = go.GetComponent<Rigidbody>();  
        
        if (rb != null)  // Ensure the arrow has a Rigidbody
        {
            rb.velocity = cam.transform.forward * shootForce;  // Apply force
        }
        else
        {
            Debug.LogError("Rigidbody is missing on the arrow prefab!");
        }

        bowAnim.SetBool("Fire", false);

        yield return new WaitForSeconds(1);  // Wait for 3 seconds before shooting again
        canShoot = true;  // Re-enable shooting
    }
}
