using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sc_PbEffectCard : Sc_PbCard
{
    public Text m_nameText;
    public Text m_effectTypeText;
    public Text m_descriptionText;

    public override void InitDisplayCard(So_Card c)
    {
        So_Effect e = (So_Effect)c;

        m_nameText.text = e.m_name;
        m_effectTypeText.text = e.m_effectType;
        m_descriptionText.text = e.m_description;

        base.InitDisplayCard(c);
    }
}
