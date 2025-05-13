using Unity.Netcode;
using UnityEngine;

public class EnemyLauncher : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject serverProjectilePrefab;
    [SerializeField] private GameObject clientProjectilePrefab;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) { return; }
        PrimaryFireServerRpc();
    }

    public override void OnNetworkDespawn()
    {
        if (!IsOwner) { return; }

    }

    [Rpc(SendTo.Server)]
    private void PrimaryFireServerRpc()
    {
        GameObject projectileInstance = Instantiate(serverProjectilePrefab, transform.position, Quaternion.identity);

        SpawnDummyProjectileClientRpc();
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void SpawnDummyProjectileClientRpc()
    {
        if (IsOwner) { return; }

        SpawnDummyProjectile();
    }

    private void SpawnDummyProjectile()
    {
        GameObject projectileInstance = Instantiate(clientProjectilePrefab, transform.position, Quaternion.identity);
    }
}