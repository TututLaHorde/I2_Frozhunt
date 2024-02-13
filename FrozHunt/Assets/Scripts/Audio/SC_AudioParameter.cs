using UnityEngine;
using UnityEngine.Audio;

public class SC_AudioParameter : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void setMastervolume(float volume)
    {
        audioMixer.SetFloat("Master", volume);
    }

    public void setMusicvolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }

    public void setSFXvolume(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
    }
    public void close()
    {
        gameObject.SetActive(false);
    }
}
