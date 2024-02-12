using TMPro;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Sc_NewPlayerController : MonoBehaviour
{
    [Header("Card Data")]
    public So_CardPlayer m_CardInfo;

    [Header("Card Image")]
    public GameObject m_Image;

    [Header("Card Text")]
    public TextMeshProUGUI m_NameTxt;
    public TextMeshProUGUI m_DamageTxt;
    public TextMeshProUGUI m_HPTxt;
    public TextMeshProUGUI m_DescriptionTxt;
    public TextMeshProUGUI m_AttackNameTxt;

    private Sc_PlayerCompetence m_competence;

    private PlayerCard m_PlayerCard;

    private Button m_Button;

    private int m_Health = 0;
    private int m_maxHealth = 0;
    private int m_damage = 0;
    [SerializeField] private bool m_canAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        m_Button = gameObject.GetComponentInChildren<Button>();
        Assign();
    }

    public void Assign()
    {
        m_PlayerCard = m_CardInfo.playerCard;

        m_maxHealth = m_CardInfo.life;
        m_Health = m_CardInfo.life;
        m_damage = m_CardInfo.damage;

        m_Image.GetComponent<Image>().sprite = m_CardInfo.cardArt;

        m_NameTxt.text = m_CardInfo.playerCard.ToString();
        m_DamageTxt.text = m_CardInfo.damage.ToString();
        m_DescriptionTxt.text = m_CardInfo.description;
        m_HPTxt.text = m_CardInfo.life.ToString();
        m_AttackNameTxt.text = m_CardInfo.attackName;

        SetMyCompetence();
    }

    private void SetMyCompetence() // Add the component with the good critical fonction  
    {
        Debug.Log("Set Competence");
        switch (m_PlayerCard)
        {
            case PlayerCard.Lance: m_competence = gameObject.AddComponent<Sc_DoubleDmgCritique>(); break;
            case PlayerCard.Massue: m_competence = gameObject.AddComponent<Sc_StunCompetence>(); break;
            case PlayerCard.Druide: m_competence = gameObject.AddComponent<Sc_HealCompetence>(); break;
        }
    }
}