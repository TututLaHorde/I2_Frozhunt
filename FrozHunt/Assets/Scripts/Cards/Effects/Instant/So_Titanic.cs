using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Titanic", menuName = "Card/Instant/Titanic")]
public class So_Titanic : So_Instant
{
    [Header("Titanic")]
    public int m_lifePlus = 5;

    public override void SelectedCard(GameObject owner)
    {
        Debug.Log("Titanic");

        Sc_FightManager.Instance.m_PlusLife += m_lifePlus;
        base.SelectedCard(owner);
    }
}
