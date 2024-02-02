using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sc_BoardManager : MonoBehaviour
{
    public List<So_Card> m_deck;
    public List<So_Card> m_cards;

    public List<GameObject> m_cardPrefabEmplacements;

    public bool m_blendInStart = false;

    private List<So_Card> m_discardDeck = new List<So_Card>();

    private void Start()
    {
        if (m_blendInStart)
            BlendDeck();

        GetCard(3);
    }

    [ContextMenu("Blend deck")]
    public void BlendDeck()
    {
        m_deck = m_deck.OrderBy(x => Random.Range(0, m_deck.Count)).ToList();
    }

    public void GetCard(int n)
    {
        for (int i = 0; i < n; i++)
        {
            m_discardDeck.Add(m_deck[0]);
            m_cards.Add(m_deck[0]);
            m_deck.RemoveAt(0);

            if (m_deck.Count < 1)
                SwitchDescardToDeck();
        }
    }

    private void SwitchDescardToDeck()
    {
        m_discardDeck = m_discardDeck.OrderBy(x => Random.Range(0, m_discardDeck.Count)).ToList();
        m_discardDeck.ForEach(x => m_deck.Add(x));

        m_discardDeck.Clear();
    }
}
