using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AnimalATK", menuName = "Card/Instant/AnimalATK")]
public class So_AnimalsAttack : So_Instant
{
    public int m_numberOfDamage = 2; // for each food you don't have
    public int m_numberOfLostFood = 2;
    public override void SelectedCard(GameObject owner)
    {
        for (int i = 0; i < m_numberOfLostFood; i++) 
        {
            if(Sc_GameManager.Instance.GetFood() > 0 )
            {
                Sc_GameManager.Instance.AddFood(-1);
            }
            else 
            {
                for(int j = 0;  j < Sc_GameManager.Instance.playerList.Count; j++) 
                {
                    Debug.Log("Give 2 Damage to player index  " + j);
                    Sc_GameManager.Instance.playerList[j].TakeDamage(m_numberOfDamage);
                }
            }
        }
        

        base.SelectedCard(owner);
    }
}
