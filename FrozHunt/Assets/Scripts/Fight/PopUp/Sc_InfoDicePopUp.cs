using System;
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
    public GameObject m_background;
    public Text m_attackStateText;
    public Text m_abilityStateText;
    public Button m_continueButton;
    

    public void SetPopUps(bool state)
    {
        m_background.SetActive(state);
    }

    public void InitPopUp(bool state)
    {
        gameObject.SetActive(state);
        m_background.SetActive(false);

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
