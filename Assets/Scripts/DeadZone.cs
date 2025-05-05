using Unity.Netcode;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        PlayerManager player = collision.gameObject.GetComponent<PlayerManager>();

        if (player != null) 
        {
            player.Die();
            //GameManager.instance.RespawnPlayer();
        }
    }
}
