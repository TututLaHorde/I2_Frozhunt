using UnityEngine;

public class SC_SFXManager : MonoBehaviour
{
    [SerializeField] private AudioClip m_audioClip;
    public void SFXPlay(AudioClip clip)
    {
        SC_MusicManager.Instance.PlayClipAt(clip, transform.position);
    }
}
