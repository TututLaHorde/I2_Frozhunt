using UnityEngine;

public enum Capacity
{
    Flee, Basic
}

[CreateAssetMenu(fileName = "Card", menuName = "Card/Enemy")]
public class So_Enemy : So_Card
{
    public Capacity EnemyCapacity;
    public int HealthPoint;
    public int AttackDamage;
    public int MeatDrop;
    public int Power;
    public Sprite CardArt;

    public override void SelectedCard(GameObject owner)
    {
        Sc_PbCard card = owner.GetComponent<Sc_PbCard>();
        Sc_BoardManager.Instance.RemoveAllPrefabCardWithout(card.m_indexPosition);


        if (owner.TryGetComponent<Sc_EnemyCardControler>(out var e))
            Sc_FightManager.Instance.StartFight(e);
    }
}
