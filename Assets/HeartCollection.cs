using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollection : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (LifeManager.instance != null && LifeManager.instance.GetLives() < 5)
            {
                LifeManager.instance.IncreaseLife();
                Destroy(gameObject); // Destroy the heart only if lives < 5
            }
            else
            {
                Debug.Log("Player's lives are full. Heart remains.");
            }
        }
    }
}
