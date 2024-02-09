using System.Collections;
using UnityEngine;

public class Sc_PbCard : MonoBehaviour
{
    [HideInInspector] public So_Card m_card;

    public bool m_canClick = true;
    public int m_indexPosition;

    [Header("Back Card")]
    [SerializeField] private GameObject m_backCard;
    [SerializeField] private float m_animationSpeed = 2f;

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

    public void StartRotateAnimation()
    {
        StartCoroutine(RotateAnimationCard());
    }

    private IEnumerator UseCardAfterTimer()
    {
        yield return new WaitForSeconds(1f);

        m_card.SelectedCard(this.gameObject);
        if(gameObject.TryGetComponent<Sc_EnemyCardControler>(out Sc_EnemyCardControler s))
        {
            Debug.Log("enemy here guys ");
            yield break;
        }
        Sc_GameManager.Instance.ToNextPhase(Sc_GameManager.eTurnPhase.Draw);
    }

    private IEnumerator RotateAnimationCard()
    {
        float timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime * m_animationSpeed;
            transform.eulerAngles = Vector3.Lerp(Vector3.zero, new Vector3(0f, -90f, 0f), timer);
            yield return null;
        }
        m_backCard.SetActive(false);

        timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime * m_animationSpeed;
            transform.eulerAngles = Vector3.Lerp(new Vector3(0f, -90f, 0f), Vector3.zero, timer);
            yield return null;
        }
    }
}
