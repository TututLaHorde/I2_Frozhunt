using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_enemyCompetence : MonoBehaviour
{
    public virtual void Competence(System.Action onAnimEnd)
    {
        Sc_FightManager.Instance.MakeEnemyAttackAnimation(onAnimEnd);
    }
}
