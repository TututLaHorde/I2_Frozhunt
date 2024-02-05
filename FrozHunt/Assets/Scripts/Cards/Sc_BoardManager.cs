using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sc_BoardManager : MonoBehaviour
{
    public List<So_Card> m_deck;
    public List<So_Card> m_cards;
    public List<So_Effect> m_effectCard;

    public List<GameObject> m_cardPrefabEmplacements;
    public List<GameObject> m_bonusCardPrefabEmplacements;

    public bool m_blendInStart = false;
    public SC_Dice m_dice;

    private List<So_Card> m_discardDeck = new List<So_Card>();
    private int m_bonusCardNumber = 0;

    public static Sc_BoardManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        if (m_blendInStart)
            BlendDeck();
    }

    [ContextMenu("Blend deck")]
    public void BlendDeck()
    {
        m_deck = m_deck.OrderBy(x => Random.Range(0, m_deck.Count)).ToList();
    }

    public void GetCard()
    {
        int r = 0;
        float t = m_dice.ThrowDice(ref r);
        StartCoroutine(GetCardAfterTimer(t + .5f, r));
    }
    private IEnumerator GetCardAfterTimer(float t, int r)
    {
        yield return new WaitForSeconds(t);
        GetCard(r);
    }
    public void GetCard(int n)
    {
        for (int i = 0; i < n; i++)
        {
            m_discardDeck.Add(m_deck[0]);
            m_cards.Add(m_deck[0]);

            GameObject e = m_cardPrefabEmplacements[i];
            GameObject inst = Instantiate(m_deck[0].m_prebafInBoard, e.transform);
            Sc_PbCard c = inst.GetComponent<Sc_PbCard>();
            c.InitDisplayCard(m_deck[0]);

            m_deck.RemoveAt(0);
            if (m_deck.Count < 1)
                SwitchDescardToDeck();
        }
    }

    public void SwitchDescardToDeck()
    {
        m_discardDeck = m_discardDeck.OrderBy(x => Random.Range(0, m_discardDeck.Count)).ToList();
        m_discardDeck.ForEach(x => m_deck.Add(x));

        m_discardDeck.Clear();
    }

    public void RemoveAllPrefabCard()
    {
        foreach (var item in m_cardPrefabEmplacements)
        {
            if (item.transform.childCount < 1)
                return;

            GameObject g = item.transform.GetChild(0).gameObject;
            if (g)
            {
                g.GetComponent<MonoBehaviour>().StopAllCoroutines();
                Destroy(g);
            }
        }
    }
    public void RemoveAllPrefabCardWithout(int index)
    {
        for (int i = 0; i < m_cardPrefabEmplacements.Count; i++)
        {
            GameObject e = m_cardPrefabEmplacements[i];
            if (i != index)
                Destroy(e);
        }
    }

    public bool AddEffectCardInHand(So_Effect e)
    {
        if (m_effectCard.Count > 2)
            return false;

        m_effectCard.Add(e);
        return true;
    }

    public void InstantiateBonusCard(So_Effect e)
    {

    }

    public void SetEnableCard(bool enable)
    {
        foreach (var item in m_cardPrefabEmplacements)
        {
            if (item.transform.childCount < 1)
                return;

            GameObject g = item.transform.GetChild(0).gameObject;
            if (g)
            {
                Sc_PbCard s = g.GetComponent<Sc_PbCard>();
                s.m_canClick = enable;
            }
        }
    }
}
