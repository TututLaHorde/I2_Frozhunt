using System.Collections;
using UnityEngine;

public class Sc_PbCard : MonoBehaviour
{
    [HideInInspector] public So_Card m_card;

    public bool m_canClick = true;
    public int m_indexPosition;

    [SerializeField] private GameObject m_cardAnim;
    [SerializeField] private GameObject m_greyCard; 

    public virtual void InitDisplayCard(So_Card c)
    {
        m_card = c;
    }
    public virtual void UseCard()
    {
        if (m_canClick)
        {
            Sc_BoardManager.Instance.RemoveAllPrefabCardWithout(m_indexPosition);
            Sc_TutorialManager.Instance.ShowEventTuto(false);
            StartCoroutine(UseCardAfterTimer());

            So_Keepable k = m_card as So_Keepable;
            if (k)
                if (k.m_forceAddHand)
                    return;

            m_canClick = false;
        }
    }

    private IEnumerator UseCardAfterTimer()
    {
        yield return new WaitForSeconds(m_cardAnim.GetComponent<SC_CardAnim>().Zoom()+0.5f);

        m_card.SelectedCard(this.gameObject);

        if (gameObject.TryGetComponent<Sc_EnemyCardControler>(out Sc_EnemyCardControler s))
        {
            yield break;
        }
        Sc_GameManager.Instance.ToNextPhase(Sc_GameManager.eTurnPhase.Draw);
    }

    public void SetGreyScreen(bool enable)
    {
        if (m_greyCard)
            m_greyCard.SetActive(!enable);
    }
}
