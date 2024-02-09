using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sc_HandCardAnim : MonoBehaviour
{
    [SerializeField] private RectTransform m_rectTransform;

    [SerializeField] private float m_upPositionValue   = 100f;
    [SerializeField] private float m_downPositionValue = -30f;
    [SerializeField] private float m_animationSpeed = 2000f;

    public UnityEvent m_onCardUp;
    public UnityEvent m_onCardDown;

    private Coroutine m_animationCoroutine;

    private void Start()
    {
        SetDownPosition();
    }

    private void OnMouseEnter()
    {
        Debug.Log("Enter");
    }
    private void OnMouseExit()
    {
        Debug.Log("Exits");
    }

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
    
    private void SetDownPosition()
    {
        m_rectTransform.localPosition = new Vector3(
            m_rectTransform.localPosition.x,
            m_downPositionValue,
            m_rectTransform.localPosition.z
        );
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
        m_onCardUp?.Invoke();
    }
    private IEnumerator DownAnimationCoroutine()
    {
        while (m_rectTransform.localPosition.y - m_downPositionValue > .3f)
        {
            m_rectTransform.localPosition = new Vector3(
                m_rectTransform.localPosition.x,
                m_rectTransform.localPosition.y - m_animationSpeed * Time.deltaTime,
                m_rectTransform.localPosition.z
            );

            yield return null;
        }
        m_onCardDown?.Invoke();
        SetDownPosition();
    }
}
