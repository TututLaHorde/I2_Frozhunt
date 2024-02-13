using UnityEngine;

public enum PlayerCard
{
    Gada, Muni, Sula, test1, test2, test3
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerCardInfo", order = 1)]

public class So_CardPlayer : ScriptableObject
{
    public PlayerCard playerCard;
    public int life;
    public int damage;
    public Sprite cardArt;
    public Sprite BigCardArt;
    public string description;
    public string attackName; 
}
