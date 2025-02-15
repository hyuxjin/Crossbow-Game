using System.Collections;
using UnityEngine;

public class HorseController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;
    public float moveSpeed = 2f;
    private bool isMoving = false;
    private float changeTimeMin = 2f;
    private float changeTimeMax = 5f;
    public LayerMask groundLayer;
    public LayerMask obstacleLayer; // For detecting obstacles

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        StartCoroutine(ChangeStateRoutine());
    }

    IEnumerator ChangeStateRoutine()
    {
        while (true)
        {
            // Wait for a random amount of time before changing the state
            float waitTime = Random.Range(changeTimeMin, changeTimeMax);
            yield return new WaitForSeconds(waitTime);

            // Trigger "Walk" animation
            anim.SetBool("Walk", true);
            isMoving = true; // Set isMoving to true when walking starts

            // Wait for 3 seconds before setting the "Walk" animation to false
            yield return new WaitForSeconds(1f);
            
            // Set the "Walk" animation to false after 3 seconds
            anim.SetBool("Walk", false);
            isMoving = false; // Set isMoving to false when walking stops
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            AdjustHeight();

            // Check if there's an obstacle ahead
            if (Physics.Raycast(transform.position, transform.forward, 1.5f, obstacleLayer))
            {
                RotateRandomly(); // If an obstacle is detected, rotate
            }

            rb.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void RotateRandomly()
    {
        float randomAngle = Random.Range(90f, 270f); // Rotate left or right
        transform.Rotate(0, randomAngle, 0);
    }

    private void AdjustHeight()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 2f, Vector3.down, out hit, 10f, groundLayer))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // If horse collides with an obstacle, rotate to another direction
        if (((1 << other.gameObject.layer) & obstacleLayer) != 0)
        {
            RotateRandomly();
        }
    }
}
