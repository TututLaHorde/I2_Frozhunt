using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Sc_BoardManager : MonoBehaviour
{
    public static Sc_BoardManager Instance;

    public bool m_blendInStart = false;
    [SerializeField] private bool m_canDiscardBonusCard = true;

    [Header("all cards")]
    public List<So_Card> m_deck;
    public List<So_Card> m_boardCards;
    public List<So_Card> m_discardDeck;
    public List<So_Effect> m_effectCard;

    [Header("emplacements")]
    public List<GameObject> m_cardPrefabEmplacements;
    public List<GameObject> m_bonusCardPrefabEmplacements;

    [Header("Dice")]
    public SC_Dice m_dice;
    public Sc_HearthAnim1 m_diceAnimation;

    [Header("Bonus Card")]
    public int m_bonusCardNumber = 0;
    public int m_maxBonusCard = 3;

    [Header("Cards spacing")]
    [SerializeField] private float m_distBetweenBonusCards;
    [SerializeField] private float m_distBetweenBoardCards;

    [Header("Draw Animation")]
    [SerializeField] private float m_delayBeteweenCards = .5f;

    private bool m_canClickDice = true;

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
        if (HasCardInBoard() || !m_canClickDice)
            return;

        int r = 0;
        float t = m_dice.ThrowDice(ref r);
        StartCoroutine(GetCardAfterTimer(t + 1f, r));

        m_canClickDice = false;
        m_diceAnimation.m_CurrentLife = m_diceAnimation.m_MaxLife;
        m_diceAnimation.transform.localScale = Vector3.one;
    }
    private IEnumerator GetCardAfterTimer(float t, int r)
    {
        yield return new WaitForSeconds(t);
        StartCoroutine(GetCard(r));

        Invoke(nameof(ReplayDiceHeartAnimation), 1f);
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

            SetEnableCard(inst, false);

            m_deck.RemoveAt(0);
            if (m_deck.Count < 1)
                SwitchDescardToDeck();

            CenterCard(m_cardPrefabEmplacements, m_distBetweenBoardCards, 0, false);
            MoveBoardCard(inst, () =>
            {
                RotateBoardCard(inst);
            });

            yield return new WaitForSeconds(m_delayBeteweenCards);
        }

        Sc_GameManager.Instance.ToNextPhase(Sc_GameManager.eTurnPhase.Selection);
        Sc_GameManager.Instance.AddTurn();
        Sc_TutorialManager.Instance.ShowEventTuto(true);

    }

    private void ReplayDiceHeartAnimation()
    {
        m_canClickDice = true;
        m_diceAnimation.m_CurrentLife = 14;
    }
    
    public void RotateBoardCard()
    {
        foreach (var item in m_cardPrefabEmplacements)
        {
            int childCount = item.transform.childCount;

            if (childCount > 0)
            {
                Sc_BoardCardAnimation pbCard = item.transform.GetChild(0).GetComponent<Sc_BoardCardAnimation>();
                pbCard.StartRotateAnimation(null);
            }
        }
    }
    public void RotateBoardCard(GameObject rotateCard)
    {
        Sc_BoardCardAnimation animator = rotateCard.GetComponent<Sc_BoardCardAnimation>();
        animator.StartRotateAnimation(() =>
        {
            SetEnableCard(rotateCard, true);
        });
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

            GameObject g = item.v.transform.GetChild(0).gameObject;
            if (g)
            {
                SetEnableCard(g, false);

                Sc_PbCard pbCard = g.GetComponent<Sc_PbCard>();
                if (!pbCard.enabled)
                    continue;

                g.GetComponent<MonoBehaviour>().StopAllCoroutines();

                if (m_boardCards.Count > 0)
                {
                    m_discardDeck.Add(m_boardCards[0]);
                    m_boardCards.RemoveAt(0);
                }
                pbCard.enabled = false;

                DiscardCardAnimation(g, () =>
                {
                    Invoke(nameof(UpdateEmplacementCard), Time.deltaTime * 3f);
                });
            }
        }
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
                SetEnableCard(g, false);

                Sc_PbCard pbCard = g.GetComponent<Sc_PbCard>();
                if (!pbCard.enabled)
                    continue;

                g.GetComponent<MonoBehaviour>().StopAllCoroutines();

                if (!item.i.Equals(index))
                    m_discardDeck.Add(m_boardCards[0]);

                m_boardCards.RemoveAt(0);
                pbCard.enabled = false;

                DiscardCardAnimation(g, () =>
                {
                    Invoke(nameof(UpdateEmplacementCard), Time.deltaTime * 3f);
                });
            }
        }
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
                SetEnableCard(g, false);

                Sc_PbCard pbCard = g.GetComponent<Sc_PbCard>();
                if (!pbCard.enabled)
                    continue;

                g.GetComponent<MonoBehaviour>().StopAllCoroutines();

                m_discardDeck.Add(m_boardCards[0]);
                m_boardCards.RemoveAt(0);
                pbCard.enabled = false;

                DiscardCardAnimation(g, null);
            }
            else
            {
                RectTransform rectTransform = item.v.GetComponent<RectTransform>();
                rectTransform.localPosition = Vector3.zero;
            }
        }

        //Invoke(nameof(UpdateEmplacementCard), Time.deltaTime * 3f);
    }
    public void RemoveBonusCard(int i)
    {
        if (!m_canDiscardBonusCard)
            return;

        m_canDiscardBonusCard = false;
        int childCount = m_bonusCardPrefabEmplacements[i].transform.childCount;

        if (childCount > 1)
        {
            GameObject e    = m_bonusCardPrefabEmplacements[i].transform.GetChild(1).gameObject;
            GameObject tuto = m_bonusCardPrefabEmplacements[i].transform.GetChild(0).gameObject;

            Sc_HandCard handCard = m_bonusCardPrefabEmplacements[i].GetComponent<Sc_HandCard>();
            Sc_HandCardAnim handCardAnimator = m_bonusCardPrefabEmplacements[i].GetComponent<Sc_HandCardAnim>();

            tuto.SetActive(false);
            handCardAnimator.m_CanUpCard = false;
            handCardAnimator.DownCardAnimation();
            handCard.m_useActive = false;

            DiscardCardAnimation(e, () =>
            {
                handCardAnimator.m_CanUpCard = true;

                m_discardDeck.Add(m_effectCard[i]);
                m_effectCard.RemoveAt(i);
                m_bonusCardNumber--;

                handCard.m_effectCard = null;
                handCardAnimator.m_CanUpCard = true;

                Invoke(nameof(ReparentBonusCard), Time.deltaTime * 3f);

                m_canDiscardBonusCard = true;
            });
        }
        else
        {
            m_canDiscardBonusCard = true;
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

                    Sc_HandCard nextHandCard = m_bonusCardPrefabEmplacements[item.i + 1].GetComponent<Sc_HandCard>();
                    Sc_HandCard handCard = item.v.GetComponent<Sc_HandCard>();

                    handCard.m_effectCard = nextHandCard.m_effectCard;
                    nextHandCard.m_effectCard = null;
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
                continue;

            GameObject g = item.transform.GetChild(0).gameObject;
            if (g)
            {
                Sc_PbCard s = g.GetComponent<Sc_PbCard>();
                s.m_canClick = enable;
            }
        }
    }
    public void SetEnableCard(GameObject card, bool enable)
    {
        if (card.TryGetComponent<Sc_PbCard>(out var c))
        {
            c.m_canClick = enable;
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

    public void DiscardCardAnimation(GameObject card, System.Action actionAfterDestroy)
    {
        Sc_BoardCardAnimation animator = card.GetComponent<Sc_BoardCardAnimation>();
        animator.StartDiscardAnimation(() =>
        {
            Destroy(card);
            actionAfterDestroy?.Invoke();
        });
    }
}