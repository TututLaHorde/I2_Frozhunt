using TMPro;
using UnityEngine;

public class SC_PlayerCount : MonoBehaviour
{
    public static SC_PlayerCount instance;

    [SerializeField] private TMP_Text m_playerCount;
    [SerializeField] private GameObject m_player;
    [SerializeField] private GameObject[] m_players;
    public int m_count;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            Debug.LogError("Intanced twice");
            return;
        }
    }

    private void Start()
    {
        m_count = 2;
        m_playerCount.text = "Player count " + m_count;
        UpdatePos();
    }


    private void UpdatePos()
    {
        float originMult = -(m_count - 1f) / 2;
        for(int i = 0; i < m_count; i++) 
        {
            m_players[i].transform.localPosition =new Vector3(500f * (originMult + i), m_players[i].transform.localPosition.y);
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
            m_players[m_count].GetComponent<Sc_PlayerCardControler>().m_CardInfo = SC_ChooseChar.instance.m_characters.Dequeue();
            m_players[m_count].GetComponent<Sc_PlayerCardControler>().Assign();
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
            SC_ChooseChar.instance.m_characters.Enqueue(m_players[m_count].GetComponent<Sc_PlayerCardControler>().m_CardInfo);
            UpdatePos();
        }
    }
}
