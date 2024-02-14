using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_SurpriseCompetence : Sc_PlayerCompetence
{
    public override void Critique(Sc_EnemyCardControler enemy)
    {
        Debug.Log("crit Surprise");
        if (enemy.Power > 0)
        {
            enemy.Damage += 1;
            enemy.Power -= 1;
        }

    }

    public override void Dodge()
    {
        throw new System.NotImplementedException();
    }
}
