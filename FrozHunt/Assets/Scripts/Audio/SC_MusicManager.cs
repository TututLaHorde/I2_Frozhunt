using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public enum WindEffect
{
    lightWind, StrongWind,
}

public enum MenuMusic
{
    menu1,menu2, victoire, gameover
}

public class SC_MusicManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup m_SFXgroup;

    [Header ("Ambient")]
    [SerializeField] private AudioSource m_ambientSource;
    [SerializeField] private AudioClip[] m_ambientClip;

    [Header ("Music")]
    [SerializeField] private AudioSource m_musicSource;
    [SerializeField] private AudioClip[] m_menuClip;

    [Header ("AudioFade")]
    [SerializeField] private float timer;

    public static SC_MusicManager Instance;
    public void ChangeMusic(AudioClip clip)
    {
        StopAllCoroutines();
        StartCoroutine(AudioFade(m_musicSource, clip));
    }

    public void menuMusic(MenuMusic menumusic)
    {
        StopAllCoroutines();
        StartCoroutine(AudioFade(m_musicSource, m_menuClip[menumusic.GetHashCode()]));
    }

    public void windStop()
    {
       StartCoroutine(Stop(m_ambientSource));  
    }

    public void musicStop()
    {
        StartCoroutine(Stop(m_musicSource));
    }

    public void ChangeAmbient(WindEffect windEffect)
    {
        StopAllCoroutines();
        StartCoroutine(AudioFade(m_ambientSource, m_ambientClip[windEffect.GetHashCode()]));
    }

    public void PlayLowHPSound(int maxhealth, int health, WindEffect windEffect)
    {
        float percent = (health*100 / maxhealth);
        print(percent);
        if (percent <= 35)
        {
            StopAllCoroutines();
            StartCoroutine(AudioFade(m_ambientSource, m_ambientClip[windEffect.GetHashCode()]));
        }
    }

    public void normalAmbient(WindEffect windEffect,int maxhealth)
    {
        int highhealth = 0;
        for (int i = 0; i < Sc_GameManager.Instance.playerList.Count; i++)
        {
            if (Sc_GameManager.Instance.playerList[i].GetCurrentHealth * 100 / maxhealth > 35)
            {
                highhealth++;
            }
        }
        if (highhealth >= Sc_GameManager.Instance.playerList.Count)
        {
            StopAllCoroutines();
            StartCoroutine(AudioFade(m_ambientSource, m_ambientClip[windEffect.GetHashCode()]));
        }

    }

    void Awake()
    {
        if (Instance != null)
        {
            print("error already instanced");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public AudioSource PlayClipAt(AudioClip clip)
    {
        //create tempaudio object
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.localPosition = Vector3.zero;
        //assign to temp the audiosource
        AudioSource SFXsource = tempGO.AddComponent<AudioSource>();
        //make temp play sound
        SFXsource.clip = clip;
        SFXsource.outputAudioMixerGroup = m_SFXgroup;     
        SFXsource.Play();
        //destroy temp once it's done;
        Destroy(tempGO, clip.length);
        return SFXsource;
    }

    private IEnumerator Stop (AudioSource audioSource)
    {
        float time = timer;
        while (time >= 0)
        {
            //lower volume until it's muted
            time -= Time.deltaTime;
            audioSource.volume = time / timer;
            yield return null;
        }
        audioSource.clip = null;
    }

    private IEnumerator AudioFade(AudioSource audioSource, AudioClip clip)
    {
        
        if (audioSource.clip != clip)
        {
            float time = timer;
            while (time >= 0)
            {
                //lower volume until it's muted
                time -= Time.deltaTime;
                audioSource.volume = time / timer;
                yield return null;
            }
            //change clip
            audioSource.clip = clip;
            audioSource.Play();
            while (time <= timer)
            {
                //increase volume until it's max volume
                time += Time.deltaTime;
                audioSource.volume = time / timer;
                yield return null;
            }
        }
    }
}
