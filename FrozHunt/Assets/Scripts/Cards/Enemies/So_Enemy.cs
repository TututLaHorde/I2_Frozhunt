using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card/Enemy")]
public class So_Enemy : So_Card
{
    public int HealthPoint;
    public int AttackDamage;
    public int MeatDrop;
    public int Power;
    public Sprite CardArt;
}
