using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card/Effet/Instant")]
public class So_Instant : So_Effect
{
    public override void SelectedCard(GameObject owner)
    {
        Sc_BoardManager.Instance.RemoveAllPrefabCard();
    }
}
