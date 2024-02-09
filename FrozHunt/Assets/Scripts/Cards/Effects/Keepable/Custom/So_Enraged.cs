using UnityEngine;

[CreateAssetMenu(fileName = "Enraged", menuName = "Card/Keepable/Enraged")]
public class So_Enraged : So_Passive
{
    [Header("Enraged")]
    public int m_enragedDiceBonus = 2;

    public override void UseEffect()
    {
        if (Sc_FightManager.Instance.m_enragedPlayerBonus.Equals(1))
            return;

        if (Sc_GameManager.Instance.m_turnPhase != Sc_GameManager.eTurnPhase.Attack)
            return;

        Sc_PlayerCardControler playerAttack = Sc_FightManager.Instance.m_lastPlayer;

        Sc_FightManager f = Sc_FightManager.Instance;
        int newResult = f.m_diceResult.Item1 + f.m_diceResult.Item2 + m_enragedDiceBonus;

        if (newResult >= f.m_Enemy.GetPower())
        {
            f.m_infoDicePopUp.SetAttackStateText(AttackState.Success);
            f.m_IsPlayerAttack = false;

            if (f.m_diceResult.Item2 > 4)
            {
                f.m_infoDicePopUp.SetAbilityStateText(AbilityState.CriticalAttack);
                f.m_isCrit = true;
            }

            if (Sc_GameManager.Instance.GetFood() > 1)
                Sc_GameManager.Instance.AddFood(-1);
            else
                playerAttack.TakeDamage(2);
        }
    }

    public override void SelectedCard(GameObject owner)
    {
        base.SelectedCard(owner);    
    }
}
