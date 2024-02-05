using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class Sc_FightManager : MonoBehaviour
{
    public static Sc_FightManager Instance;

    public Sc_EnemyCardControler m_Enemy;
    private Sc_PlayerCardControler m_lastPlayer;

    public GameObject m_pop_up;

    public (int, int) dice;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) { Instance = this; }
        m_pop_up.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartFight(Sc_EnemyCardControler enemy)
    {
        m_Enemy = enemy;
    }

    // m_lastPlayer.CanAttack= true; need To remove this if you want him to stop attack. make him inactive instead
    public void Attack(Sc_PlayerCardControler player)
    {
        if(!player.CanAttack)
            return;

        RandDice();

        m_lastPlayer = player;

        player.CanAttack = false;

        int result = dice.Item1 + dice.Item2;
        Debug.Log(result);
        if(result < m_Enemy.GetPower() && !m_Enemy.Stun) { player.TakeDamage(m_Enemy.GetDamage()); m_lastPlayer.CanAttack = true; }
        else 
        {
            if(m_Enemy.Stun)
                m_Enemy.Stun = false;

            m_Enemy.TakeDamage(player.GetDamage());
            if (dice.Item2 > 4)
            {
                player.Crit(m_Enemy);
                m_lastPlayer.CanAttack = true;
                return;
            }

            player.CanCrit();
            m_pop_up.SetActive(true);
        }

    }

    public void WantToCritique()
    {
        m_lastPlayer.Crit(m_Enemy);
        Debug.Log("Remove Food");
        m_lastPlayer.CanAttack = true;
        m_pop_up.SetActive(false);
    }

    public void DontWantToCritique()
    {
        Debug.Log("Don't crit");
        m_lastPlayer.CanAttack= true;
        m_pop_up.SetActive(false);
    }

    private void RandDice()
    {
        dice.Item1 = Random.Range(1, 7);
        dice.Item2 = Random.Range(1, 7);
    }

}
