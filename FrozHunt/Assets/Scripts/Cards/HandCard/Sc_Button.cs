using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Sc_Button : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent m_rightClickButtonEvent;
    public UnityEvent m_leftClickButtonEvent;
    public UnityEvent m_middleClickButtonEvent;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            m_leftClickButtonEvent?.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Middle)
            m_middleClickButtonEvent?.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Right)
            m_rightClickButtonEvent?.Invoke();
    }
}
