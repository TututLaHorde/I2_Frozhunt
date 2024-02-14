using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Fragile", menuName = "Card/Instant/Fragile")]
public class So_Fragile : So_Instant
{
    [Header("Fragile")]
    public int m_damagePlus = -2;

    public override void SelectedCard(GameObject owner)
    {
        Debug.Log("Fragile");

        Sc_FightManager.Instance.m_DamagePlus += m_damagePlus;
        base.SelectedCard(owner);
    }
}
