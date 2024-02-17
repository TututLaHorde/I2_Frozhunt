using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum AttackState
{
    Failure,
    Success
}
public enum AbilityState
{
    CriticalAttack,
    CriticalParade,
    Nothing
}

public class Sc_InfoDicePopUp : MonoBehaviour
{
    public Action m_onAttackLaunch;

    [Header("Components")]
    [SerializeField] private TMP_Text m_txtAtkState;
    [SerializeField] private TMP_Text m_txtAbilityState;
    [SerializeField] private TMP_Text m_txtBtnContinue;
    [SerializeField] private Button m_btnContinue;
    private Image m_imgBtnContinue;

    [Header("Params")]
    [SerializeField] private float m_AnimSpeed = 0;

    private float m_alpha = 0;

    /*--------------------------------------------------------------*/

    private void Start()
    {
        m_imgBtnContinue = m_btnContinue.GetComponent<Image>();
        SetAllAlpha();
        m_btnContinue.interactable = false;
    }

    /*--------------------------------------------------------------*/

    public void SetPopUps(bool state)
    {
        if (state)
            StartCoroutine(ActiveAllColors());
        else
            StartCoroutine(DesactiveAnimation()); 
    }

    public void InitPopUp(bool state)
    {
        gameObject.SetActive(state);
        StartCoroutine(DesactiveAnimation());

    }

    public void ContinueButtonClicked()
    {
        Sc_FightManager.Instance.TriggerEffect();
        Sc_BoardManager.Instance.SetActiveSpecialHandCardWithTag(false, "ActiveOnContinueFightClicked");
    }

    public void SetAttackStateText(AttackState s)
    {
        string txt = string.Empty;

        switch (s)
        {
            case AttackState.Success: txt = "Successful Attack"; break;
            case AttackState.Failure: txt = "Failed attack"; break;
            default: break;
        }

        m_txtAtkState.text = txt;
    }
    public void SetAbilityStateText(AbilityState s)
    {
        string txt = string.Empty;

        switch (s)
        {
            case AbilityState.CriticalAttack: txt = "Critical Attack"; break;
            case AbilityState.CriticalParade: txt = "Critical Parade"; break;
            case AbilityState.Nothing: txt = ""; break;
        }

        m_txtAbilityState.text = txt;
    }

    /*--------------------------------------------------------------*/

    private IEnumerator DesactiveAnimation()
    {
        m_btnContinue.interactable = false;
        while(m_alpha > 0)
        {
            m_alpha -= m_AnimSpeed * Time.deltaTime;
            SetAllAlpha();
            yield return null;
        }
        yield return null;
    }

    private IEnumerator ActiveAllColors()
    {
        m_alpha = 0;

        while (m_alpha < 1)
        {
            m_alpha += m_AnimSpeed * Time.deltaTime;
            SetAllAlpha();
            yield return null;
        }
        m_btnContinue.interactable = true;
        yield return null;
    }

    private void SetAllAlpha()
    {
        m_txtAtkState.color     = SetColorAlpha(m_txtAtkState.color, m_alpha);
        m_txtAbilityState.color = SetColorAlpha(m_txtAbilityState.color, m_alpha);
        m_txtBtnContinue.color  = SetColorAlpha(m_txtBtnContinue.color, m_alpha);
        m_imgBtnContinue.color  = SetColorAlpha(m_imgBtnContinue.color, m_alpha);
    }

    private Color SetColorAlpha(Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }
}
