using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_MenusButton : MonoBehaviour
{
    [SerializeField] private GameObject m_pauseMenu;
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene"); 
    }
    public void LoadLore()
    {
        SceneManager.LoadScene("LoreScene");
    }

    public void LoadPlayScene()
    {
        SceneManager.LoadScene("ProtoGameMenu");
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
        m_pauseMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
