using System;
using System.Collections;
using UnityEngine;

public class SC_CardAnimManager : MonoBehaviour
{
    public static SC_CardAnimManager instance;

    [Header("zoomanim")]
    [SerializeField] private AnimationCurve m_animCurveZoom;
    [SerializeField] private float m_maxScale;
    [SerializeField] private float m_maxTimeScale;
    [SerializeField] private float m_animationSpeed = 3f;
    private Vector3 m_minScale;
    

    [Header("discardanim")]
    [SerializeField] private AnimationCurve m_animCurveDiscard;
    [SerializeField] private GameObject m_canva;
    [SerializeField] private float m_maxTimeDiscard;
    private float cardwidth;
    private Vector3 startpos;

    private void Awake()
    {
        instance = this;
    }
    public float Zoom(GameObject m_card)
    {
        m_minScale = m_card.transform.localScale;
        StartCoroutine(ChangeScale(m_card));
        return m_maxTimeScale;
    }

    private IEnumerator ChangeScale(GameObject m_card)
    {
        float timeleft = m_maxTimeScale;
        while (timeleft >= 0.0f)
        {
            timeleft -= Time.deltaTime;
            m_card.transform.localScale = m_minScale+m_minScale*(m_maxScale-1)*m_animCurveZoom.Evaluate(timeleft/m_maxTimeScale);
            yield return null;
        }
        m_card.transform.localScale = m_minScale;
    }

    public float discard(GameObject m_card, Action m_endAc)
    {
        cardwidth = m_card.GetComponent<RectTransform>().rect.width;
        startpos = m_card.transform.localPosition;
        StartCoroutine(MoveLeft(m_card, m_endAc));
        return(m_maxTimeDiscard);
    }

    private IEnumerator MoveLeft(GameObject m_card, Action m_endAc)
    {
        //float timeleft = m_maxTimeDiscard;
        while (m_card.transform.localPosition.x > -m_canva.GetComponent<RectTransform>().rect.width)
        {
            //timeleft -= Time.deltaTime;
            m_card.transform.localPosition += Vector3.left * m_animationSpeed * Time.deltaTime;
            //m_card.transform.localPosition = startpos + new Vector3(
            //    -m_canva.GetComponent<RectTransform>().rect.width / 2 - cardwidth / 2 - startpos.x,
            //    0
            //) * m_animCurveDiscard.Evaluate(1 - timeleft / m_maxTimeDiscard);
            yield return null;
        }
        m_endAc.Invoke();
    }
}
