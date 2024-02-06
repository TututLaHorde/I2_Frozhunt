using System;
using System.Collections;
using System.Collections.Generic;
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

    public void ContinueButtonClicked()
    {
        m_onAttackLaunch?.Invoke();
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
            case AbilityState.Nothing:        txt = "nothing";         break;
        }  

        m_abilityStateText.text = txt;
    }
}
