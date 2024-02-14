using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_FireCompetence : Sc_PlayerCompetence
{
    public override void Critique(Sc_EnemyCardControler enemy)
    {
        Debug.Log("crit Fire");
        if (enemy.Damage > 1)
        {
            enemy.Damage -= 1;
        }
    }

    public override void Dodge()
    {
        throw new System.NotImplementedException();
    }
}
