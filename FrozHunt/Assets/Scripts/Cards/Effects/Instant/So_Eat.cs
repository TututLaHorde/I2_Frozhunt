using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Eat", menuName = "Card/Instant/Eat")]
public class So_Eat : So_Instant
{
    [Header("Eat")]
    public int m_numberOfDamage = 2;
    public int m_useFoodPerPlayer = -1;

    public override void SelectedCard(GameObject owner)
    {
        if (Sc_GameManager.Instance.GetFood() >= Mathf.Abs(m_useFoodPerPlayer * Sc_GameManager.Instance.playerList.Count))
        {
            Sc_GameManager.Instance.AddFood(m_useFoodPerPlayer * Sc_GameManager.Instance.playerList.Count);
        }
        else
        {
            for (int j = 0; j < Sc_GameManager.Instance.playerList.Count; j++)
            {
                Debug.Log("Give 2 Damage to player index  " + j);
                Sc_GameManager.Instance.playerList[j].TakeDamage(m_numberOfDamage);
            }
        }

        base.SelectedCard(owner);
    }

}
