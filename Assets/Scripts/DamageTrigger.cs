using Unity.Services.Lobbies.Models;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerManager player = collision.gameObject.GetComponent<PlayerManager>();

        if (player != null)
            player.KnockBack();
    }
}
