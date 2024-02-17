using UnityEngine;

[CreateAssetMenu(fileName = "TantalizingSmell", menuName = "Card/Instant/TantalizingSmell")]
public class So_TantalizingSmell : So_Instant
{
    [Header("TantalizingSmell")]
    public int m_powerPlus = 1;

    public override void SelectedCard(GameObject owner)
    {
        Sc_FightManager.Instance.m_PowerPlus += m_powerPlus;
        base.SelectedCard(owner);
    }
}
