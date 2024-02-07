using System.Collections.Generic;
using UnityEngine;

public class SC_ChooseChar : MonoBehaviour
{
    public static SC_ChooseChar instance;

    [SerializeField] private So_CardPlayer[] characters;
    [SerializeField] private GameObject[] m_player;
    public Queue<So_CardPlayer> m_characters = new();

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
        MakeQueue(2);
    }

    public void MakeQueue(int PlayerNumber)
    {
        for (int i = 0; i < characters.Length; i++)
        {
            // load every prefab into the queue
            m_characters.Enqueue(characters[i]);
        }
        for (int j = 0; j < PlayerNumber; j++)
        {
            //assign to the card a player character and reassign the values
            m_player[j].GetComponent<Sc_PlayerCardControler>().m_CardInfo = m_characters.Dequeue();
            m_player[j].GetComponent<Sc_PlayerCardControler>().Assign();
        }
    }
    public void SwitchChar(Sc_PlayerCardControler Player_Controller)
    {
        //add to the queue the current character
        m_characters.Enqueue(Player_Controller.m_CardInfo);
        //assign to the card a player character and reassign the values
        Player_Controller.GetComponent<Sc_PlayerCardControler>().m_CardInfo = m_characters.Dequeue();
        Player_Controller.Assign();
    }
}
