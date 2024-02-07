using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enraged", menuName = "Card/Keepable/Enraged")]
public class So_Enraged : So_Passive
{
    public override void UseEffect()
    {
        Sc_PlayerCardControler p = Sc_FightManager.Instance.m_lastPlayer;
        Sc_FightManager.Instance.m_enragedPlayerBonus = 1;

        if (Sc_GameManager.Instance.GetFood() > 1)
            Sc_GameManager.Instance.AddFood(-1);
        else
            p.TakeDamage(2);
    }

    public override void SelectedCard(GameObject owner)
    {
        base.SelectedCard(owner);    
    }
}
