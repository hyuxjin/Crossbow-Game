using System.Collections;
using UnityEngine;

public class ChickenController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;
    public float moveSpeed = 2f;
    private bool isMoving = false;
    private float changeTimeMin = 1f;
    private float changeTimeMax = 2f;
    public LayerMask groundLayer; 

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
            float waitTime = Random.Range(changeTimeMin, changeTimeMax);
            yield return new WaitForSeconds(waitTime);

            isMoving = Random.value > 0.5f; // 50% chance to walk or idle

            if (isMoving) 
            {
                anim.SetTrigger("Walk");
                RotateRandomly();
            }
            else 
            {
                anim.SetTrigger("Idle");
            }
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            AdjustHeight();
            rb.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void RotateRandomly()
    {
        float randomAngle = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0, randomAngle, 0);
    }

    private void AdjustHeight()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 2f, Vector3.down, out hit, 10f, groundLayer))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }
}
