using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSpawner : MonoBehaviour
{
    public GameObject heartPrefab; // Assign in Inspector
    public Terrain terrain; // Assign terrain in Inspector
    public int heartCount = 20; // Number of hearts
    public float floatHeight = 1f; // Adjusted height to float properly

    void Start()
    {
        ScatterHearts();
    }

    void ScatterHearts()
    {
        for (int i = 0; i < heartCount; i++)
        {
            // Get random X, Z within terrain bounds
            float randomX = Random.Range(terrain.transform.position.x, terrain.transform.position.x + terrain.terrainData.size.x);
            float randomZ = Random.Range(terrain.transform.position.z, terrain.transform.position.z + terrain.terrainData.size.z);

            // Get the terrain height at (X, Z)
            float terrainHeight = terrain.SampleHeight(new Vector3(randomX, 0, randomZ)) + terrain.transform.position.y;

            // Clamp height to prevent spawning too high
            terrainHeight = Mathf.Clamp(terrainHeight, terrain.transform.position.y, terrain.transform.position.y + terrain.terrainData.size.y);

            // Ensure the heart floats properly
            Vector3 spawnPosition = new Vector3(randomX, terrainHeight + floatHeight, randomZ);
            Instantiate(heartPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
