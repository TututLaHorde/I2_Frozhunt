using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Sc_GameManager : MonoBehaviour
{
    private static Sc_GameManager m_instance = null;
    public static Sc_GameManager Instance => m_instance;

    public int m_currentFood;
    public int m_turnCount;

    [SerializeField] public static int m_foodMax = 20;
    public static int m_turnCountMax = 20;

    public List<Sc_PlayerCardControler> playerList;

    // In scene debug
    public TextMeshProUGUI m_turnText;
    public TextMeshProUGUI m_foodText;
    public TextMeshProUGUI m_playerText;
    public TextMeshProUGUI m_phaseText;

    public int m_testValue;


    private void Awake()
    {
        if (m_instance != null && m_instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            m_instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

        m_currentFood = 0;
        m_turnCount = 1;
    }

    public enum eTurnPhase
    {
        Draw,
        Selection,
        Attack
    }
    public eTurnPhase m_turnPhase = eTurnPhase.Draw;

    public void ToNextPhase()
    {
        switch (m_turnPhase)
        {
            case eTurnPhase.Draw:
                m_turnPhase = eTurnPhase.Selection;
                break;

            case eTurnPhase.Selection:
                m_turnPhase = eTurnPhase.Attack;
                break;

            case eTurnPhase.Attack:
                m_turnPhase = eTurnPhase.Draw;
                break;
        }

        m_phaseText.text = "Phase : " + m_turnPhase.ToString(); // scene debug
    }

    public void ToNextPhase(eTurnPhase phase)
    {
        m_turnPhase = phase;
        m_phaseText.text = "Phase : " + m_turnPhase.ToString(); // scene debug
    }

    public void AddTurn()
    {
        if(m_turnCount < m_turnCountMax)
        {
            m_turnCount++;
        }
        else
        {
            PrintGameOver();
        }

        m_turnText.text = "Turn : " + m_turnCount.ToString(); // scene debug
    }

    
    public int AddFood(int addedFood)
    {
        int res = m_currentFood + addedFood;
        if (res > 0 && res <= m_foodMax)
        {
            m_currentFood += addedFood;
        }
        else if (m_currentFood >= m_foodMax)
        {
            PrintGameOver();
        }

        if (res < 0)
        {
            m_currentFood = 0;
            m_foodText.text = "Food : " + m_currentFood.ToString(); // scene debug
            return res;
        }

        m_foodText.text = "Food : " + m_currentFood.ToString(); // scene debug
        return 0;
    }

    public void AddFoodButton() // void return so it can be tested with a button
    {
        AddFood(m_testValue);
    }

    public void PrintGameOver()
    {
        if(m_currentFood < m_foodMax)
        {
            Debug.Log("Perdu ! Vous avez dépassé le maximum de tours");
        }
        else
        {
            Debug.Log("Victoire ! Vous avez suffisamment de nourriture");
        }
    }

    public void AddPlayer(Sc_PlayerCardControler player)
    {
        if(player != null)
        {
            playerList.Add(player);
            m_playerText.text = "n° players : " + playerList.Count(); // scene debug
        }
    }

    public void AllPlayersTakeDamage(int damage)
    {
        for(int i = 0; i <= playerList.Count; i++)
            playerList[i].TakeDamage(damage);
        
    }
}
