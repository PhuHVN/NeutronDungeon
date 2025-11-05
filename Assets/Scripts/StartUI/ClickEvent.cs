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
        AudioManage.instance.PlayMusicBackgroundStart();
        infoPanel.SetActive(false);

        play.onClick.AddListener(OnPlayClick);
        settings.onClick.AddListener(OnSettingsClick);
        exit.onClick.AddListener(OnExitClick);
        info.onClick.AddListener(ToggleInfoPanel);
        infoExitButton.onClick.AddListener(ToggleInfoPanel);
    }

    void OnPlayClick()
    {
        AudioManage.instance.PlayButtonClickSound();
        AudioManage.instance.StopMusic();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void OnSettingsClick()
    {
        AudioManage.instance.PlayButtonClickSound();
        Debug.Log("Settings Button Clicked");
    }

    void OnExitClick()
    {
        AudioManage.instance.PlayButtonClickSound();
        AudioManage.instance.StopMusic();
        Application.Quit();
        Debug.Log("Exit Button Clicked");
    }

    void ToggleInfoPanel()
    {
        AudioManage.instance.PlayButtonClickSound();
        infoPanel.SetActive(!infoPanel.activeSelf);
    }
}
