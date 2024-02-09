using UnityEngine.UI;

public class Sc_PbEffectCard : Sc_PbCard
{
    public Text m_nameText;
    public Text m_effectTypeText;
    public Text m_descriptionText;
    public RawImage m_groundImage;

    public override void InitDisplayCard(So_Card c)
    {
        So_Effect e = (So_Effect)c;

        m_nameText.text = e.m_name;
        m_effectTypeText.text = e.m_effectType;
        m_descriptionText.text = e.m_description;
        m_groundImage.texture = e.m_icon;

        base.InitDisplayCard(c);
    }
}
