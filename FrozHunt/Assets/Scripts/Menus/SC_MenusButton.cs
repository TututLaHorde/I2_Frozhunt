using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_MenusButton : MonoBehaviour
{
    [SerializeField] private GameObject m_pauseMenu;
    [SerializeField] private List<So_CardPlayer> m_players = new();
    public List<Sc_PlayerCardControler> m_playersControler = new();

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene"); 
    }

    public void ChooseChar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("ChooseCharacterScene");
    }
    public void LoadLore()
    {
        for(int i = 0; i < SC_PlayerCount.instance.m_count ; i++) 
        {
            m_players.Add(m_playersControler[i].m_CardInfo);
        }
        Save init = new Save(m_players);
        Sc_SaveData.Instance.SaveToJson(init);
        SceneManager.LoadScene("LoreScene");
    }
    public void LoadPlayScene()
    {
        SceneManager.LoadScene("ProtoGameScene");
    }

    public void LoadCreditScene()
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void LoadVictory()
    {
        SceneManager.LoadScene("VictoryScene");
    }

    public void LoadDefeat()
    {
        SceneManager.LoadScene("DefeatScene");
    }

    public void resume()
    {
        Time.timeScale = 1f;
        m_pauseMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
