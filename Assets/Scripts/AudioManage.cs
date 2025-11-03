using UnityEngine;

public class AudioManage : MonoBehaviour
{
    public static AudioManage instance;
    private AudioSource m_AudioSource;
    [SerializeField] private AudioClip buttonClip;
    [SerializeField] private AudioClip musicBackgroudStart;
    [SerializeField] private AudioClip musicBackgroudInGame;
    [SerializeField] private AudioClip shoot;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip magic;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayButtonClickSound()
    {
        m_AudioSource.PlayOneShot(buttonClip);
    }
    public void PlayMusicBackgroudStart()
    {
        m_AudioSource.clip = musicBackgroudStart;
        m_AudioSource.loop = true;
        m_AudioSource.Play();
    }
    public void PlayMusicBackgroudInGame()
    {
        m_AudioSource.clip = musicBackgroudInGame;
        m_AudioSource.loop = true;
        m_AudioSource.Play();
    }
    public void StopMusic()
    {
        m_AudioSource.Stop();
    }
    public void PlayShootSound()
    {
        m_AudioSource.PlayOneShot(shoot);
    }
    public void PlayHitSound()
    {
        m_AudioSource.PlayOneShot(hit);
    }
    public void PlayMagicSound()
    {
        m_AudioSource.PlayOneShot(magic);
    }

}
