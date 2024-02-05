using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sc_PopUpManager : MonoBehaviour
{
    public static Sc_PopUpManager Instance;

    [Header("Heal PopUp")]
    public GameObject HealPopUp;
    public TextMeshProUGUI Name1;
    public TextMeshProUGUI Name2;
    public TextMeshProUGUI Life1;
    public TextMeshProUGUI Life2;
    public GameObject Image1;
    public GameObject Image2;




    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SetHealPopUp(Sc_PlayerCardControler player1, Sc_PlayerCardControler player2)
    {
        Name1.text = player1.m_NameTxt.text;
        Life1.text = player1.m_HPTxt.text + "/20";
        Image1.GetComponent<Image>().sprite = player1.m_Image.GetComponent<Sprite>();

        Name2.text = player2.m_NameTxt.text;
        Life2.text = player2.m_HPTxt.text + "/20";
        Image2.GetComponent<Image>().sprite = player2.m_Image.GetComponent<Sprite>();
    }

    public void HealPlayer1()
    {
        Sc_GameManager.Instance.playerList[0].Heal(5);
        Debug.Log("Heal P1");
        HealPopUp.SetActive(false);
    }
    public void HealPlayer2()
    {
        Sc_GameManager.Instance.playerList[1].Heal(5);
        Debug.Log("Heal P2");
        HealPopUp.SetActive(false);
    }
}
