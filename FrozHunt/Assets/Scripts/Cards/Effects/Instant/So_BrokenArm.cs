using UnityEngine;

[CreateAssetMenu(fileName = "BrokenArm", menuName = "Card/Instant/BrokenArm")]
public class So_BrokenArm : So_Instant
{
    [Header("BrokenArm")]
    public int m_damage = -4;

    public override void SelectedCard(GameObject owner)
    {
        Sc_FightManager.Instance.m_PlayerAttaque += m_damage;

        for (int j = 0; j < Sc_GameManager.Instance.playerList.Count; j++)
        {
            Sc_GameManager.Instance.playerList[j].SetMalusOfDamage(Sc_FightManager.Instance.m_PlayerAttaque);
        }

        base.SelectedCard(owner);
    }
}
