using UnityEngine;

[CreateAssetMenu(fileName = "Coated", menuName = "Card/Instant/Coated")]
public class So_Coated : So_Instant
{
    [Header("Coated")]
    public int m_meatPlus = 2;

    public override void SelectedCard(GameObject owner)
    {
        Sc_FightManager.Instance.m_MeatPlus += m_meatPlus;
        base.SelectedCard(owner);
    }
}
