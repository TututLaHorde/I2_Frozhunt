using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_BoardCardAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;

    [Header("Turn Animation")]
    [SerializeField] private GameObject m_backCard;
    [SerializeField] private float m_animationSpeed = 2f;

    [Header("Move Animation")]
    [SerializeField] private AnimationCurve m_xMoveAnimationCurve;
    [SerializeField] private AnimationCurve m_yMoveAnimationCurve;
    [SerializeField] private AnimationCurve m_zRotateAnimationCurve;
    [SerializeField] private Vector2 m_startMovePosition = new Vector2(-900f, 800f);

    public void StartRotateAnimation()
    {
        StartCoroutine(RotateAnimationCard());
    }
    public void StartMoveAnimation(Action actionAfterMove)
    {
        StartCoroutine(MoveAnimationCard(actionAfterMove));
    }

    private IEnumerator RotateAnimationCard()
    {
        float timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime * m_animationSpeed;
            transform.eulerAngles = Vector3.Lerp(Vector3.zero, new Vector3(0f, -90f, 0f), timer);
            yield return null;
        }
        m_backCard.SetActive(false);

        timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime * m_animationSpeed;
            transform.eulerAngles = Vector3.Lerp(new Vector3(0f, -90f, 0f), Vector3.zero, timer);
            yield return null;
        }
    }
    private IEnumerator MoveAnimationCard(Action actionAfterMove)
    {
        float timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime;

            float resultXCurve = m_xMoveAnimationCurve.Evaluate(timer);
            float resultYCurve = m_yMoveAnimationCurve.Evaluate(timer);

            transform.localPosition = new Vector3(
                Mathf.Lerp(m_startMovePosition.x, 0f, resultXCurve),
                Mathf.Lerp(m_startMovePosition.y, 0f, resultYCurve),
                0f
            );

            float resultZCurve = m_zRotateAnimationCurve.Evaluate(timer);
            rectTransform.rotation = Quaternion.Euler(
                rectTransform.rotation.x,
                rectTransform.rotation.y,
                Mathf.Lerp(-90f, 0f, resultZCurve)
            );

            yield return null;
        }

        actionAfterMove?.Invoke();
    }
}
