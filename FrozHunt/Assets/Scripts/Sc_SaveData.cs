using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class Sc_SaveData : MonoBehaviour
{
    public static Sc_SaveData Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            LoadFromJson(ref Sc_GameManager.Instance.playerList) ;
        if(Input.GetKeyDown(KeyCode.S))
            SaveToJson();
    }
    public Save cards = new Save();
    public void SaveToJson()
    {
        string Save = JsonUtility.ToJson(cards);
        string FilePath = Application.persistentDataPath + "/Save.json";
        Debug.Log(FilePath);
        System.IO.File.WriteAllText(FilePath, Save);
        Debug.Log("Sauvegarder");
    }

    public void LoadFromJson(ref List<Sc_PlayerCardControler> playerList)
    {
        string FilePath = Application.persistentDataPath + "/Save.json";
        string Save = System.IO.File.ReadAllText(FilePath);
        cards = JsonUtility.FromJson<Save>(Save);
        playerList = cards.players;
        for(int i = 0; i< playerList.Count; i++)
        {
            Debug.Log("Player Parameter Loding");
            cards.players[i].m_CardInfo = cards.cardPlayers[i];
        }
        Debug.Log("Load");
    }

}

[System.Serializable]
public class Save
{
    public List<Sc_PlayerCardControler> players = new();
    public List<So_CardPlayer> cardPlayers = new();
}