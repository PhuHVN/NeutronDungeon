using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [Header("UI References")]
    public GameObject pauseScene;       // Canvas PauseScene

    private bool isPaused = false;
    //void Start()
    //{
    //    pauseScene.SetActive(false);
    //}
    public void OnPauseClicked()
    {
        if (isPaused) return;

        // D?ng th?i gian trong game
        Time.timeScale = 0f;
        isPaused = true;

        // Hi?n PauseScene
        pauseScene.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
