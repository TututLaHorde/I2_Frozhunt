using Unity.VisualScripting;
using UnityEngine;

public class Sc_HandCard : MonoBehaviour
{
    public bool m_enable = true;
    public bool m_useActive = true;

    public So_Effect m_effectCard;
    public int m_indexPosition;

    public void SetActiveHandCardButton(bool enable)
    {
        m_enable = enable;
        m_useActive = enable;

        Sc_PbCard card = transform.GetChild(1).GetComponent<Sc_PbCard>();
        card.SetGreyScreen(enable);
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
        if (!m_useActive)
            return;

        if (!m_effectCard)
            return;

        m_effectCard.UseEffect();

        Sc_PopUpManager.Instance.SetIndexCardBonus(m_indexPosition);

        if (m_effectCard.m_canDiscardCard && m_effectCard.m_destroyOnUse)
            Sc_BoardManager.Instance.RemoveBonusCard(m_indexPosition);
    }
}
