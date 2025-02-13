using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointsManager : MonoBehaviour
{
    public static PointsManager instance;
    public TMP_Text pointsText;
    private int points = 0; // Start at 0

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
        
        UpdatePointsUI();
    }

    public void AddPoints(int amount)
    {
        points += amount; // Add points
        UpdatePointsUI();
    }

    private void UpdatePointsUI()
    {
        pointsText.text = "Points: " + points; // Update UI
    }
}
