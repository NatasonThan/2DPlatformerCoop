using Unity.Netcode;
using UnityEngine;

public class SpawnEnemy : NetworkBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    void Start()
    {
        if (IsServer)
        {
            SpawnEnemyPoint();
        }
    }

    void SpawnEnemyPoint()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy Prefab not assigned!");
            return;
        }

        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        var networkObject = enemy.GetComponent<NetworkObject>();

        if (networkObject != null)
        {
            networkObject.Spawn();
        }
        else
        {
            Debug.LogError("Enemy prefab does not have a NetworkObject component!");
        }
    }
}
