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
        SC_MusicManager.Instance.windStop();
        SC_MusicManager.Instance.menuMusic(MenuMusic.menu2);
        SceneManager.LoadScene("MainMenuScene"); 
    }

    public void ChooseChar()
    {
        Time.timeScale = 1f;
        SC_MusicManager.Instance.windStop();
        SC_MusicManager.Instance.menuMusic(MenuMusic.menu2);     
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
        SC_MusicManager.Instance.musicStop();
        SC_MusicManager.Instance.ChangeAmbient(WindEffect.lightWind);
        SceneManager.LoadScene("ProtoGameScene");
    }

    public void LoadCreditScene()
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void LoadVictory()
    {
        SC_MusicManager.Instance.windStop();
        SC_MusicManager.Instance.menuMusic(MenuMusic.victoire);
        SceneManager.LoadScene("VictoryScene");
    }

    public void LoadDefeat()
    {
        SC_MusicManager.Instance.windStop();
        SC_MusicManager.Instance.menuMusic(MenuMusic.gameover);
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
