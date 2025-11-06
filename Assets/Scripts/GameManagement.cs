using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public static GameManagement Instance { get; private set; }
    private int totalBossesToDefeat = 0;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehavior is created
    void Start()
    {
        if (AudioManage.instance != null)
        {
            AudioManage.instance.PlayMusicBackgroundInGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void WinGame()
    {

        EndGameUIController.Instance.pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public int incrementTotalDeathsToWin()
    {
        return ++totalBossesToDefeat;
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        AudioManage.instance.PlayMusicBackgroundInGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
