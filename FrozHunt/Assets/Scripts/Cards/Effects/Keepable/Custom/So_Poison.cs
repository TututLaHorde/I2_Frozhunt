using UnityEngine;


[CreateAssetMenu(fileName = "Poison", menuName = "Card/Keepable/Poison")]
public class So_Poison : So_Consumable
{
    [Header("Poison")]
    public int m_numberOfPowerToLower = 1;

    public override void UseEffect()
    {
        Sc_FightManager.Instance.m_Enemy.Power -= m_numberOfPowerToLower;
    }
}
