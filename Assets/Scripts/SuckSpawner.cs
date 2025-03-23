using UnityEngine;

public class SuckSpawner : MonoBehaviour
{
    public GameObject spawnPrefab;
    public int amountToSpawn = 5;
    [SerializeField]
    public Vector3 minSpawn = new Vector3(10, 4, 10);
    [SerializeField]
    public Vector3 maxSpawn = new Vector3(0, 4, 0);

    private void Start()
    {
        SpawnNpcs();
    }

    void SpawnNpcs()
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject newNpc = Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);
            Transform child = newNpc.transform.GetChild(0).GetChild(1); ;
            newNpc.transform.parent = transform;
            if (child != null)
            {
                Renderer renderer = child.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = GetRandomColor();
                }
            }
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(minSpawn.x, maxSpawn.x),
            Random.Range(minSpawn.y, maxSpawn.y),
            Random.Range(minSpawn.z, maxSpawn.z)
            );
        
        return transform.position + randomPosition;
    }

    Color GetRandomColor()
    {
        return new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
    }

    private void OnDrawGizmos()
    {
        Vector3 cubeSize = new Vector3(maxSpawn.x - minSpawn.x, maxSpawn.y - minSpawn.y, maxSpawn.z - minSpawn.z);
        Gizmos.DrawWireCube(transform.position, cubeSize);
    }
}
