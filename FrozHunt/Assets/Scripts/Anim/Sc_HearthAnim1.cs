using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Sc_HearthAnim1 : MonoBehaviour
{
    public float m_CurrentLife = 20;
    public float m_MaxLife = 20;
    public float m_speedSlow = 1.0f;
    [SerializeField] private float m_speedNormal = 2.0f;
    [SerializeField] private float m_speedFast = 4.0f;
    [SerializeField] private float m_speedSuperFast = 8.0f;

    private int m_direction = 1;

    private Vector3 m_NewScale = Vector3.one;

    private float m_pourcentage = 0;

    void Start()
    {
        StartCoroutine(HearthAnim());
    }

    private IEnumerator HearthAnim()
    {
        while (true) 
        {
            m_pourcentage = (m_CurrentLife / m_MaxLife) * 100;

            //anim is scaling or discaling
            if (transform.localScale.x > 1.5)
                m_direction = -1;
            else if (transform.localScale.x < 1)
                m_direction = 1;


            if (m_pourcentage >= 75)
            {
                //no heart anim
                transform.localScale = Vector3.one;
            }
            else if(m_pourcentage < 75  && m_pourcentage >= 50)
            {
                //slow heart anim
                ChangeHeartScale(m_speedSlow, Time.deltaTime);
            }
            else if (m_pourcentage < 50 && m_pourcentage >= 25)
            {
                //normal heart anim
                ChangeHeartScale(m_speedNormal, Time.deltaTime);
            }
            else if (m_pourcentage < 25 && m_pourcentage >= 10)
            {
                //fast heart anim
                ChangeHeartScale(m_speedFast, Time.deltaTime);
            }
            else
            {
                //super fast heart anim
                ChangeHeartScale(m_speedSuperFast, Time.deltaTime);
            }

            yield return null;       
        } 
    }

    private void ChangeHeartScale(float speed, float deltaTime)
    {
        Transform trs = transform;
        m_NewScale.Set(
            trs.localScale.x + speed * m_direction * deltaTime,
            trs.localScale.y + speed * m_direction * deltaTime,
            trs.localScale.z + speed * m_direction * deltaTime
            );
        trs.localScale = m_NewScale;
    }

}
