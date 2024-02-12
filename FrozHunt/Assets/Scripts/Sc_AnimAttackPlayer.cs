using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_AnimAttackPlayer : MonoBehaviour
{

    [SerializeField] GameObject m_CardToAnim;

    private Transform m_transformCard;

    public float m_PosYForLoadAttack = 100;

    public GameObject m_EnemyPosition;
    private Vector3 m_FirstPosition = Vector3.zero;
    private Vector3 m_NewPos = Vector3.zero;

    private float time = 0;

    private bool m_anim = false;

    private bool m_canContinue = false; 

    private int m_state = 0;

    public float m_speedLoad = 1;
    public float m_speedAttack = 1;
    public float m_speedReturn = 1;

    // Start is called before the first frame update
    void Start()
    {
        m_transformCard = m_CardToAnim.transform;
        m_FirstPosition = m_transformCard.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.R)) 
        { StartCoroutine(AnimAttack()); }
        
    }


    public IEnumerator AnimAttack()
    {
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
                        StartCoroutine(AnimAttackEnemy());
                        break;

                    case 2:
                        StartCoroutine(AnimReturnToInitialPos());
                        break;
                }
            }


            yield return null;
        }



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

    public IEnumerator AnimAttackEnemy()
    {
        time = Time.time;

        m_NewPos = m_transformCard.position;

        while ((Time.time - time) * m_speedAttack < 1f)
        {
            Debug.Log("Move Card For Attack Enemy  ");
            m_transformCard.position = Vector3.Lerp(m_NewPos, m_EnemyPosition.transform.position, (Time.time - time) * m_speedAttack);
            yield return null;
        }

        m_state++;
        m_canContinue = true;

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

        yield return null;
    }


}
