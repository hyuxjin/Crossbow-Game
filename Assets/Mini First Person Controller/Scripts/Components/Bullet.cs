using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    private static int targetCount = 0; // ✅ Static: Shared across all bullets

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Objects"))
        {
            print("Hit " + collision.gameObject.name + " !");

            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
            }

            transform.position = collision.contacts[0].point;
            transform.SetParent(collision.transform);
        }
        else if (collision.gameObject.CompareTag("Target")) // 🎯 Hits a target
            {
                targetCount += 1;
                print("Hit Target: " + targetCount + " !");

                if (PointsManager.instance != null) 
                {
                    PointsManager.instance.AddPoints(100); // ✅ Add 100 points!
                }

                if (rb != null)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    rb.isKinematic = true;
                }

                collision.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        if (collision.gameObject.CompareTag("Animal")) // Make sure the target has this tag
        {
            LifeManager.instance.DecreaseLife();

                        if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
            }

            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

    }
}
