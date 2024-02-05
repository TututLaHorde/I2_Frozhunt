using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_PbCard : MonoBehaviour
{
    [HideInInspector] public So_Card m_card;

    public virtual void InitDisplayCard(So_Card c)
    {
        m_card = c;
    }
    public virtual void UseCard()
    {
        m_card.SelectedCard();
    }
}
