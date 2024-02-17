using UnityEngine;

[CreateAssetMenu(fileName = "Bag", menuName = "Card/Keepable/Bag")]
public class So_Bag : So_Keepable
{
    [Header("Bag")]
    public int m_numberOfPlace = 2;
    public override void SelectedCard(GameObject owner)
    {
        Sc_BoardManager.Instance.m_maxBonusCard += m_numberOfPlace;
        base.SelectedCard(owner);
    }
}
