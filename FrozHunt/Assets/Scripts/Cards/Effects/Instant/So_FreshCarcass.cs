using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FreshCarcass", menuName = "Card/Instant/FreshCarcass")]
public class So_FreshCarcass : So_Instant
{
    public int m_numberOfWinFood = 1;
    public override void SelectedCard(GameObject owner)
    {
        Sc_GameManager.Instance.AddFood(m_numberOfWinFood);
        base.SelectedCard(owner);
    }
}
