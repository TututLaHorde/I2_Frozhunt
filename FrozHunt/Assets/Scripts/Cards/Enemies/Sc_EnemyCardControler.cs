using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sc_EnemyCardControler : MonoBehaviour
{
    public So_Enemy m_CardInfo;
    public GameObject m_Image;
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

    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        m_Health -= damage;
        if (m_Health < 0)
        {
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

    private void Dead()
    {
        Debug.Log("Enemy is Dead, You Win this Battle");
    }

    //abstract public void Competence();
}
