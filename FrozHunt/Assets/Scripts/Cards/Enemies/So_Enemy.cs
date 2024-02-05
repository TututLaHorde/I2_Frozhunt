using System.Collections;
using System.Collections.Generic;
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
        if (owner.TryGetComponent<Sc_EnemyCardControler>(out var e))
            Sc_FightManager.Instance.StartFight(e);
    }
}
