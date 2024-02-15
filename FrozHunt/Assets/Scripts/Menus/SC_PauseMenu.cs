using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class SC_PauseMenu : MonoBehaviour
{
    [SerializeField]private GameObject m_pauseMenu;
    [SerializeField]private GameObject m_settingsMenu;
    public void PauseMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (m_settingsMenu.activeSelf == false)
            {
                m_pauseMenu.SetActive(!m_pauseMenu.activeSelf);
                if (m_pauseMenu.activeSelf)
                {
                    //pause the game
                    Time.timeScale = 0f;
                }
                else
                {
                    //restart the game
                    Time.timeScale = 1f;
                }
            }
        }
    }
}
