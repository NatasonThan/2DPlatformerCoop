using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    private PlayerManager player;

    private void Awake()
    {
        player = GetComponentInParent<PlayerManager>();
    }
    public void FinishRespawn() => player.RespawnFinished(true);
}
