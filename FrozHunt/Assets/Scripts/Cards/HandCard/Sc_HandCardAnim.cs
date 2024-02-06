using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_HandCardAnim : MonoBehaviour
{
    [SerializeField] private RectTransform m_rectTransform;

    [SerializeField] private float m_upPositionValue   = 100f;
    [SerializeField] private float m_downPositionValue = -30f;
    [SerializeField] private float m_animationSpeed = 170f;

    private Coroutine m_animationCoroutine;

    public void UpCardAnimation()
    {
        StopAnimationCoroutine();
        m_animationCoroutine = StartCoroutine(UpAnimationCoroutine());
    }
    public void DownCardAnimation() 
    {
        StopAnimationCoroutine();
        m_animationCoroutine = StartCoroutine(DownAnimationCoroutine());
    }

    private void StopAnimationCoroutine()
    {
        if (m_animationCoroutine != null)
            StopCoroutine(m_animationCoroutine);
    }

    private IEnumerator UpAnimationCoroutine()
    {
        while (m_rectTransform.localPosition.y < m_upPositionValue)
        {
            m_rectTransform.localPosition = new Vector3(
                m_rectTransform.localPosition.x,
                m_rectTransform.localPosition.y + m_animationSpeed * Time.deltaTime,
                m_rectTransform.localPosition.z
            );

            yield return null;
        }
    }
    private IEnumerator DownAnimationCoroutine()
    {
        while (m_rectTransform.position.y > m_downPositionValue)
        {
            m_rectTransform.localPosition = new Vector3(
                m_rectTransform.localPosition.x,
                m_rectTransform.localPosition.y - m_animationSpeed * Time.deltaTime,
                m_rectTransform.localPosition.z
            );

            yield return null;
        }
    }
}
