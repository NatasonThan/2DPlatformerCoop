using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player")]
    [SerializeField] private GameObject playerPrefab;
    /*[SerializeField] private Transform respawnPoint;
    [SerializeField] private float respawnDelay;*/
    public PlayerManager player;

    [Header("Fruit Management")]
    public bool fruitsHaveRandomLook;
    public int fruitCollected;
    public int totalFruits;

    [Header("Traps")]
    public GameObject arrowPrefab;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        CollectedFruitsInfo();
    }

    private void CollectedFruitsInfo()
    {
        Fruit[] allFruits = FindObjectsByType<Fruit>(FindObjectsSortMode.None);
        totalFruits = allFruits.Length;
    }

    /*public void RespawnPlayer() 
    {
        GameObject newPlayer = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
        player = newPlayer.GetComponent<PlayerManager>();
    }*/
    /*public void RespawnPlayer() => StartCoroutine(RespawnCourutine());
    
    private IEnumerator RespawnCourutine() 
    {
        yield return new WaitForSeconds(respawnDelay);

        GameObject newPlayer = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
        player = newPlayer.GetComponent<PlayerManager>();
    }*/

    public void AddFruit() => fruitCollected++;
    public bool FruitsHaveRandomLook() => fruitsHaveRandomLook;

    public void CreatObject(GameObject prepab, Transform target, float delay = 0) 
    {
        StartCoroutine(CreteObjectCorutine(prepab, target, delay));
    }

    private IEnumerator CreteObjectCorutine(GameObject prefab, Transform traget, float delay) 
    {
        Vector3 newPosition = traget.position;

        yield return new WaitForSeconds(delay);

        GameObject newObject = Instantiate(prefab, newPosition, Quaternion.identity);
    }
}
