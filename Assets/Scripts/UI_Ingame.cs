using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UI_Ingame : MonoBehaviour
{
    public static UI_Ingame instance;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI fruitsText;

    [SerializeField] private GameObject pauseUI;

    private bool isPaused;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseButton();
    }
    public void PauseButton() 
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1;
            pauseUI.SetActive(false);
        }
        else 
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseUI.SetActive(true);
        }
    }

    public void GoToMainMenu() 
    {
        if (NetworkManager.Singleton.IsHost) 
        {
            HostSingleton.Instance.GameManager.Shutdown();
        }

        ClientSingleton.Instance.GameManager.Disconnect();

        SceneManager.LoadScene("MainMenu");
    }

    public void UpdateFruitUI(int collectFruits, int totalFruits)
    {
        fruitsText.text = collectFruits + " / " + totalFruits;
    }

    public void UpdateTimerUI(float timer)
    {
        timerText.text = timer.ToString("00") + " S";
    }
}
