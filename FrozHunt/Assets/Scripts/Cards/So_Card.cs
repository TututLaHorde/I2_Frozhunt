using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card/Default")]
public class So_Card : ScriptableObject
{
    public string m_name;
    public string m_description;
    public Texture2D m_icon;

    public virtual void SelectedCard() {}
}
