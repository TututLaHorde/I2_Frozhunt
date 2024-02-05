using System.Collections;
using UnityEngine;

public class SC_Dice : MonoBehaviour
{
    [SerializeField] SO_Dice m_soDice;
    [SerializeField] private AnimationCurve m_animCurve;
    [SerializeField] private int m_result;
    private Sprite[] m_dice;
    private float m_maxTime;
    private SpriteRenderer m_sprites;
    private Sprite m_sprite;
    

    private void Start()
    {
        m_dice = m_soDice.m_dice;
        m_maxTime = m_soDice.m_maxTime;
        if (GetComponent<SpriteRenderer>() != null)
        {
            m_sprites = GetComponent<SpriteRenderer>();
            if (m_dice == null)
            {
                print("error : empty list");
            }
            else
            {
                StartCoroutine(Dice());
            }
        }
        else
        {
            print("error : no sprite renderer");
        }
       
    }
    private IEnumerator Dice()
    {     
        float timeleft = m_maxTime;   
        while (timeleft >= 0.0f)
        {
            
            float deltaTime = m_animCurve.Evaluate(1-timeleft / m_maxTime);
            Debug.Log(deltaTime);
            timeleft -= deltaTime;
            m_result = Random.Range(0, m_dice.Length);
            m_sprite = m_dice[m_result];
            m_sprites.sprite = m_sprite;
            yield return new WaitForSeconds(deltaTime);
        }
        m_result += 1;
    }
}
