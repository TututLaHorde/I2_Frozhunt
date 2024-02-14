using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoneThrow", menuName = "Card/Keepable/StoneThrow")]
public class So_StoneThrow : So_Consumable
{
    [Header("StoneThrow")]
    public int m_damage = 3;

    public override void UseEffect()
    {
        Debug.Log("StoneThrow");
        Sc_FightManager.Instance.m_Enemy.TakeDamage(m_damage);
    }
}
