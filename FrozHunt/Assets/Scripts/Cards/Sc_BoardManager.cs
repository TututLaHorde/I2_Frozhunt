using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Sc_BoardManager : MonoBehaviour
{
    public static Sc_BoardManager Instance;

    public bool m_blendInStart = false;

    [Header("all cards")]
    public List<So_Card> m_deck;
    public List<So_Card> m_boardCards;
    public List<So_Card> m_discardDeck;
    public List<So_Effect> m_effectCard;

    [Header("emplacements")]
    public List<GameObject> m_cardPrefabEmplacements;
    public List<GameObject> m_bonusCardPrefabEmplacements;

    public SC_Dice m_dice;

    public int m_bonusCardNumber = 0;
    public int m_maxBonusCard = 3;

    [Header("Cards spacing")]
    [SerializeField] private float m_distBetweenBonusCards;
    [SerializeField] private float m_distBetweenBoardCards;

    [Header("Draw Animation")]
    [SerializeField] private float m_delayBeteweenCards = .5f;

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
        StartCoroutine(GetCardAfterTimer(t *1.4f, r));

        Sc_GameManager.Instance.ToNextPhase(Sc_GameManager.eTurnPhase.Selection);
        Sc_GameManager.Instance.AddTurn();
    }
    private IEnumerator GetCardAfterTimer(float t, int r)
    {
        yield return new WaitForSeconds(t);
        StartCoroutine(GetCard(r));
    }
    public IEnumerator GetCard(int n)
    {
        CenterGameObjectList(m_cardPrefabEmplacements, m_distBetweenBoardCards, n);

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

            CenterCard(m_cardPrefabEmplacements, m_distBetweenBoardCards, 0, false);
            MoveBoardCard(inst, () =>
            {

            });

            yield return new WaitForSeconds(m_delayBeteweenCards);
        }

        RotateBoardCard();
    }
    public void RotateBoardCard()
    {
        foreach (var item in m_cardPrefabEmplacements)
        {
            int childCount = item.transform.childCount;

            if (childCount > 0)
            {
                Sc_BoardCardAnimation pbCard = item.transform.GetChild(0).GetComponent<Sc_BoardCardAnimation>();
                pbCard.StartRotateAnimation();
            }
        }
    }
    public void RotateBoardCard(GameObject rotateCard)
    {
        Sc_BoardCardAnimation animator = rotateCard.GetComponent<Sc_BoardCardAnimation>();
        animator.StartRotateAnimation();
    }
    public void MoveBoardCard(GameObject moveCard, System.Action actionAfterMove)
    {
        Sc_BoardCardAnimation animator = moveCard.GetComponent<Sc_BoardCardAnimation>();
        animator.StartMoveAnimation(actionAfterMove);
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

        Invoke(nameof(UpdateEmplacementCard), Time.deltaTime * 3f);
    }
    public void RemoveAllPrefabCardWithDiscardIndex(int index)
    {
        foreach (var item in m_cardPrefabEmplacements.Select((v, i) => new { v, i }))
        {
            if (item.v.transform.childCount < 1)
                continue;

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

        Invoke(nameof(UpdateEmplacementCard), Time.deltaTime * 3f);
    }
    public void RemoveAllPrefabCardWithout(int index)
    {
        foreach (var item in m_cardPrefabEmplacements.Select((v, i) => new { v, i }))
        {
            if (item.v.transform.childCount < 1)
                continue;

            GameObject g = item.v.transform.GetChild(0).gameObject;
            if (g && item.i != index)
            {
                g.GetComponent<MonoBehaviour>().StopAllCoroutines();

                m_discardDeck.Add(m_boardCards[0]);
                m_boardCards.RemoveAt(0);
                Destroy(g);
            }
        }

        Invoke(nameof(UpdateEmplacementCard), Time.deltaTime * 3f);
    }
    public void RemoveBonusCard(int i)
    {
        int childCount = m_bonusCardPrefabEmplacements[i].transform.childCount;

        if (childCount > 1)
        {
            GameObject e = m_bonusCardPrefabEmplacements[i].transform.GetChild(1).gameObject;
            m_discardDeck.Add(m_effectCard[i]);
            m_effectCard.RemoveAt(i);
            m_bonusCardNumber--;

            Destroy(e);
            Invoke(nameof(ReparentBonusCard), Time.deltaTime * 3f);
        }
    }

    public void UpdateEmplacementCard()
    {
        CenterCard(m_cardPrefabEmplacements, m_distBetweenBoardCards);
    }
    public void ReparentBonusCard()
    {
        foreach (var item in m_bonusCardPrefabEmplacements.Select((v, i) => new { v, i }))
        {
            if (item.i + 1 > 2)
                continue;

            int currentChildCount = item.v.transform.childCount;

            if (currentChildCount < 2)
            {
                int nextChildCount = m_bonusCardPrefabEmplacements[item.i + 1].transform.childCount;

                if (nextChildCount > 1)
                {
                    Transform move = m_bonusCardPrefabEmplacements[item.i + 1].transform.GetChild(1);
                    RectTransform t = move.GetComponent<RectTransform>();

                    move.SetParent(item.v.transform);
                    t.localPosition = Vector3.zero;
                }
            }
        }

        CenterCard(m_bonusCardPrefabEmplacements, m_distBetweenBonusCards, 1);
    }

    public void CenterCard(List<GameObject> e, float cardSpacing, int childCountEnable = 0, bool centerAfterEnable = true)
    {
        int activeNumber = 0;
        List<GameObject> l = new List<GameObject>();

        foreach (var item in e)
        {
            int cCount = item.transform.childCount;
            item.SetActive(cCount > childCountEnable);

            if (cCount > childCountEnable)
            {
                activeNumber++;
                l.Add(item);
            }
        }

        if (centerAfterEnable)
            CenterGameObjectList(l, cardSpacing, activeNumber);
    }
    public void CenterGameObjectList(List<GameObject> e, float p, int n)
    {
        float originMult = -(n - 1f) / 2f;
        for (int i = 0; i < n; i++)
        {
            Transform t = e[i].transform;
            t.localPosition = new Vector3(p * (originMult + i), t.localPosition.y);
        }
    }

    public bool AddEffectCardInHand(So_Effect e)
    {
        if (m_effectCard.Count > m_maxBonusCard - 1)
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
        h.SetActiveHandCardButton(e.m_enableButton);
        c.InitDisplayCard(e);
        inst.transform.GetChild(1).gameObject.SetActive(false);

        m_bonusCardNumber++;
        CenterCard(m_bonusCardPrefabEmplacements, m_distBetweenBonusCards, 1);
        Sc_TutorialManager.Instance.m_handCapacityWindow.SetActive(false);
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

    public void SetActiveSpecialHandCardWithTag(bool enable, string tag)
    {
        foreach (var item in m_bonusCardPrefabEmplacements)
        {
            if (item.TryGetComponent<Sc_HandCard>(out var h))
            {
                if (h.m_effectCard == null)
                    continue;

                if (h.m_effectCard.m_cardTag.Split('.').Contains(tag))
                {
                    h.SetActiveHandCardButton(enable);
                }
            }
        }
    }
}