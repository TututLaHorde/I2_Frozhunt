using System.Collections;
using TMPro;
using UnityEngine;

public class Sc_FightManager : MonoBehaviour
{
    public static Sc_FightManager Instance;

    // Make the enemy private when the enemy can be set with STARTFIGHT()
    public Sc_EnemyCardControler m_Enemy;
    public Sc_PlayerCardControler m_lastPlayer;

    [Header("Pop-Up")]
    public GameObject m_pop_up;
    public TextMeshProUGUI m_textPopUp;
    public string m_textPopUpFood;
    public string m_textPopUpLife;
    public Sc_InfoDicePopUp m_infoDicePopUp;

    [Header("Dice")]
    public SC_Dice m_dice;
    public SC_Dice m_diceCrit;

    public (int, int) m_diceResult;

    private int m_numberOfPlayerAttack = 0;
    public int m_enragedPlayerBonus;

    private bool m_canAttack = true;

    public bool m_isCrit = false;
    public int m_damage = 0;

    public bool m_IsPlayerAttack = false;


    void Start()
    {
        if (Instance == null) { Instance = this; }
        m_pop_up.GetComponent<Sc_MoveOnX>().ShowObject();
    }

    // Call when an enemy card is choose for set the enemy 
    public void StartFight(Sc_EnemyCardControler enemy)
    {
        Sc_GameManager.Instance.ToNextPhase(Sc_GameManager.eTurnPhase.Attack);
        m_Enemy = enemy;
        m_canAttack = true;
        //m_infoDicePopUp.InitPopUp(true);

    }

    // m_lastPlayer.CanAttack= true; need To remove this if you want him to stop attack. make him inactive instead


    // Call in the button of the card
    public void Attack(Sc_PlayerCardControler player)
    {
        if (m_canAttack)
        {
            StartCoroutine(AttackCoroutine(player));
        }
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

            if (m_numberOfPlayerAttack == Sc_GameManager.Instance.playerList.Count)
            {
                AllPlayerCanAttack();
            }

            int result = m_diceResult.Item1 + m_diceResult.Item2;
            Debug.Log(result + "     ////    " + m_diceResult.Item2);

            m_infoDicePopUp.SetPopUps(true);
            Sc_TutorialManager.Instance.m_confirmAttackWindow.SetActive(Sc_TutorialManager.Instance.m_isFirstFight);
            if(result < m_Enemy.GetPower() && !m_Enemy.Stun)
            {
                m_infoDicePopUp.SetAttackStateText(AttackState.Failure);
                m_infoDicePopUp.SetAbilityStateText(AbilityState.Nothing);
                m_IsPlayerAttack = true;
                Sc_BoardManager.Instance.SetActiveSpecialHandCardWithTag(true, "ActiveOnFailureAttack");
            }
            else
            {
                m_infoDicePopUp.SetAttackStateText(AttackState.Success);
                if (m_diceResult.Item2 > 4)
                {
                    m_infoDicePopUp.SetAbilityStateText(AbilityState.CriticalAttack);
                    m_isCrit = true;
                }
                else
                {
                    if (Sc_GameManager.Instance.GetFood() > 0)
                        m_textPopUp.text = m_textPopUpFood;
                    else
                        m_textPopUp.text = m_textPopUpLife;

                    m_pop_up.GetComponent<Sc_MoveOnX>().HideObject();
                    m_infoDicePopUp.SetAbilityStateText(AbilityState.Nothing);
                }

            }

            if(m_Enemy.Stun)
            {
                m_Enemy.Stun = false;
            }
        }
        else
        {
            m_infoDicePopUp.SetAttackStateText(AttackState.Success);
            if (m_diceResult.Item2 > 4)
            {
                m_infoDicePopUp.SetAbilityStateText(AbilityState.CriticalAttack);
                m_isCrit = true;
            }
            else
            {
                if (Sc_GameManager.Instance.GetFood() > 0)
                    m_textPopUp.text = m_textPopUpFood;
                else
                    m_textPopUp.text = m_textPopUpLife;
                m_pop_up.GetComponent<Sc_MoveOnX>().HideObject();

                //m_pop_up.SetActive(true);
                m_infoDicePopUp.SetAbilityStateText(AbilityState.Nothing);
            }

        }
        if(m_Enemy.Stun)
        {
            m_Enemy.Stun = false;
        }
    }
    

    public void MakeAttackAnimation()
    {
        if (!m_IsPlayerAttack)
        {
            StartCoroutine(m_lastPlayer.gameObject.GetComponent<Sc_AnimAttackPlayer>().AnimAttack());
        }
        else
        {

            Sc_AnimAttackPlayer temp = m_Enemy.gameObject.GetComponent<Sc_AnimAttackPlayer>();
            temp.m_EnemyPosition = m_lastPlayer.gameObject;
            temp.m_ShakeObject = GameObject.FindGameObjectWithTag("Shake");
            temp.m_CardToAnim = m_Enemy.gameObject;
            temp.SetFirstPos(m_Enemy.gameObject.transform.localPosition);
            StartCoroutine(temp.AnimAttack());
        }
    }

    public void TriggerEffect()
    {
        if(!m_IsPlayerAttack)
        {
            m_damage += m_lastPlayer.GetDamage();
            if (m_isCrit)
                m_lastPlayer.Crit(m_Enemy);
            m_Enemy.TakeDamage(m_damage);
        }
        else 
        {
            m_Enemy.Competence();
            m_lastPlayer.TakeDamage(m_Enemy.GetDamage());
        }
        
        m_canAttack = true;
        m_enragedPlayerBonus = 0;
        m_infoDicePopUp.SetPopUps(false);
        Sc_TutorialManager.Instance.m_isFirstFight = false;
        Sc_TutorialManager.Instance.m_confirmAttackWindow.SetActive(Sc_TutorialManager.Instance.m_isFirstFight);
        m_pop_up.GetComponent<Sc_MoveOnX>().ShowObject();
        m_isCrit = false;
        m_damage = 0;
        m_IsPlayerAttack = false;
    }

    private void AllPlayerCanAttack()
    {
        m_numberOfPlayerAttack = 0;
        for (int i = 0; i < Sc_GameManager.Instance.playerList.Count; i++)
        {
            Debug.Log("I m called");
            Sc_GameManager.Instance.playerList[i].SetAttackActive();
        }

    }

    public void WantToCritique() // The Player clique on Yes on the critical pop_up
    {
        if(m_Enemy!=null)
        {
            m_infoDicePopUp.SetAbilityStateText(AbilityState.CriticalAttack);

            if (Sc_GameManager.Instance.GetFood() > 0)
                Sc_GameManager.Instance.AddFood(-1);
            else
                m_lastPlayer.TakeDamage(2);

            Debug.Log("Remove Food");

        }

        m_pop_up.GetComponent<Sc_MoveOnX>().ShowObject();
        m_isCrit = true;
    }

    public void DontWantToCritique() // The Player clique on No on the critical pop_up
    {
        Debug.Log("Don't crit");
        m_pop_up.GetComponent<Sc_MoveOnX>().ShowObject();
    }


    // Trow the Two attack Dice
    private float RandDice()
    {
        m_dice.ThrowDice(ref m_diceResult.Item1);
        return m_diceCrit.ThrowDice(ref m_diceResult.Item2);

    }

    public void EndFight()
    {
        Sc_BoardManager.Instance.SetActiveSpecialHandCardWithTag(false, "DesactiveOnEndFight");
        m_infoDicePopUp.SetPopUps(false);
        AllPlayerCanAttack();
        Debug.Log("END OF THE FIGHT");
        Sc_GameManager.Instance.ToNextPhase(Sc_GameManager.eTurnPhase.Draw);
        Sc_BoardManager.Instance.RemoveAllPrefabCard();
        //m_infoDicePopUp.InitPopUp(false);
    }
}
