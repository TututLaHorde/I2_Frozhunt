using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class Sc_FightManager : MonoBehaviour
{
    public static Sc_FightManager Instance;

    public Sc_EnemyCardControler m_Enemy;
    private Sc_PlayerCardControler m_Player;

    public (int, int) dice;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) { Instance = this; }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartFight(Sc_EnemyCardControler enemy)
    {
        m_Enemy = enemy;
    }
    public void Attack(Sc_PlayerCardControler player)
    {
        RandDice();
        int result = dice.Item1 + dice.Item2;
        Debug.Log(result);
        if(result < m_Enemy.GetPower()) { player.TakeDamage(m_Enemy.GetDamage()); }
        else 
        {
            m_Enemy.TakeDamage(player.GetDamage());
            if (dice.Item2 > 4)
            {
                player.Crit();
                return;
            }

            player.CanCrit();
        }

    }

    private void RandDice()
    {
        dice.Item1 = Random.Range(1, 7);
        dice.Item2 = Random.Range(1, 7);
    }

}
