using UnityEngine;
using UnityEngine.UI;

public class Sc_PbEffectCard : Sc_PbCard
{
    [Header("Effect Card")]
    public RawImage m_groundImage;

    public override void InitDisplayCard(So_Card c)
    {
        So_Effect e = (So_Effect)c;

        m_groundImage.texture = e.m_icon;

        base.InitDisplayCard(c);
    }
}
