using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab; // Assign your target prefab in the Inspector
    public Terrain terrain; // Assign your terrain in the Inspector
    public int spawnCount = 5; // Number of targets to spawn

    void Start()
    {
        SpawnTargets();
    }

    void SpawnTargets()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = GetRandomPositionOnTerrain();
            Instantiate(targetPrefab, spawnPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomPositionOnTerrain()
    {
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;
        float terrainHeight = terrain.terrainData.size.y;

        float randomX = Random.Range(0, terrainWidth);
        float randomZ = Random.Range(0, terrainLength);
        
        // Get terrain height at the random X, Z position
        float y = terrain.SampleHeight(new Vector3(randomX, 0, randomZ)) + terrain.transform.position.y;

        return new Vector3(randomX + terrain.transform.position.x, y, randomZ + terrain.transform.position.z);
    }
}
