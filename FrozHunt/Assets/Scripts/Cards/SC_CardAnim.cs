using System.Collections;
using UnityEngine;

public class SC_CardAnim : MonoBehaviour
{
    [SerializeField] private AnimationCurve m_animCurve;
    [SerializeField] private GameObject m_card;
    [SerializeField] private float m_maxScale;
    [SerializeField] private float m_maxTime;
    private Vector3 m_minScale;

    private void Start()
    {
        m_minScale = m_card.transform.localScale;
    }

    public float Zoom()
    {
        //m_minScale = m_card.transform.localScale;
        StartCoroutine(ChangeScale());
        return m_maxTime;
    }

    public void ZoomUp()
    {
        StopAllCoroutines();
        StartCoroutine(ZoomUpAnim());
    }

    public void ZoomDown()
    {
        StopAllCoroutines();
        StartCoroutine(ZoomDownAnim());
    }

    public IEnumerator ZoomUpAnim()
    {
        Debug.Log("ZoomUp");
        float timeleft = m_maxTime;
        while (m_card.transform.localScale.x < m_maxScale)
        {
            timeleft -= Time.deltaTime;
            m_card.transform.localScale *= 1+(Time.deltaTime * 2);

            yield return null;
        }
        m_card.transform.localScale = new Vector3(m_maxScale, m_maxScale, m_maxScale);
    }

    public IEnumerator ZoomDownAnim()
    {
        Debug.Log("ZoomDown");
        float timeleft = m_maxTime;
        while (m_card.transform.localScale.x > m_minScale.x)
        {
            m_card.transform.localScale *= 1 - (Time.deltaTime * 2);

            yield return null;
        }
        m_card.transform.localScale = m_minScale;
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
