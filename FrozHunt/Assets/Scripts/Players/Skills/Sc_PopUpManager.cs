using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Sc_PopUpManager : MonoBehaviour
{
    public static Sc_PopUpManager Instance;

    [Header("Heal PopUp")]
    public GameObject ButtonQuit;
    public GameObject HealPopUp;
    public TextMeshProUGUI Name1;
    public TextMeshProUGUI Name2;
    public TextMeshProUGUI Life1;
    public TextMeshProUGUI Life2;
    public GameObject Image1;
    public GameObject Image2;
    public GameObject PlayersHealCard;


    private int m_healValue;
    private int m_indexCardBonus = 0;

    private bool m_isMedicine = false;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        SetNumberOfPlayer(Sc_GameManager.Instance.playerList.Count);
    }

    public void SetHealPopUp(bool canQuit)
    {
        m_isMedicine = canQuit;

        for(int i = 0; i< Sc_GameManager.Instance.playerList.Count; i++) 
        {
            SetHealCard(PlayersHealCard.transform.GetChild(i), Sc_GameManager.Instance.playerList[i]);
        }

        ButtonQuit.SetActive(canQuit);
    }

    public void SetHealCard(Transform card, Sc_PlayerCardControler player)
    {
        card.GetChild(2).GetComponent<TextMeshProUGUI>().text = player.m_NameTxt.text;
        card.GetChild(3).GetComponent<TextMeshProUGUI>().text = player.m_HPTxt.text + "/20";
        card.GetChild(0).GetComponent<Image>().sprite = player.m_CardInfo.BigCardArt;
        
    }

    public void SetHealthValue(int h)
    {
        m_healValue = h;
    }

    public void HealPlayer(int playerIndex)
    {
        Sc_GameManager.Instance.playerList[playerIndex].Heal(m_healValue);
        Debug.Log("Heal P1");
        HealPopUp.SetActive(false);
        if(m_isMedicine)
            Sc_BoardManager.Instance.RemoveBonusCard(m_indexCardBonus);
    }
    //public void HealPlayer2()
    //{
    //    Sc_GameManager.Instance.playerList[1].Heal(m_healValue);
    //    Debug.Log("Heal P2");
    //    HealPopUp.SetActive(false);
    //    if (m_isMedicine)
    //        Sc_BoardManager.Instance.RemoveBonusCard(m_indexCardBonus);
    //}

    public void QuitHealPopUp()
    {
        HealPopUp.SetActive(false);
    }

    public void SetNumberOfPlayer(int numberOfPlayer)
    {
        for(int i = 0; i<numberOfPlayer; i++)
        {
            PlayersHealCard.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    public void SetIndexCardBonus(int value) => m_indexCardBonus = value;
}
