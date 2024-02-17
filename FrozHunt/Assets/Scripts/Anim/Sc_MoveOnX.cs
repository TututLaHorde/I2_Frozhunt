using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Sc_MoveOnX : MonoBehaviour
{
    public enum m_State
    {
        Show, Hide, Nothing
    }

    private Vector3 m_FirstPosition = new();
    private float m_time = 0;

    public float m_MovementInX = 0;
    public float m_Speed = 0;

    [Header("Active OnEvent")]
    public m_State m_EventDraw, m_EventSelection, m_EventAttack = m_State.Nothing;

    private Transform m_Transform;
    private Vector3 m_HidePos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        m_Transform = gameObject.transform;

        m_FirstPosition = m_Transform.localPosition;
        m_HidePos.Set(m_FirstPosition.x + m_MovementInX, m_Transform.localPosition.y, m_Transform.localPosition.z);

        MyListeners(m_EventDraw, 0);
        MyListeners(m_EventSelection, 1);
        MyListeners(m_EventAttack, 2);
    }

    private void MyListeners(m_State state,int index)
    {
            switch (state)
            {
                case m_State.Show:
                    GetEvent(index).AddListener(ShowObject);
                    break;

                case m_State.Hide:
                    GetEvent(index).AddListener(HideObject);
                break;

                case m_State.Nothing:
                    break;
            }
    }

    private UnityEvent GetEvent(int index)
    {
        switch(index) 
        {
            case 0:
                return Sc_GameManager.Instance.m_DrawEvent;

            case 1:
                return Sc_GameManager.Instance.m_SelectionEvent;

            case 2:
                return Sc_GameManager.Instance.m_AttackEvent;
        
        }
        return null;
    }


    public void ShowObject()
    {
        StartCoroutine(Show());
    }
    public void HideObject()
    {
        StartCoroutine (Hide());
    }

    private IEnumerator Show()
    {
        m_time = Time.time;

        while (m_Transform.localPosition.x < m_FirstPosition.x)
        {
            m_Transform.localPosition = Vector3.Lerp(m_HidePos, m_FirstPosition, (Time.time - m_time) * m_Speed);
            Debug.Log("Show");
            yield return null;
        }


        yield return null;
    }
    private IEnumerator Hide()
    {
        m_time = Time.time;


        while (m_Transform.localPosition.x > m_HidePos.x)
        {
            m_Transform.localPosition = Vector3.Lerp(m_FirstPosition, m_HidePos, (Time.time - m_time)*m_Speed);
            Debug.Log("Hide");
            yield return null;
        }


        yield return null;
    }
}
