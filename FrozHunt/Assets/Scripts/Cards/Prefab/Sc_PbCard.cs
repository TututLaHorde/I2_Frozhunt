using System.Collections;
using UnityEngine;

public class Sc_PbCard : MonoBehaviour
{
    [HideInInspector] public So_Card m_card;

    public bool m_canClick = true;
    public int m_indexPosition;
    [SerializeField] private GameObject m_cardAnim;

    public virtual void InitDisplayCard(So_Card c)
    {
        m_card = c;
    }
    public virtual void UseCard()
    {
        if (m_canClick)
        {
            Sc_BoardManager.Instance.RemoveAllPrefabCardWithout(m_indexPosition);

            StartCoroutine(UseCardAfterTimer());
        }
    }

    private IEnumerator UseCardAfterTimer()
    {
        yield return new WaitForSeconds(m_cardAnim.GetComponent<SC_CardAnim>().Zoom()+0.5f);

        m_card.SelectedCard(this.gameObject);
        if (gameObject.TryGetComponent<Sc_EnemyCardControler>(out Sc_EnemyCardControler s))
        {
            Debug.Log("enemy here guys ");
            yield break;
        }
        Sc_GameManager.Instance.ToNextPhase(Sc_GameManager.eTurnPhase.Draw);
    }
}
