using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SC_AudioParameter : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField] private Slider SliderMasters;
    [SerializeField] private Slider SliderMusic;
    [SerializeField] private Slider SliderAmbient;
    public void setMastervolume(float volume)
    {
        audioMixer.SetFloat("Master", volume);
    }

    private void OnEnable()
    {
        if (audioMixer.GetFloat("Master", out float Mastervalue))
        {
            SliderMasters.SetValueWithoutNotify(Mastervalue);
        }
        if (audioMixer.GetFloat("Music", out float Musicvalue))
        {
            SliderMusic.SetValueWithoutNotify(Musicvalue);
        }
        if (audioMixer.GetFloat("Ambient", out float value))
        {
            SliderAmbient.SetValueWithoutNotify(value);
        }
    }


    public void setMusicvolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }

    public void setSFXvolume(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
    }

    public void setAmbientVolume(float volume)
    {
        audioMixer.SetFloat("Ambient", volume);
    }
    public void close()
    {
        gameObject.SetActive(false);
    }
}
