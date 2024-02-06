using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SC_PlayerCount : MonoBehaviour
{
    [SerializeField] private TMP_Text m_playerCount;
    [SerializeField] private GameObject m_player;
    [SerializeField] private GameObject[] m_players;
    private int m_count;

    private void Start()
    {
        m_count = 2;
        m_playerCount.text = "Player count " + m_count;
        UpdatePos();
    }


    private void UpdatePos()
    {
        int originMult = -(m_count - 1) / 2;
        for(int i = 0; i < m_count; i++) 
        {
            m_players[i].transform.position = new Vector3(100 * (originMult + i), m_players[i].transform.position.y);
        }
    }
    public void Increase()
    {
        if (m_count < 4)
        {
            if (!m_players[m_count].activeSelf)
            {
                m_players[m_count].SetActive(true);
            }
            m_count++;
            m_playerCount.text = "Player count " + m_count;
            UpdatePos();
        }  
    }

    public void Decrease()
    {
        if (m_count > 2)
        {
            m_count--;
            m_playerCount.text = "Player count " + m_count;
            if (m_players[m_count].activeSelf)
            {
                m_players[m_count].SetActive(false);
            }
            UpdatePos();
        }
    }
}
