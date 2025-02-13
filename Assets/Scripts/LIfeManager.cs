using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public RawImage[] lifeImages; // Drag 5 Raw Images in Inspector
    private int lives = 5; // Start with 5 lives
    public AudioSource hitSound; // 🎵 Assign this in Inspector
    public static LifeManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        UpdateLifeUI();
    }

    public void DecreaseLife()
    {
        if (lives > 0)
        {
            lives--;

            // 🔊 Play hit sound if assigned
            if (hitSound != null)
            {
                hitSound.Play();
            }

            UpdateLifeUI();
        }

        // Game Over Condition
        if (lives <= 0)
        {
            Debug.Log("Game Over!");
        }
    }

    private void UpdateLifeUI()
    {
        // Enable only the correct life images (show remaining lives)
        for (int i = 0; i < lifeImages.Length; i++)
        {
            lifeImages[i].enabled = (i < lives); // Show images for remaining lives
        }
    }
}
