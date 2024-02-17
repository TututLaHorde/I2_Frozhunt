using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card/Effet/Keepable")]
public class So_Keepable : So_Effect
{
    public override void SelectedCard(GameObject owner)
    {
        bool canSet = Sc_BoardManager.Instance.AddEffectCardInHand(this);
        Sc_PbCard c = owner.GetComponent<Sc_PbCard>();

        if (canSet)
        {
            Sc_BoardManager.Instance.InstantiateBonusCard(this);
        }

        Destroy(owner);
    }

    public override void UseEffect()
    {
        Debug.Log("Use Effect");
    }
}
