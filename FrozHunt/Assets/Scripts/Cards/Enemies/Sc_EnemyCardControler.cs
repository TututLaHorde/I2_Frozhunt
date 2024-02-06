using System.Collections;
using System.Collections.Generic;
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

    public override void InitDisplayCard(So_Card c)
    {
        base.InitDisplayCard(c);

        m_CardInfo = c as So_Enemy;

        m_enemyCapacity = m_CardInfo.EnemyCapacity;

        m_maxHealth = m_CardInfo.HealthPoint;
        m_Health = m_CardInfo.HealthPoint;
        m_damage = m_CardInfo.AttackDamage;
        m_meat = m_CardInfo.MeatDrop;
        m_power = m_CardInfo.Power;
        m_Image.GetComponent<Image>().sprite = m_CardInfo.CardArt;
        m_NameTxt.text = m_CardInfo.m_name.ToString();
        m_DamageTxt.text = m_CardInfo.AttackDamage.ToString();
        m_DescriptionTxt.text = m_CardInfo.m_description;
        m_HPTxt.text = m_CardInfo.HealthPoint.ToString();
        m_MeatDropTxt.text = m_CardInfo.MeatDrop.ToString();
        m_PowerTxt.text = m_CardInfo.Power.ToString();

        SetMyCompetence();
    }

    public void TakeDamage(int damage)
    {
        m_Health -= damage;
        if (m_Health <= 0)
        {
            print(GetMeat() + "this is how much food that should be given");
            Sc_GameManager.Instance.AddFood(GetMeat());
            m_Health = 0;
            Dead();
        }
        m_HPTxt.text = m_Health.ToString();
        Debug.Log("Enemy Take Damage");
    }

    public void Heal(int heal)
    {
        m_Health += heal;
        if (m_Health > m_maxHealth)
        {
            m_Health = m_maxHealth;
        }
        m_HPTxt.text = m_Health.ToString();
    }

    public int GetDamage() => m_damage;
    public int GetPower() => m_power;
    public int GetMeat() => m_meat;

    private void Dead()
    {
        Sc_FightManager.Instance.EndFight();
        Debug.Log("Enemy is Dead, You Win this Battle");
    }

    public bool Stun
    {
        get { return m_stun;}
        set { m_stun = value; }
    }

    public void Competence()
    {
        m_competence.Competence();
    }

    private void SetMyCompetence() // Add the component with the good critical fonction  
    {
        switch (m_enemyCapacity)
        {
            case Capacity.Flee: m_competence = gameObject.AddComponent<FleeCompetenceEnemy>(); break;
            case Capacity.Basic: m_competence = gameObject.AddComponent<Sc_enemyCompetence>(); break;
        }
    }

    //abstract public void Competence();
}
