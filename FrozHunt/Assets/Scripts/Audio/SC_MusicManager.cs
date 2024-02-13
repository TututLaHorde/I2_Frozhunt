using UnityEngine;
using UnityEngine.Audio;

public class SC_MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] playlist;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioMixerGroup m_SFXgroup;

    public static SC_MusicManager Instance;
    public void ChangeMusic(int arraypos)
    {
        if(arraypos < playlist.Length)
        {
            audioSource.clip = playlist[arraypos];
            audioSource.Play();
        }
        else
        {
            print("out of bounds");
        }
    }

    void Awake()
    {
        if (Instance != null)
        {
            print("error already instanced");
            return;
        }
        Instance = this;
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public AudioSource PlayClipAt(AudioClip clip)
    {
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.localPosition = Vector3.zero;
        AudioSource SFXsource = tempGO.AddComponent<AudioSource>();
        SFXsource.clip = clip;
        SFXsource.outputAudioMixerGroup = m_SFXgroup;
        SFXsource.Play();
        Destroy(tempGO, clip.length);
        return SFXsource;
    }
}
