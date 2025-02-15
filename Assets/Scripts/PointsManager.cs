using UnityEngine;
using TMPro;
using System.Collections;

public class PointsManager : MonoBehaviour
{
    public static PointsManager instance;
    public TMP_Text pointsText;  // Main points display
    public TMP_Text addSubScoreText;  // Text for showing added or subtracted score
    private int points = 0; // Start at 0

    // Add public AudioClips for point increase/decrease sounds
    public AudioClip increaseSound;  // Sound for increasing points
    public AudioClip decreaseSound;  // Sound for decreasing points

    private AudioSource audioSource;  // AudioSource to play the sound

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

        // Get the AudioSource component from the same GameObject
        audioSource = GetComponent<AudioSource>();
        UpdatePointsUI();
    }

    public void AddPoints(int amount)
    {
        points += amount; // Add points
        UpdatePointsUI();
        
        // Show the change in score (positive)
        StartCoroutine(ShowAddSubScore("+" + amount, Color.green));  // Green for added points
    }

    public void DecreasePoints(int amount)
    {
        points -= amount; // Decrease points
        
        // Show the change in score (negative)
        StartCoroutine(ShowAddSubScore("-" + amount, Color.red)); 
        UpdatePointsUI();
    }

    // Method to update the UI text
    private void UpdatePointsUI()
    {
        pointsText.text = "Points: " + points; // Update the main points display
    }

    // Coroutine to display the added/subtracted score and hide it after 1 second
    private IEnumerator ShowAddSubScore(string scoreChange, Color textColor)
    {
        addSubScoreText.text = scoreChange;  // Set the text to show the score change
        addSubScoreText.color = textColor;   // Set the color based on add or subtract
        addSubScoreText.gameObject.SetActive(true);  // Show the UI element

        yield return new WaitForSeconds(1f);  // Wait for 1 second

        addSubScoreText.gameObject.SetActive(false);  // Hide the UI element again
    }

    // Method to play the sound effect
    public void PlaySoundEffect(bool isIncrease)
    {
        if (audioSource != null)
        {
            if (isIncrease && increaseSound != null)
            {
                audioSource.PlayOneShot(increaseSound); // Play the increase sound
            }
            else if (!isIncrease && decreaseSound != null)
            {
                audioSource.PlayOneShot(decreaseSound); // Play the decrease sound
            }
        }
    }

    public int GetFinalScore()
    {
        return points; // Returns the current score
    }

}
