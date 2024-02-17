using UnityEngine;

[CreateAssetMenu(fileName = "Blizzard", menuName = "Card/Instant/Blizzard")]
public class So_Blizzard : So_Instant
{
    [Header("Blizzard")]
    public int m_damage = 2;

    public override void SelectedCard(GameObject owner)
    {
        for (int j = 0; j < Sc_GameManager.Instance.playerList.Count; j++)
        {
            Sc_GameManager.Instance.playerList[j].TakeDamage(m_damage);
        }
        base.SelectedCard(owner);
    }
}
