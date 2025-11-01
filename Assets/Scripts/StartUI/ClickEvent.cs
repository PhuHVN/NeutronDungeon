using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickEvent : MonoBehaviour
{
    [Header("Menu Button")]
    public Button play;
    public Button settings;
    public Button exit;

    [Header("Info Button")]
    public Button info;
    public GameObject infoPanel;
    public Button infoExitButton;

    void Start()
    {
        infoPanel.SetActive(false);

        play.onClick.AddListener(OnPlayClick);
        settings.onClick.AddListener(OnSettingsClick);
        exit.onClick.AddListener(OnExitClick);
        info.onClick.AddListener(ToggleInfoPanel);
        infoExitButton.onClick.AddListener(ToggleInfoPanel);
    }

    void OnPlayClick()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void OnSettingsClick()
    {
        Debug.Log("Settings Button Clicked");
    }

    void OnExitClick()
    {
        Application.Quit();
        Debug.Log("Exit Button Clicked");
    }

    void ToggleInfoPanel()
    {
        infoPanel.SetActive(!infoPanel.activeSelf);
    }
}
