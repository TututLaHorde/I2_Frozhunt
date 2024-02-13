using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

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
    public GameObject m_background;
    public Text m_attackStateText;
    public Text m_abilityStateText;
    public Text m_continueButtonText;
    public UnityEngine.UI.Button m_continueButton;
    public Image m_continueImage;
    public float m_AnimSpeed = 0;

    private float m_alpha = 1;


    private void Start()
    {
        m_alpha = 0;
        m_attackStateText.color = new Color(m_attackStateText.color.r, m_attackStateText.color.g, m_attackStateText.color.b, m_alpha);
        m_abilityStateText.color = new Color(m_abilityStateText.color.r, m_abilityStateText.color.g, m_abilityStateText.color.b, m_alpha);
        m_continueButtonText.color = new Color(m_continueButtonText.color.r, m_continueButtonText.color.g, m_continueButtonText.color.b, m_alpha);
        m_continueImage.color = new Color(m_continueImage.color.r, m_continueImage.color.g, m_continueImage.color.b, m_alpha);
        m_continueButton.interactable = false;
    }

    public void SetPopUps(bool state)
    {
        //m_background.SetActive(state);
        if (state)
            StartCoroutine(ActiveAllColors());
        else
            StartCoroutine(DesactiveAnimation()); 
    }

    public void InitPopUp(bool state)
    {
        gameObject.SetActive(state);
        //m_background.SetActive(false);
        StartCoroutine(DesactiveAnimation());

    }



    private IEnumerator DesactiveAnimation()
    {
        m_continueButton.interactable = false;
        while(m_alpha > 0)
        {
            m_alpha -= m_AnimSpeed * Time.deltaTime;
            m_attackStateText.color = new Color(m_attackStateText.color.r, m_attackStateText.color.g, m_attackStateText.color.b, m_alpha);
            m_abilityStateText.color = new Color(m_abilityStateText.color.r, m_abilityStateText.color.g, m_abilityStateText.color.b, m_alpha);
            m_continueButtonText.color = new Color(m_continueButtonText.color.r, m_continueButtonText.color.g, m_continueButtonText.color.b, m_alpha);
            m_continueImage.color = new Color(m_continueImage.color.r, m_continueImage.color.g, m_continueImage.color.b, m_alpha);
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
            m_attackStateText.color = new Color(m_attackStateText.color.r, m_attackStateText.color.g, m_attackStateText.color.b, m_alpha);
            m_abilityStateText.color = new Color(m_abilityStateText.color.r, m_abilityStateText.color.g, m_abilityStateText.color.b, m_alpha);
            m_continueButtonText.color = new Color(m_continueButtonText.color.r, m_continueButtonText.color.g, m_continueButtonText.color.b, m_alpha);
            m_continueImage.color = new Color(m_continueImage.color.r, m_continueImage.color.g, m_continueImage.color.b, m_alpha);
            yield return null;
        }
        m_continueButton.interactable = true;
        yield return null;
    }


    public void ContinueButtonClicked()
    {
        Sc_FightManager.Instance.MakeAttackAnimation();
        Sc_BoardManager.Instance.SetActiveSpecialHandCardWithTag(false, "ActiveOnContinueFightClicked");
    }

    public void SetAttackStateText(AttackState s)
    {
        string txt = string.Empty;

        switch (s)
        {
            case AttackState.Success: txt = "Successful Attack"; break;
            case AttackState.Failure: txt = "Failed attack";     break;
            default: break;
        }

        m_attackStateText.text = txt;
    }
    public void SetAbilityStateText(AbilityState s)
    {
        string txt = string.Empty;

        switch (s) 
        {
            case AbilityState.CriticalAttack: txt = "Critical Attack"; break;
            case AbilityState.CriticalParade: txt = "Critical Parade"; break;
            case AbilityState.Nothing:        txt = "";         break;
        }  

        m_abilityStateText.text = txt;
    }
}
