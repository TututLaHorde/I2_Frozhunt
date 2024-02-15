using UnityEngine;


[CreateAssetMenu(fileName = "AnimalATK", menuName = "Card/Instant/AnimalATK")]
public class So_AnimalsAttack : So_Instant
{
    public int m_numberOfDamage = 4;
    public int m_lostFood = -2;
    public override void SelectedCard(GameObject owner)
    {
        if (Sc_GameManager.Instance.GetFood() >= Mathf.Abs(m_lostFood))
        {
            Sc_GameManager.Instance.AddFood(m_lostFood);
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
