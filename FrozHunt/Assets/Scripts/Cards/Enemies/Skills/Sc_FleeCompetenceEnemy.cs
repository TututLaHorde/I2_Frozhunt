using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeCompetenceEnemy : Sc_enemyCompetence
{
    public override void Competence() { Debug.Log("Enemy Flee"); Sc_FightManager.Instance.EndFight(); }
}
