using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_HealCompetence : Sc_PlayerCompetence
{

    public int m_healValue = 5;
    public override void Critique(Sc_EnemyCardControler enemy)
    {
        Sc_PopUpManager.Instance.HealPopUp.SetActive(true);
        Sc_PopUpManager.Instance.SetHealthValue(m_healValue);
        Sc_PopUpManager.Instance.SetHealPopUp(Sc_GameManager.Instance.playerList[0], Sc_GameManager.Instance.playerList[1], false);
    }

    public override void Dodge()
    {
        throw new System.NotImplementedException();
    }
}
