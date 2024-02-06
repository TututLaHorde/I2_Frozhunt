using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sc_FightManager : MonoBehaviour
{
    public static Sc_FightManager Instance;

    // Make the enemy private when the enemy can be set with STARTFIGHT()
    public Sc_EnemyCardControler m_Enemy;
    private Sc_PlayerCardControler m_lastPlayer;

    [Header("Pop-Up")]
    public GameObject m_pop_up;
    public TextMeshProUGUI m_textPopUp;
    public string m_textPopUpFood;
    public string m_textPopUpLife;

    [Header("Dice")]
    public SC_Dice m_dice;
    public SC_Dice m_diceCrit;

    private (int, int) m_diceResult;

    private int m_numberOfPlayerAttack = 0;

    private bool m_canAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) { Instance = this; }
        m_pop_up.SetActive(false);

        //Test need to be delete
        StartFight(m_Enemy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Call when an enemy card is choose for set the enemy 
    public void StartFight(Sc_EnemyCardControler enemy)
    {
        Sc_GameManager.Instance.ToNextPhase(Sc_GameManager.eTurnPhase.Attack);
        m_Enemy = enemy;
        m_canAttack = true;
    }

    // m_lastPlayer.CanAttack= true; need To remove this if you want him to stop attack. make him inactive instead


    // Call in the button of the card
    public void Attack(Sc_PlayerCardControler player)
    {
        StartCoroutine(AttackCoroutine(player));
    }


    // Attack Coroutine for attack when the dice animation is finish
    public IEnumerator AttackCoroutine(Sc_PlayerCardControler player)
    {
        if (player.CanAttack && m_canAttack)
        {
            m_canAttack = false;
            player.SetAttackInactive();
            m_lastPlayer = player;
            m_numberOfPlayerAttack++;

            yield return new WaitForSeconds(RandDice() + 1f);

            if(m_numberOfPlayerAttack == Sc_GameManager.Instance.playerList.Count)
            {
                AllPlayerCanAttack();
            }

            int result = m_diceResult.Item1 + m_diceResult.Item2;
            Debug.Log(result + "     ////    " + m_diceResult.Item2);
            if (result < m_Enemy.GetPower() && !m_Enemy.Stun) 
            {
                player.TakeDamage(m_Enemy.GetDamage());
                m_Enemy.Competence();
                m_canAttack = true;
            }
            else
            {
                if (m_Enemy.Stun)
                    m_Enemy.Stun = false;

                m_Enemy.TakeDamage(player.GetDamage());
                if (m_diceResult.Item2 > 4)
                {
                    player.Crit(m_Enemy);
                    m_canAttack = true;
                    yield break;  
                }

                player.CanCrit();
                if (Sc_GameManager.Instance.GetFood() > 0)
                    m_textPopUp.text = m_textPopUpFood;
                else
                    m_textPopUp.text = m_textPopUpLife;
                m_pop_up.SetActive(true);

            }
        }

    }

    private void AllPlayerCanAttack()
    {
        m_numberOfPlayerAttack = 0;
        for (int i = 0; i < Sc_GameManager.Instance.playerList.Count; i++)
        {
            Sc_GameManager.Instance.playerList[i].SetAttackActive();
        }

    }

    public void WantToCritique() // The Player clique on Yes on the critical pop_up
    {
        m_lastPlayer.Crit(m_Enemy);
        if (Sc_GameManager.Instance.GetFood() > 0)
            Sc_GameManager.Instance.AddFood(-1);
        else
            m_lastPlayer.TakeDamage(2);
        Debug.Log("Remove Food");
        m_pop_up.SetActive(false);
        m_canAttack = true;
    }

    public void DontWantToCritique() // The Player clique on No on the critical pop_up
    {
        Debug.Log("Don't crit");
        m_pop_up.SetActive(false);
        m_canAttack = true;
    }


    // Trow the Two attack Dice
    private float RandDice()
    {
        m_dice.ThrowDice(ref m_diceResult.Item1);
        return m_diceCrit.ThrowDice(ref m_diceResult.Item2);

    }

    public void EndFight()
    {
        AllPlayerCanAttack();
        Debug.Log("END OF THE FIGHT");
        Sc_GameManager.Instance.ToNextPhase(Sc_GameManager.eTurnPhase.Draw);
    }
}
