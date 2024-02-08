using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_HandCard : MonoBehaviour
{
    public bool m_enable = true;

    public So_Effect m_effectCard;
    public int m_indexPosition;

    public void SetActiveHandCardButton(bool enable)
    {
        m_enable = enable;
        GetComponent<Sc_Button>().enabled = enable;
    }

    public void RemoveEffectCard()
    {
        if (!m_effectCard)
            return;

        if (m_effectCard.m_canDiscardCard)
            Sc_BoardManager.Instance.RemoveBonusCard(m_indexPosition);
    }
    public void UseEffectCard()
    {
        if (!m_effectCard)
            return;

        m_effectCard.UseEffect();

        Sc_PopUpManager.Instance.SetIndexCardBonus(m_indexPosition);

        if (m_effectCard.m_canDiscardCard && m_effectCard.m_destroyOnUse)
            Sc_BoardManager.Instance.RemoveBonusCard(m_indexPosition);
    }
}
