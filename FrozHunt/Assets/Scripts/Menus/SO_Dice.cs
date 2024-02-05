using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Dice",menuName ="Dice")]
public class SO_Dice : ScriptableObject
{
    public Sprite[] m_dice;
    public float m_maxTime;
}
