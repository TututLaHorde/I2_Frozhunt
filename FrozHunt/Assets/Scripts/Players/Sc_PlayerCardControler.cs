using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[System.Serializable]
public class Sc_PlayerCardControler : MonoBehaviour
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
    private bool m_canAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        m_Button = gameObject.GetComponentInChildren<Button>();

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        m_Health -= damage;
        if(m_Health <= 0) 
        {
            m_Health = 0;
            Dead();
        }
        m_HPTxt.text = m_Health.ToString();
        Debug.Log("Player Take Damage");
    }

    public void Heal(int heal)
    {
        m_Health += heal;
        if(m_Health > m_maxHealth) 
        {
            m_Health = m_maxHealth;
        }
        m_HPTxt.text = m_Health.ToString();
    }

    public int GetDamage() => m_damage;

    private void Dead()
    {
        Debug.Log("Player is Dead, You Loose");
        SceneManager.LoadScene("DefeatScene");
    }

    public void Crit(Sc_EnemyCardControler enemy)
    {
        Debug.Log("Player Crit");
        m_competence.Critique(enemy);
    }

    public void CanCrit()
    {
        Debug.Log("Player Can Crit");

    }

    public bool CanAttack 
    { 
        get { return m_canAttack; } 
        set { m_canAttack = value; } 
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

    public void SetAttackInactive()
    {
       m_Button.interactable = false;
        CanAttack = false;
    }

    public void SetAttackActive()
    {
        m_Button.interactable = true;
        CanAttack = true;
    }

    //abstract public void Competence();
}
