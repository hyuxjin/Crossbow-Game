using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public RawImage[] lifeImages; // Assign life images in Inspector
    private int lives = 5; // Start with 5 lives


    public AudioSource healSound;
    public AudioSource hitSound; // Assign sound in Inspector
    public AudioSource gameOverSound; // 🔊 Game Over sound
    public AudioSource victorySound; // 🔊 Victory sound

    public GameObject gameOverPanel; // Assign the GameOver panel in Inspector
    public TMP_Text gameOverScoreText; // Assign the GameOver panel's score text in Inspector

    public FirstPersonMovement player; // Reference to the player movement script
    public Crossbow shooting; // Reference to the shooting script
    public int GetTargetCount() => targetCount;

    public GameObject victoryPanel; 
    public TMP_Text victoryScoreText; 
    private int targetCount = 0; // ✅ Centralized tracking

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

        // Hide GameOver panel at the start
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }

        // Ensure cursor is locked at the start
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void IncreaseTargetCount()
    {
        targetCount++;

        if (targetCount >= 15)
        {
            ShowVictoryPanel();
        }
    }

    private void ShowVictoryPanel()
    {
        if (victoryPanel != null)
        {
            int finalScore = PointsManager.instance.GetFinalScore();
            victoryScoreText.text = "Final Score: " + finalScore;
            victoryPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // 🔊 Play victory sound
            if (victorySound != null)
            {
                victorySound.Play();
            }
            else
            {
                Debug.LogWarning("Victory sound is not assigned!");
            }
        }
    }

    public void DecreaseLife()
    {
        if (lives > 0)
        {
            lives--;

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

            // Ensure latest points are updated before showing GameOver panel
            StartCoroutine(ShowGameOverDelayed());
        }
    }

    private IEnumerator ShowGameOverDelayed()
    {
        yield return new WaitForEndOfFrame(); // Wait until the end of the frame to ensure updates are processed

        ShowGameOverPanel();
    }

    private void UpdateLifeUI()
    {
        for (int i = 0; i < lifeImages.Length; i++)
        {
            lifeImages[i].enabled = (i < lives);
        }
    }

    public void IncreaseLife()
    {
        if (lives < lifeImages.Length && lives < 5) // Prevent increasing beyond 5
        {
            lives++;
            UpdateLifeUI();

            // 🔊 Play heal sound
            if (healSound != null)
            {
                healSound.Play();
            }
            else
            {
                Debug.LogWarning("Heal sound is not assigned!");
            }
        }
        else
        {
            Debug.Log("Lives are already at maximum (5). Heart will not be destroyed.");
        }
    }

    public int GetLives()
    {
        return lives;
    }


    private void ShowGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            // Get the final score from PointsManager
            int finalScore = PointsManager.instance.GetFinalScore();

            // Update the GameOver panel's score text
            if (gameOverScoreText != null)
            {
                gameOverScoreText.text = "Score: " + finalScore;
            }
            else
            {
                Debug.LogError("GameOver Score Text is not assigned in Inspector!");
            }

            gameOverPanel.SetActive(true);

            // 🎯 Show cursor when Game Over
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // 🚫 Disable player movement and shooting
            if (player != null)
                player.enabled = false;

            if (shooting != null)
                shooting.enabled = false;

            // 🔊 Play Game Over sound
            if (gameOverSound != null)
            {
                gameOverSound.Play();
            }
            else
            {
                Debug.LogWarning("Game Over sound is not assigned!");
            }
        }
        else
        {
            Debug.LogError("GameOver panel is not assigned in the Inspector!");
        }
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("MainMenu"); // 🔄 Load the Main Menu
    }
}
