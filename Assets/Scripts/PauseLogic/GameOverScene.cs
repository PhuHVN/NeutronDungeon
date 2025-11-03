using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    [Header("UI References")]
    public GameObject pauseScene;       // Canvas PauseScene
    //public Button pauseButton;          // Button m? Pause menu
    //public Button continueButton;       // Button Continue
    //public Button quitButton;           // Button Quit

    private bool isPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void OnRestartClicked()  // ?? nút Restart
    {
        Time.timeScale = 1f; // Ti?p t?c th?i gian trý?c khi reload
        isPaused = false;

        // Reload l?i scene hi?n t?i
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void OnQuitClicked()
    {
        // Khôi ph?c t?c ð? game trý?c khi load scene
        Time.timeScale = 1f;

        // Load scene kh?i ð?u
        SceneManager.LoadScene("StartScene");
    }


}
