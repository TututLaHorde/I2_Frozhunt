using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public enum PlayerCard
{
    Massue, Druide, Lance, test1, test2, test3
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerCardInfo", order = 1)]

public class So_CardPlayer : ScriptableObject
{
    public PlayerCard playerCard;
    public int life;
    public int damage;
    public Sprite cardArt;
    public string description;
    public string attackName;
    
}
