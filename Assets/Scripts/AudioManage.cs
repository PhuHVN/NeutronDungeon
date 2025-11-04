using UnityEngine;
using UnityEngine.SceneManagement; // <-- 1. Add this

public class AudioManage : MonoBehaviour
{
    public static AudioManage instance;
    private AudioSource m_AudioSource;      // For background music
    private AudioSource sfx_AudioSource;    // For sound effects

    [Header("UI Sounds")]
    [SerializeField] private AudioClip buttonClip;

    [Header("Background Music")]
    [SerializeField] private AudioClip musicBackgroundStart;
    [SerializeField] private AudioClip musicBackgroundInGame;
    [SerializeField] private AudioClip musicBossInGame;

    [Header("Game Sound Effects")]
    [SerializeField] private AudioClip shoot;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip magic;

    private void Awake()
    {
        // --- 1. Singleton Pattern ---
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return; // Stop running code if this is a duplicate
        }

        // --- 2. Setup AudioSources (MOVED FROM START) ---
        AudioSource[] audioSources = GetComponents<AudioSource>();

        if (audioSources.Length >= 2)
        {
            m_AudioSource = audioSources[0];    // Music
            sfx_AudioSource = audioSources[1];  // Sound effects
        }
        else if (audioSources.Length == 1)
        {
            m_AudioSource = audioSources[0];
            sfx_AudioSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            m_AudioSource = gameObject.AddComponent<AudioSource>();
            sfx_AudioSource = gameObject.AddComponent<AudioSource>();
        }

        // --- 3. Configure AudioSources ---
        m_AudioSource.playOnAwake = false;
        m_AudioSource.loop = true;

        sfx_AudioSource.playOnAwake = false;
        sfx_AudioSource.loop = false;
    }

    // --- 4. Listen for Scene Changes ---
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // This function runs every time a new scene is finished loading
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check the name of the newly loaded scene
        if (scene.name == "StartScene") // <-- Use your exact StartScene name
        {
            PlayMusicBackgroundStart();
        }
        else if (scene.name == "Level 2" || scene.name == "Level 1") // <-- Use your game level names
        {
            PlayMusicBackgroundInGame();
        }
        else if(scene.name == "BossStage1")
        {
            PLayBossMusicInGame();
        }
    }

    // --- Public Functions (No changes needed, they are great) ---

    public void PlayButtonClickSound()
    {
        if (sfx_AudioSource != null && buttonClip != null)
            sfx_AudioSource.PlayOneShot(buttonClip);
    }

    public void PlayMusicBackgroundStart()
    {
        if (m_AudioSource != null && musicBackgroundStart != null)
        {
            m_AudioSource.clip = musicBackgroundStart;
            m_AudioSource.Play();
        }
    }

    public void PlayMusicBackgroundInGame()
    {
        if (m_AudioSource != null && musicBackgroundInGame != null)
        {
            m_AudioSource.clip = musicBackgroundInGame;
            m_AudioSource.Play();
        }
    }

    public void PLayBossMusicInGame()
    {
        if (m_AudioSource != null && musicBossInGame != null)
        {
            m_AudioSource.clip = musicBossInGame;
            m_AudioSource.Play();
        }
    }

    public void StopMusic()
    {
        if (m_AudioSource != null) m_AudioSource.Stop();
    }

    public void PlayShootSound()
    {
        if (sfx_AudioSource != null && shoot != null)
            sfx_AudioSource.PlayOneShot(shoot);
    }

    public void PlayHitSound()
    {
        if (sfx_AudioSource != null && hit != null)
            sfx_AudioSource.PlayOneShot(hit);
    }

    public void PlayMagicSound()
    {
        if (sfx_AudioSource != null && magic != null)
            sfx_AudioSource.PlayOneShot(magic);
    }
}