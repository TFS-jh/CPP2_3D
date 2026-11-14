using UnityEngine;

public class ObligatoryRandomizedSpawns : MonoBehaviour
{
    [SerializeField] private GameObject[] randomPrefabs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log($"World pos: {transform.position}");

        transform.position = new Vector3(Random.Range(70, 105), 152, Random.Range(345, 395));

        SpawnRandomizedObjects();
    }

    private void SpawnRandomizedObjects()
    {
        if (randomPrefabs.Length == 0)
        {
            Debug.Log("no random prefabs but does it really matter");
            return;
        }

        int random = Random.Range(0, randomPrefabs.Length);

        Instantiate(randomPrefabs[random], transform.position, transform.rotation);
    }
}
