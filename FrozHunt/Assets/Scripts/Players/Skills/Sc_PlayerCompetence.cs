using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sc_PlayerCompetence : MonoBehaviour
{
    public abstract void Critique(Sc_EnemyCardControler enemy);
    public abstract void Dodge();
}
