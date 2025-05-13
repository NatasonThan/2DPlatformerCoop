using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private UI_Ingame inGameUI;

    [Header("Level Managment")]
    [SerializeField] private float levelTimer;

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
        inGameUI = UI_Ingame.instance;
        CollectedFruitsInfo();
        SetTimeOpen();
    }
    private void Update()
    {
        levelTimer = Time.time;

        inGameUI.UpdateTimerUI(levelTimer);
    }

    private void CollectedFruitsInfo()
    {
        Fruit[] allFruits = FindObjectsByType<Fruit>(FindObjectsSortMode.None);
        totalFruits = allFruits.Length;

        inGameUI.UpdateFruitUI(fruitCollected, totalFruits);
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

    public void AddFruit() 
    {
        fruitCollected++;
        inGameUI.UpdateFruitUI(fruitCollected, totalFruits);
    }
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

    private void SetTimeOpen() 
    {
        Time.timeScale = 1;
        levelTimer = 0;
        levelTimer = Time.time;
        inGameUI.UpdateTimerUI(levelTimer);
    }
}
