using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public static GameManagement Instance { get; private set; }
    private int totalBossesToDefeat = 0;
    private void Awake()
    {
        if(Instance == null)
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
        if(AudioManage.instance != null)
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
        Debug.Log("You Win!");
    }
    public int incrementTotalDeathsToWin()
    {
        return ++totalBossesToDefeat;
    }
}
