using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card/Effet")]
public class So_Effect : So_Card
{
    [Header("Effect Card")]
    public string m_effectType;

    public virtual void UseEffect() {}
}
