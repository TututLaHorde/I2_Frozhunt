using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class SC_Dice : MonoBehaviour
{
    [SerializeField] SO_Dice m_soDice;
    [SerializeField] private AnimationCurve m_animCurve;
    [SerializeField] private int m_result;
    private Sprite[] m_dice;
    private float m_maxTime;
    private Image m_sprites;
    private Sprite m_sprite;
    
    private void Start()
    {
        //get value of Scriptable Object
        m_dice = m_soDice.m_dice;
        m_maxTime = m_soDice.m_maxTime;
    }

    public float ThrowDice(ref int result)
    {
        result = Random.Range(1, m_dice.Length+1);
        if (TryGetComponent(out Image sp))
        {
            //get SpriteRenderer
            m_sprites = sp;
            //check that list is not empty
            Assert.IsNotNull(m_dice, "error : empty list");
            StartCoroutine(Dice(result));
        }
        else
        {
            Debug.LogError("error : no sprite renderer");
        }
        return (m_maxTime);
    }
    private IEnumerator Dice(int result)
    {     
        float timeleft = m_maxTime;
        while (timeleft >= 0.0f)
        {
            //animate dice
            float deltaTime = m_animCurve.Evaluate(1-timeleft / m_maxTime);
            timeleft -= deltaTime;
            m_result = Random.Range(0, m_dice.Length);
            m_sprite = m_dice[m_result];
            m_sprites.sprite = m_sprite;
            yield return new WaitForSeconds(deltaTime);
        }
        //display finalresult
        m_result = result-1;
        m_sprite = m_dice[m_result];
        m_sprites.sprite = m_sprite;
    }
}
