using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] private GameObject finishPoint;
    [SerializeField] private GameObject failText;
    //private Animator anim => GetComponent<Animator>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerManager player = collision.GetComponent<PlayerManager>();

        if (player != null && GameManager.instance.fruitCollected == GameManager.instance.totalFruits)
        {
            //anim.SetTrigger("activate");
            Debug.Log("Win");
            finishPoint.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Debug.Log("You need to collected all fruits");
            failText.SetActive(true);
        }
    }
}