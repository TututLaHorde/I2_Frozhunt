using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_DoubleDmgCritique : Sc_PlayerCompetence
{
    Sc_PlayerCardControler m_player;
    public override void Critique(Sc_EnemyCardControler enemy)
    {
        if (m_player == null)
            m_player = gameObject.GetComponent<Sc_PlayerCardControler>();
        enemy.TakeDamage(m_player.GetDamage());
        Debug.Log("Crit DDmg");
    }

    public override void Dodge()
    {
        throw new System.NotImplementedException();
    }
}
