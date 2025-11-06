using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    public Transform[] spawnPoints;

    protected virtual void Start()
    {
        foreach (var point in spawnPoints)
        {
            int randomItemIndex = Random.Range(0, itemPrefabs.Length);
            GameObject chosenItem = itemPrefabs[randomItemIndex];

            Instantiate(chosenItem, point.position, Quaternion.identity);
        }
    }
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnItem();
        }
    }
    public void SpawnItem()
    {
        if (itemPrefabs == null || itemPrefabs.Length == 0)
        {
            Debug.LogError("Chưa gán Prefab vật phẩm nào trong Item Prefabs!");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Chưa gán các điểm Spawn Point!");
            return;
        }
        int randomItemIndex = Random.Range(0, itemPrefabs.Length);
        GameObject chosenItem = itemPrefabs[randomItemIndex];

        int randomPointIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenPoint = spawnPoints[randomPointIndex];

        Instantiate(chosenItem, chosenPoint.position, Quaternion.identity);
    }
}
