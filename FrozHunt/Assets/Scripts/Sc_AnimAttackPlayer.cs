using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_AnimAttackPlayer : MonoBehaviour
{

    public GameObject m_CardToAnim;

    private Transform m_transformCard;

    public float m_PosYForLoadAttack = 100;

    [Header("Position Of Enemy")]
    public GameObject m_EnemyPosition;

    private Vector3 m_FirstPosition = Vector3.zero;
    private Vector3 m_NewPos = Vector3.zero;

    private float time = 0;

    [SerializeField]private bool m_anim = false;

    private bool m_canContinue = false; 

    [SerializeField]private int m_state = 0;

    [Header("Movement Speed")]
    public float m_speedLoad = 1;
    public float m_speedAttack = 1;
    public float m_speedReturn = 1;

    [Header("Shake")]
    public float m_ShakeDuration = 2;
    private float m_ShakeTime = 0;
    public float m_shakeMagnitude = 2;
    public GameObject m_ShakeObject;
    public AnimationCurve m_AnimationCurve;

    private Sc_HandCardAnim m_cardAnimHand;


    // Start is called before the first frame update
    void Start()
    {
        m_cardAnimHand = gameObject.GetComponent<Sc_HandCardAnim>();
        m_transformCard = m_CardToAnim.transform;
        m_FirstPosition = m_transformCard.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.R)) 
        { StartCoroutine(AnimAttack(null));}
    }

    public IEnumerator AnimAttack(System.Action onAnimEnd)
    {
        if(m_cardAnimHand != null)
        {
            m_cardAnimHand.m_CanUpCard = false;
            m_cardAnimHand.DownCardAnimation();
        }

        m_anim = true;
        m_state = 0;
        m_canContinue = true;
        while (m_anim)
        {
            if(m_canContinue)
            {
                m_canContinue=false;
                switch(m_state) 
                {
                    case 0:
                        StartCoroutine(AnimLoadAttack());
                        break;
                    case 1:
                        StartCoroutine(AnimAttackEnemy(onAnimEnd));
                        break;
                    case 2:
                        StartCoroutine(AnimReturnToInitialPos());
                        break;
                    default: m_anim = false; break;
                }
            }



            yield return null;
        }

        if (m_cardAnimHand != null)
            m_cardAnimHand.m_CanUpCard = true;

        yield return null;
    }

    public IEnumerator AnimLoadAttack()
    {
        time = Time.time;
        m_NewPos.Set(m_FirstPosition.x, m_FirstPosition.y - m_PosYForLoadAttack, m_FirstPosition.z);

        while ((Time.time - time) *m_speedLoad < 1)
        {
            Debug.Log("Move Card For Load Attack   "  + m_FirstPosition.x + "    " + m_FirstPosition.y);
            m_transformCard.localPosition = Vector3.Lerp(m_FirstPosition, m_NewPos, (Time.time - time) * m_speedLoad);
            yield return null;
        }

        m_state++;
        m_canContinue = true;

        yield return null;
    }

    public IEnumerator AnimAttackEnemy(System.Action onAnimEnd)
    {
        time = Time.time;

        m_NewPos = m_transformCard.position;

        while ((Time.time - time) * m_speedAttack < 1f)
        {
            Debug.Log("Move Card For Attack Enemy  ");
            m_transformCard.position = Vector3.Lerp(m_NewPos, m_EnemyPosition.transform.position, (Time.time - time) * m_speedAttack);
            yield return null;
        }

        onAnimEnd?.Invoke();
        m_state++;
        m_canContinue = true;
        StartCoroutine(Shake());

        yield return null;
    }

    public IEnumerator AnimReturnToInitialPos()
    {
        time = Time.time;

        m_NewPos = m_transformCard.localPosition;

        while ((Time.time - time) * m_speedReturn < 1f)
        {
            Debug.Log("Move Card For Attack Enemy  ");
            m_transformCard.localPosition = Vector3.Lerp(m_NewPos, m_FirstPosition, (Time.time - time) * m_speedReturn);
            yield return null;
        }

        m_state++;
        m_canContinue = true;

        yield return null;
    }

    private IEnumerator Shake()
    {
        m_ShakeTime = Time.time + m_ShakeDuration;
        //Vector3 initialPosition = m_ShakeObject.transform.localPosition;

        Debug.Log("    initial POS 1 ::  " );
        float timePass = 0;
        while (Time.time < m_ShakeTime)
        {
            timePass += Time.deltaTime;
            // Debug.Log("Shake  : " + ((m_AnimationCurve.Evaluate(timePass) * m_shakeMagnitude)));
            m_ShakeObject.transform.localPosition = Vector3.zero + Random.insideUnitSphere * (m_AnimationCurve.Evaluate(timePass) * m_shakeMagnitude);
            yield return null;
        }


        m_ShakeObject.transform.localPosition = Vector3.zero;

        Debug.Log("    initial POS 2 ::  " + m_ShakeObject.transform.localPosition);


        yield return null;
    }

    public void SetFirstPos(Vector3 value) => m_FirstPosition = value;
}
