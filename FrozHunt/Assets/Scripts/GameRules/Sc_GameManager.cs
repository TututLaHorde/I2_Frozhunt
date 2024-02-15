using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sc_GameManager : MonoBehaviour
{
    public static Sc_GameManager Instance { get; private set; }

    public int m_currentFood;
    public int m_turnCount;

    public int m_foodMax = 20;
    public int m_turnCountMax = 20;

    public List<Sc_PlayerCardControler> playerList;

    public ScriptableObject So_Enemy;

    public UnityEvent m_DrawEvent = new();
    public UnityEvent m_SelectionEvent = new();
    public UnityEvent m_AttackEvent = new();

    [Header("Button")]
    public Button m_draw;
    public List<Button> m_playerButton;

    [Space(10)]

    // In scene debug
    public TextMeshProUGUI m_turnText;
    public TextMeshProUGUI m_foodText;

    public int m_testValue;

    

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        //get number of players
        Sc_SaveData.Instance.LoadFromJson();
        ToNextPhase(eTurnPhase.Draw);

        //init food & turn
        m_currentFood = 0;
        m_turnCount = 0;

        ScaleValues(0);
        m_foodText.text = m_currentFood.ToString() + "/" + m_foodMax; // scene debug
        m_turnText.text =  m_turnCount.ToString() + "/" + m_turnCountMax; // scene debug
    }

    public enum eTurnPhase
    {
        Draw,
        Selection,
        Attack
    }
    public eTurnPhase m_turnPhase = eTurnPhase.Draw;

    public void PhaseButton()
    {
        switch (m_turnPhase)
        {
            case eTurnPhase.Draw:
                m_draw.interactable = true;
                m_DrawEvent.Invoke();
                SetPlayerAttackButton(false);

                break;

            case eTurnPhase.Selection:
                m_draw.interactable = false;
                m_SelectionEvent.Invoke();
                SetPlayerAttackButton(false);
                break;

            case eTurnPhase.Attack:
                m_draw.interactable = false;
                m_AttackEvent.Invoke();
                SetPlayerAttackButton(true);
                break;
        }
    }

    public void SetPlayerAttackButton(bool isInteractible)
    {
        foreach(var b in m_playerButton)
        {
            b.interactable = isInteractible;
        }
    }

    public void ToNextPhase(eTurnPhase phase)
    {
        m_turnPhase = phase;
        PhaseButton();
        Sc_TutorialManager.Instance.ShowTuto();
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

        m_turnText.text = m_turnCount.ToString() + "/" + m_turnCountMax; // scene debug
    }

    
    public int AddFood(int addedFood)
    {
        int res = m_currentFood + addedFood;
        m_currentFood = Mathf.Clamp(res, 0, m_foodMax);
        if (m_currentFood == m_foodMax)
        {
            PrintGameOver();
        }

        m_foodText.text = m_currentFood.ToString() + "/" + m_foodMax; // scene debug
        if (res < 0)
        {
            return res;
        }
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
            SC_MusicManager.Instance.windStop();
            SC_MusicManager.Instance.menuMusic(MenuMusic.gameover);
            SceneManager.LoadScene("DefeatScene");
        }
        else
        {
            SC_MusicManager.Instance.windStop();
            SC_MusicManager.Instance.menuMusic(MenuMusic.victoire);
            SceneManager.LoadScene("VictoryScene");
        }
    }

    public void AddPlayer(Sc_PlayerCardControler player)
    {
        if(player != null)
        {
            playerList.Add(player);
        }
    }

    public void AllPlayersTakeDamage(int damage)
    {
        for(int i = 0; i <= playerList.Count; i++)
            playerList[i].TakeDamage(damage);
        
    }

    public int GetFood() => m_currentFood;


    public int ScaleValues(int enemyHealth)
    {
        //change default value
        int scaledTurn = m_turnCountMax; 
        int scaledFood = m_foodMax;

        float enemyHealthMultiplier = (float)(0.5 + (0.25 * playerList.Count()));
        float scaledEnemyHealth;

        int numberOfPlayers;
        numberOfPlayers = playerList.Count;
        switch (numberOfPlayers)
        {
            case 2:
                scaledTurn = 20;
                scaledFood = 20;
                break;

            case 3:
                scaledTurn = 25;
                scaledFood = 30;
                break;

            case 4:
                scaledTurn = 30;
                scaledFood = 40;
                break;
        }

        m_turnCountMax = scaledTurn;
        m_foodMax = scaledFood;
        scaledEnemyHealth = enemyHealth * enemyHealthMultiplier;
        enemyHealth = Mathf.RoundToInt(scaledEnemyHealth);

        return enemyHealth;
    }

}
