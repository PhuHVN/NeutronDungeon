using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScene : MonoBehaviour
{
    [Header("UI References")]
    public GameObject pauseScene;       // Canvas PauseScene
                                        //public Button pauseButton;          // Button m? Pause menu
                                        //public Button continueButton;       // Button Continue
                                        //public Button quitButton;           // Button Quit
                                        // Start is called once before the first execution of Update after the MonoBehaviour is created
                                        //void Start()
                                        //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}
    private bool isPaused = false;

    public void OnContinueClicked()
    {
        // Ti?p t?c game
        Time.timeScale = 1f;
        isPaused = false;

        // ?n PauseScene
        pauseScene.SetActive(false);
    }

    public void OnQuitClicked()
    {
        // Khôi ph?c t?c ð? game trý?c khi load scene
        Time.timeScale = 1f;

        // Load scene kh?i ð?u
        SceneManager.LoadScene("StartScene");
    }
}
