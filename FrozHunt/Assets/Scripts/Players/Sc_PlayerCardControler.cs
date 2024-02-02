using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sc_PlayerCardControler : MonoBehaviour
{
    public So_CardPlayer m_CardInfo;
    public GameObject m_Image;
    public TextMeshProUGUI m_NameTxt;
    public TextMeshProUGUI m_DamageTxt;
    public TextMeshProUGUI m_PVTxt;
    public TextMeshProUGUI m_DescriptionTxt;
    public TextMeshProUGUI m_AttackNameTxt;

    private int m_Health = 0;
    private int m_maxHealth = 0;
    private int m_damage = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_maxHealth = m_CardInfo.life;
        m_Health = m_CardInfo.life;
        m_damage = m_CardInfo.damage;
        m_Image.GetComponent<Image>().sprite = m_CardInfo.cardArt;
        m_NameTxt.text = m_CardInfo.playerCard.ToString();
        m_DamageTxt.text = m_CardInfo.damage.ToString();
        m_DescriptionTxt.text = m_CardInfo.description;
        m_PVTxt.text = m_CardInfo.life.ToString();
        m_AttackNameTxt.text = m_CardInfo.attackName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        m_Health -= damage;
        if(m_Health < 0) 
        {
            Dead();
        }
        m_PVTxt.text = m_Health.ToString();
    }

    public void Heal(int heal)
    {
        m_Health += heal;
        if(m_Health > m_maxHealth) 
        {
            m_Health = m_maxHealth;
        }
        m_PVTxt.text = m_Health.ToString();
    }

    public int GetDamage() => m_damage;

    private void Dead()
    {
        Debug.Log("Player is Dead, You Loose");
    }

    //abstract public void Competence();
}
