using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Poison", menuName = "Card/Keepable/Poison")]
public class So_Poison : So_Consumable
{
    [Header("Poison")]
    public int m_numberOfPowerToLower = 1;

    public override void UseEffect()
    {
        Debug.Log("Poison");
        Sc_FightManager.Instance.m_Enemy.Power -= m_numberOfPowerToLower;
    }
}
