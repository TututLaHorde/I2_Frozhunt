using System.Collections.Generic;
using UnityEngine;

public class Sc_SaveData : MonoBehaviour
{
    public static Sc_SaveData Instance;

    public GameObject m_PlayersCard;
    public Save cards = new Save();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            LoadFromJson() ;
        if(Input.GetKeyDown(KeyCode.S))
            SaveToJson(cards);
    }


    public void SaveToJson(Save save)
    {
        cards = save;
        string Save = JsonUtility.ToJson(cards);
        string FilePath = Application.persistentDataPath + "/Save.json";
        System.IO.File.WriteAllText(FilePath, Save);
    }

    public void LoadFromJson()
    {
        string FilePath = Application.persistentDataPath + "/Save.json";
        string Save = System.IO.File.ReadAllText(FilePath);
        cards = JsonUtility.FromJson<Save>(Save);
        Sc_GameManager.Instance.playerList.Clear();

        for (int i = 0; i< cards.cardPlayers.Count; i++)
        {
            GameObject player = m_PlayersCard.transform.GetChild(i).gameObject;
            Sc_GameManager.Instance.playerList.Add(player.GetComponent<Sc_PlayerCardControler>());
            player.SetActive(true);

            Sc_GameManager.Instance.playerList[i].m_CardInfo = cards.cardPlayers[i];
        }
    }

}

[System.Serializable]
public class Save
{

    public List<So_CardPlayer> cardPlayers = new();
    public Save(List<So_CardPlayer> card)
    {
        cardPlayers = card;
    }
    public Save()
    {
    }
}