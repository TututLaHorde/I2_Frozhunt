using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_HandCard : MonoBehaviour
{
    public So_Effect m_effectCard;
    public int m_indexPosition;

    public void RemoveEffectCard()
    {
        if (m_effectCard.m_canDiscardCard)
            Sc_BoardManager.Instance.RemoveBonusCard(m_indexPosition);
    }

    public void UseEffectCard()
    {
        m_effectCard?.UseEffect();
        Sc_BoardManager.Instance.RemoveBonusCard(m_indexPosition);
    }
}
