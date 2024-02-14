using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_BoostCompetence : Sc_PlayerCompetence
{
    Sc_PlayerCardControler m_player;
    private void Start()
    {
        m_player = gameObject.GetComponent<Sc_PlayerCardControler>();
    }
    public override void Critique(Sc_EnemyCardControler enemy)
    {
        enemy.TakeDamage(m_player.m_CardInfo.life - m_player.GetCurrentHealth);
        Debug.Log("Crit Boost " + (m_player.m_CardInfo.life - m_player.GetCurrentHealth));

    }

    public override void Dodge()
    {
        throw new System.NotImplementedException();
    }
}
