using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card/Default")]
public class So_Card : ScriptableObject
{
    [Header("Base Card")]
    public string m_name;
    public string m_description;
    public Texture2D m_icon;
    public GameObject m_prebafInBoard;

    public virtual void SelectedCard(GameObject owner) { }
}
