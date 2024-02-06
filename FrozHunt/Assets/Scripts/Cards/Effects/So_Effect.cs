using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card/Effet")]
public class So_Effect : So_Card
{
    [Header("Effect Card")]
    public string m_effectType;
    public bool m_canDiscardCard = true;

    public virtual void UseEffect() { }
}
