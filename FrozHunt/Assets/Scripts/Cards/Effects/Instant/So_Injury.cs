using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;


[CreateAssetMenu(fileName = "Injury ", menuName = "Card/Instant/Injury ")]
public class So_Injury : So_Instant
{
    private int m_randomIndex = 0;
    public int m_numberOfDamage = 2;

    public override void SelectedCard(GameObject owner)
    {

        m_randomIndex = Random.Range(0, Sc_GameManager.Instance.playerList.Count);
        Sc_GameManager.Instance.playerList[m_randomIndex].TakeDamage(m_numberOfDamage);


        base.SelectedCard(owner);
    }
}
