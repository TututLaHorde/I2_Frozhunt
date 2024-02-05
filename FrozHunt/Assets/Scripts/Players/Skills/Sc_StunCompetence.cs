using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_StunCompetence : Sc_PlayerCompetence
{
    public override void Critique(Sc_EnemyCardControler enemy)
    {
        Debug.Log("crit stun");
        enemy.Stun = true;
    }

    public override void Dodge()
    {
        throw new System.NotImplementedException();
    }
}
