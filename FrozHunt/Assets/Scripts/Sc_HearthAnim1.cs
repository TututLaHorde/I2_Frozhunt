using System.Collections;
using UnityEngine;

public class Sc_HearthAnim1 : MonoBehaviour
{
    public float m_CurrentLife = 20;
    public float m_MaxLife = 20;
    public float m_speedSlow = 1.0f;
    [SerializeField] private float m_speedNormal = 1.0f;
    [SerializeField] private float m_speedFast = 1.0f;
    [SerializeField] private float m_speedSuperFast = 1.0f;

    private int m_direction = 1;

    private Vector3 m_NewScale = Vector3.one;

    private float m_pourcentage = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HearthAnim());
    }

    private IEnumerator HearthAnim()
    {
        while (true) 
        {
            m_pourcentage = (m_CurrentLife / m_MaxLife) * 100;

            if (gameObject.transform.localScale.x > 1.5)
                m_direction = -1;
            else if ((gameObject.transform.localScale.x < 1))
                m_direction = 1;


            if (m_pourcentage >= 75)
            { Debug.Log("OK  " + m_pourcentage); }

            else if(m_pourcentage < 75  && m_pourcentage >= 50)
            {
                m_NewScale.Set(
                    gameObject.transform.localScale.x + m_speedSlow * m_direction * Time.deltaTime,
                    gameObject.transform.localScale.y + m_speedSlow * m_direction* Time.deltaTime,
                    gameObject.transform.localScale.z + m_speedSlow * m_direction* Time.deltaTime
                    );
                gameObject.transform.localScale = m_NewScale;
                Debug.Log("Slow  " + m_pourcentage);
            }

            else if (m_pourcentage < 50 && m_pourcentage >= 25)
            {
                m_NewScale.Set(
                    gameObject.transform.localScale.x + m_speedNormal * m_direction* Time.deltaTime,
                    gameObject.transform.localScale.y + m_speedNormal * m_direction* Time.deltaTime,
                    gameObject.transform.localScale.z + m_speedNormal * m_direction* Time.deltaTime
                    );
                gameObject.transform.localScale = m_NewScale;
                Debug.Log("Normal   " + m_pourcentage);
            }

            else if (m_pourcentage < 25 && m_pourcentage >= 10)
            {
                m_NewScale.Set(
                    gameObject.transform.localScale.x + m_speedFast * m_direction* Time.deltaTime,
                    gameObject.transform.localScale.y + m_speedFast * m_direction* Time.deltaTime,
                    gameObject.transform.localScale.z + m_speedFast * m_direction* Time.deltaTime
                    );
                gameObject.transform.localScale = m_NewScale;
                Debug.Log("Fast  "+ m_pourcentage);
            }
            else
            {
                m_NewScale.Set(
                    gameObject.transform.localScale.x + m_speedSuperFast * m_direction* Time.deltaTime,
                    gameObject.transform.localScale.y + m_speedSuperFast * m_direction* Time.deltaTime,
                    gameObject.transform.localScale.z + m_speedSuperFast * m_direction* Time.deltaTime
                    );
                gameObject.transform.localScale = m_NewScale;
                Debug.Log("Super Fast  " + m_pourcentage);
            }


            yield return null;
        
        }
    }
}
