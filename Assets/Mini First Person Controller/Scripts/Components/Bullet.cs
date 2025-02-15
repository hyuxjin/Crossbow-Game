using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    
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
        else if (collision.gameObject.CompareTag("Target")) // 🎯 Target Hit
        {
            LifeManager.instance.IncreaseTargetCount(); // ✅ Correct tracking
            print("Hit Target! Total Hits: " + LifeManager.instance.GetTargetCount()); 

            float distance = Vector3.Distance(transform.position, collision.transform.position);
            int points = Mathf.Max(0, (int)(100 / (distance + 1) + 100));

            if (PointsManager.instance != null)
            {
                PointsManager.instance.AddPoints(points);
                PointsManager.instance.PlaySoundEffect(true);
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
        else if (collision.gameObject.CompareTag("Animal"))
        {
            LifeManager.instance.DecreaseLife();

            if (PointsManager.instance != null)
            {
                PointsManager.instance.DecreasePoints(50);
                PointsManager.instance.PlaySoundEffect(false);
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
    }
}
