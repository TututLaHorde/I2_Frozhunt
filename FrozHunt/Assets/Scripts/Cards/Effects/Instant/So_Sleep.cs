using UnityEngine;

[CreateAssetMenu(fileName = "Sleep", menuName = "Card/Instant/Sleep")]
public class So_Sleep : So_Instant
{
    [Header("Sleep")]
    public int m_heal = 2;

    public override void SelectedCard(GameObject owner)
    {
        for (int j = 0; j < Sc_GameManager.Instance.playerList.Count; j++)
        {
            Sc_GameManager.Instance.playerList[j].Heal(m_heal);
        }
        base.SelectedCard(owner);
    }
}
