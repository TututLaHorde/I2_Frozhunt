using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_DoubleDmgCritique : Sc_PlayerCompetence
{
    Sc_PlayerCardControler m_player;

    private void Start()
    {
        m_player = gameObject.GetComponent<Sc_PlayerCardControler>();
    }

    public override void Critique(Sc_EnemyCardControler enemy)
    {
        enemy.TakeDamage(m_player.GetDamage());       
        Debug.Log("Crit DDmg");
    }

    public override void Dodge()
    {
        throw new System.NotImplementedException();
    }
}
