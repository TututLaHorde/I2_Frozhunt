using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class So_MedicinalHerb : So_Consumable
{
    public int m_healValue;

    public override void UseEffect()
    {
        Sc_PopUpManager.Instance.HealPopUp.SetActive(true);
        Sc_PopUpManager.Instance.SetHealthValue(m_healValue);

        Sc_PopUpManager.Instance.SetHealPopUp(
            Sc_GameManager.Instance.playerList[0],    
            Sc_GameManager.Instance.playerList[1]    
        );
    }
}
