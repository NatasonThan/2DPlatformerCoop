using System.Collections;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class RespawnHandler : NetworkBehaviour
{
    [SerializeField] private PlayerServer playerPrefab;
    [SerializeField] private float keptCoinPercentage;

    public override void OnNetworkSpawn()
    {
        if (!IsServer) { return; }
        PlayerServer[] players = FindObjectsByType<PlayerServer>(FindObjectsSortMode.None);
        foreach (PlayerServer player in players)
        {
            HandlePlayerSpawned(player);
        }

        PlayerServer.OnPlayerSpawned += HandlePlayerSpawned;
        PlayerServer.OnPlayerDespawned += HandlePlayerDespawned;
    }

    public override void OnNetworkDespawn()
    {
        if (!IsServer) { return; }
        PlayerServer.OnPlayerSpawned -= HandlePlayerSpawned;
        PlayerServer.OnPlayerDespawned -= HandlePlayerDespawned;
    }

    private void HandlePlayerSpawned(PlayerServer player)
    {
        player.Health.OnDie += (health) => HandlePlayerDie(player);
    }

    private void HandlePlayerDespawned(PlayerServer player)
    {
        player.Health.OnDie -= (health) => HandlePlayerDie(player);
    }

    private void HandlePlayerDie(PlayerServer player)
    {
        //int keptCoins = (int)(player.Wallet.TotalCoins.Value * (keptCoinPercentage / 100));
        Destroy(player.gameObject);

        StartCoroutine(RespawnPlayer(player.OwnerClientId));
    }

    private IEnumerator RespawnPlayer(ulong ownerClientId)
    {
        yield return null;

        PlayerServer playerInstance = Instantiate(
            playerPrefab, SpawnPoint.GetRandomSpawnPos(), Quaternion.identity);

        playerInstance.NetworkObject.SpawnAsPlayerObject(ownerClientId);

        //playerInstance.Wallet.TotalCoins.Value += keptCoins;
    }
}