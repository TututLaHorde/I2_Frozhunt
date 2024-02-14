using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card/Effet/Keepable/ShinyPebbles")]
public class So_ShinyPebbles : So_Keepable
{
    public override void UseEffect()
    {
    }

    public override void SelectedCard(GameObject owner)
    {
        bool canSet = Sc_BoardManager.Instance.AddEffectCardInHand(this);
        Sc_PbCard c = owner.GetComponent<Sc_PbCard>();

        if (canSet)
        {
            Sc_BoardManager.Instance.InstantiateBonusCard(this);
            Destroy(owner);
        }
        else
        {
            Sc_BoardManager.Instance.RemoveAllPrefabCardWithout(c.m_indexPosition);
        }
    }
}
