using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using Unity.VisualScripting;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private GameObject loseUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        PlayerManager player = collision.GetComponent<PlayerManager>();

        if (player != null) 
        {
            player.Die();
            //GameManager.instance.RespawnPlayer();
            loseUI.SetActive(true);
        }
    }
}
