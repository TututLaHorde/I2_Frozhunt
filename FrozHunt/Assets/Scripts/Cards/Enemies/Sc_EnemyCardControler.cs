using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sc_EnemyCardControler : Sc_PbCard
{
    [Header("Card Data")]
    public So_Enemy m_CardInfo;

    [Header("Card Image")]
    public GameObject m_Image;

    [Header("Card Text")]
    public TextMeshProUGUI m_NameTxt;
    public TextMeshProUGUI m_DamageTxt;
    public TextMeshProUGUI m_HPTxt;
    public TextMeshProUGUI m_PowerTxt;
    public TextMeshProUGUI m_DescriptionTxt;
    public TextMeshProUGUI m_MeatDropTxt;

    private int m_Health = 0;
    private int m_maxHealth = 0;
    private int m_damage = 0;
    private int m_meat = 0;
    private int m_power = 0;

    private Capacity m_enemyCapacity;

    private Sc_enemyCompetence m_competence;

    private bool m_stun = false;

    private Sc_HearthAnim1 m_heartAnim;

    private void Start()
    {
        m_heartAnim = GetComponentInChildren<Sc_HearthAnim1>();
        m_heartAnim.m_MaxLife = m_maxHealth;
        m_heartAnim.m_CurrentLife = m_Health;
    }

    public override void InitDisplayCard(So_Card c)
    {
        base.InitDisplayCard(c);

        m_CardInfo = c as So_Enemy;

        m_enemyCapacity = m_CardInfo.EnemyCapacity;

        m_maxHealth = m_CardInfo.HealthPoint;
        m_Health = m_CardInfo.HealthPoint;

        m_maxHealth = Sc_GameManager.Instance.ScaleValues(m_Health);
        m_Health = Sc_GameManager.Instance.ScaleValues(m_Health);

        m_damage = m_CardInfo.AttackDamage;
        m_meat = m_CardInfo.MeatDrop;
        m_power = m_CardInfo.Power;
        m_Image.GetComponent<Image>().sprite = m_CardInfo.CardArt;
        m_NameTxt.text = m_CardInfo.m_name.ToString();
        m_DamageTxt.text = m_CardInfo.AttackDamage.ToString();
        m_DescriptionTxt.text = m_CardInfo.m_description;

        m_HPTxt.text = m_Health.ToString();

        m_MeatDropTxt.text = m_CardInfo.MeatDrop.ToString();
        m_PowerTxt.text = m_CardInfo.Power.ToString();

        SetMyCompetence();
    }

    public void TakeDamage(int damage)
    {
        if (m_Health <= 0)
            return;
        m_Health -= damage;
        if (m_Health <= 0)
        {
            Sc_GameManager.Instance.AddFood(Meat);
            m_Health = 0;
            Dead();
        }
        m_HPTxt.text = m_Health.ToString();
        m_heartAnim.m_CurrentLife = m_Health;
    }

    public void Heal(int heal)
    {
        m_Health += heal;
        if (m_Health > m_maxHealth)
        {
            m_Health = m_maxHealth;
        }
        m_HPTxt.text = m_Health.ToString();
        m_heartAnim.m_CurrentLife = m_Health;
    }

    public int Health
    {
        get { return m_Health; }

        set
        {
            m_Health = value;
            if (m_Health > m_maxHealth)
                m_maxHealth = m_Health;
            m_HPTxt.text = m_Health.ToString();
        }
    }

    public int Damage
    {
        get { return m_damage; }

        set
        {
            m_damage = value;
            m_DamageTxt.text = m_damage.ToString();
        }
    }
    public int Power
    {
        get { return m_power; }

        set 
        { 
            m_power = value;  
            m_PowerTxt.text = m_power.ToString();
        }
    }
    public int Meat
    {
        get { return m_meat; }

        set
        {
            m_meat = value;
            m_MeatDropTxt.text = m_meat.ToString();
        }
    }


    private void Dead()
    {
        Sc_FightManager.Instance.EndFight();
    }

    public bool Stun
    {
        get { return m_stun;}
        set { m_stun = value; }
    }

    public void Competence(Action onEndCompetence)
    {
        m_competence.Competence(onEndCompetence);
    }

    private void SetMyCompetence() // Add the component with the good critical fonction  
    {
        switch (m_enemyCapacity)
        {
            case Capacity.Flee: m_competence = gameObject.AddComponent<FleeCompetenceEnemy>(); break;
            case Capacity.Basic: m_competence = gameObject.AddComponent<Sc_enemyCompetence>(); break;
        }
    }
}
