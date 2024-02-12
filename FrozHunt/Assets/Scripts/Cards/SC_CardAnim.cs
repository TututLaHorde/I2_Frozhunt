using System.Collections;
using UnityEngine;

public class SC_CardAnim : MonoBehaviour
{
    [SerializeField] private AnimationCurve m_animCurve;
    [SerializeField] private GameObject m_card;
    [SerializeField] private float m_maxScale;
    [SerializeField] private float m_maxTime;
    private Vector3 m_minScale;

    public float Zoom()
    {
        m_minScale = m_card.transform.localScale;
        StartCoroutine(ChangeScale());
        return m_maxTime;
    }

    private IEnumerator ChangeScale()
    {
        float timeleft = m_maxTime;
        while (timeleft >= 0.0f)
        {
            timeleft -= Time.deltaTime;
            m_card.transform.localScale = m_minScale+m_minScale*(m_maxScale-1)*m_animCurve.Evaluate(timeleft/m_maxTime);
            yield return null;
        }
        m_card.transform.localScale = m_minScale;
    }
}
