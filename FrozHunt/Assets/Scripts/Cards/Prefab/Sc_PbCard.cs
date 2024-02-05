using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_PbCard : MonoBehaviour
{
    [HideInInspector] public So_Card m_card;

    public bool m_canClick = true;

    public virtual void InitDisplayCard(So_Card c)
    {
        m_card = c;
    }
    public virtual void UseCard()
    {
        if (m_canClick)
        {
            StartCoroutine(UseCardAfterTimer());
            Sc_BoardManager.Instance.SetEnableCard(false);
        }
    }

    private IEnumerator UseCardAfterTimer()
    {
        yield return new WaitForSeconds(1f);
        m_card.SelectedCard();
    }
}
