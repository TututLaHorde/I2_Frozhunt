using System;
using UnityEngine;

public class Sc_PopUpMovement : MonoBehaviour
{
    public float m_speed = 60f;

    private int m_dir = -1;

    public Int32 min;
    public Int32 max;

    public bool m_OnXAxis = true;

    private void Update()
    {
        Vector3 newPos;

        if(m_OnXAxis)
        {
            newPos = transform.position + new Vector3(m_dir * m_speed * Time.deltaTime, 0, 0);
            transform.position = newPos;

            if (transform.position.x < min)
                m_dir = 1;

            if (transform.position.x > max)
                m_dir = -1;
        }
        else
        {
            newPos = transform.position + new Vector3(0, m_dir * m_speed * Time.deltaTime, 0);
            transform.position = newPos;

            if (transform.position.y < min)
                 m_dir = 1;
            
            if (transform.position.y > max)
                m_dir = -1;
            
        }
        
    }
}
