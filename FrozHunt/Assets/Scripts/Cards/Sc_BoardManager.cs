using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Sc_BoardManager : MonoBehaviour
{
    public List<So_Card> m_deck;
    public List<So_Card> m_boardCards;
    public List<So_Card> m_discardDeck;
    public List<So_Effect> m_effectCard;

    public List<GameObject> m_cardPrefabEmplacements;
    public List<GameObject> m_bonusCardPrefabEmplacements;

    public bool m_blendInStart = false;
    public SC_Dice m_dice;

    public int m_bonusCardNumber = 0;

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

    public void BlendDeck()
    {
        m_deck = m_deck.OrderBy(x => Random.Range(0, m_deck.Count)).ToList();
    }

    public void GetCard()
    {
        if (HasCardInBoard())
            return;

        int r = 0;
        float t = m_dice.ThrowDice(ref r);
        StartCoroutine(GetCardAfterTimer(t + .5f, r));

        Sc_GameManager.Instance.ToNextPhase(Sc_GameManager.eTurnPhase.Selection);
        Sc_GameManager.Instance.AddTurn();
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
            m_boardCards.Add(m_deck[0]);

            GameObject e = m_cardPrefabEmplacements[i];
            GameObject inst = Instantiate(m_deck[0].m_prebafInBoard, e.transform);
            Sc_PbCard c = inst.GetComponent<Sc_PbCard>();
            c.InitDisplayCard(m_deck[0]);
            c.m_indexPosition = i;

            m_deck.RemoveAt(0);
            if (m_deck.Count < 1)
                SwitchDescardToDeck();
        }
    }

    public bool HasCardInBoard()
    {
        foreach (var item in m_cardPrefabEmplacements)
        {
            int childCount = item.transform.childCount;

            if (childCount > 0)
                return true;
        }

        return false;
    }

    public void SwitchDescardToDeck()
    {
        m_discardDeck = m_discardDeck.OrderBy(x => Random.Range(0, m_discardDeck.Count)).ToList();
        m_discardDeck.ForEach(x => m_deck.Add(x));

        m_discardDeck.Clear();
    }

    public void RemoveAllPrefabCard()
    {
        foreach (var item in m_cardPrefabEmplacements.Select((v, i) => new {v, i}))
        {
            if (item.v.transform.childCount < 1)
                continue;

            Debug.Log("Remove this card");
            GameObject g = item.v.transform.GetChild(0).gameObject;
            if (g)
            {
                g.GetComponent<MonoBehaviour>().StopAllCoroutines();
                
                if (m_boardCards.Count > 0)
                {
                    m_discardDeck.Add(m_boardCards[0]);
                    m_boardCards.RemoveAt(0);
                }

                Destroy(g);
            }
        }
    }
    public void RemoveAllPrefabCardWithDiscardIndex(int index)
    {
        foreach (var item in m_cardPrefabEmplacements.Select((v, i) => new { v, i }))
        {
            if (item.v.transform.childCount < 1)
                return;

            GameObject g = item.v.transform.GetChild(0).gameObject;
            if (g)
            {
                g.GetComponent<MonoBehaviour>().StopAllCoroutines();

                if (!item.i.Equals(index))
                    m_discardDeck.Add(m_boardCards[0]);

                m_boardCards.RemoveAt(0);
                Destroy(g);
            }
        }
    }
    public void RemoveAllPrefabCardWithout(int index)
    {
        foreach (var item in m_cardPrefabEmplacements.Select((v, i) => new { v, i }))
        {
            if (item.v.transform.childCount < 1)
                return;

            GameObject g = item.v.transform.GetChild(0).gameObject;
            if (g && item.i != index)
            {
                g.GetComponent<MonoBehaviour>().StopAllCoroutines();

                m_discardDeck.Add(m_boardCards[0]);
                m_boardCards.RemoveAt(0);
                Destroy(g);
            }
        }
    }
    public void RemoveBonusCard(int i)
    {
        int childCount = m_bonusCardPrefabEmplacements[i].transform.childCount;

        if (childCount > 0)
        {
            GameObject e = m_bonusCardPrefabEmplacements[i].transform.GetChild(0).gameObject;
            m_discardDeck.Add(m_effectCard[i]);
            m_effectCard.RemoveAt(i);
            m_bonusCardNumber--;

            Destroy(e);
            Invoke(nameof(ReparentBonusCard), Time.deltaTime);
        }
    }

    public void ReparentBonusCard()
    {
        foreach (var item in m_bonusCardPrefabEmplacements.Select((v, i) => new { v, i }))
        {
            if (item.i + 1 > 2)
                return;

            int currentChildCount = item.v.transform.childCount;

            if (currentChildCount < 1)
            {
                int nextChildCount = m_bonusCardPrefabEmplacements[item.i + 1].transform.childCount;

                if (nextChildCount > 0)
                {
                    Transform move = m_bonusCardPrefabEmplacements[item.i + 1].transform.GetChild(0);
                    RectTransform t = move.GetComponent<RectTransform>();

                    move.SetParent(item.v.transform);
                    t.localPosition = Vector3.zero;
                }
            }
        }

        CenterBonusCard();
    }
    public void CenterBonusCard()
    {
        int activeNumber = 0;

        foreach (var item in m_bonusCardPrefabEmplacements)
        {
            int cCount = item.transform.childCount;
            item.SetActive(cCount > 0);
            activeNumber += cCount > 0 ? 1 : 0;
        }

        float originMult = -(activeNumber - 1f) / 2f;
        for (int i = 0; i < activeNumber; i++)
        {
            Transform t = m_bonusCardPrefabEmplacements[i].transform;
            t.localPosition = new Vector3(600f * (originMult * i), t.localPosition.y);
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
        GameObject g = m_bonusCardPrefabEmplacements[m_bonusCardNumber];
        GameObject inst = Instantiate(e.m_prebafInBoard, g.transform);

        Sc_PbCard c = inst.GetComponent<Sc_PbCard>();
        Sc_HandCard h = g.GetComponent<Sc_HandCard>();
        Button b = inst.GetComponent<Button>();

        b.enabled = false;
        h.m_effectCard = e;
        c.InitDisplayCard(e);

        m_bonusCardNumber++;
        CenterBonusCard();
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