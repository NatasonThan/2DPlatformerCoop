using UnityEngine;

public enum FruitType { Apple , Banana , Cherry , Kiwi , Melon , Orange , Pineapple , Strawberry}

public class Fruit : MonoBehaviour
{
    [SerializeField] private FruitType fruitType;
    [SerializeField] private GameObject pickupVfx;

    private GameManager gameManager;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        gameManager = GameManager.instance;
        SetRandomLookIfNeeded();
    }
    private void SetRandomLookIfNeeded() 
    {
        if (gameManager.FruitsHaveRandomLook() == false) 
        {
            UpdateFruitVisuals();
            return;
        }

        int randowIndex = Random.Range(0, 8);
        anim.SetFloat("fruitIndex", randowIndex);
    }
    private void UpdateFruitVisuals() => anim.SetFloat("fruitIndex", (int)fruitType); 
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        PlayerManager player = collision.GetComponent<PlayerManager>();

        if (player != null) 
        {
            gameManager.AddFruit();
            AudioManager.instance.PlaySFX(7);
            Destroy(gameObject);

            GameObject newFx = Instantiate(pickupVfx,transform.position,Quaternion.identity);
        }
    }
}
