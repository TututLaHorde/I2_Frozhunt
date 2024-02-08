using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_TutorialManager : MonoBehaviour
{
    public static Sc_TutorialManager Instance { get; private set; }

    public GameObject m_objectiveWindow;
    public GameObject m_drawWindow;
    public GameObject m_confirmAttackWindow;
    public GameObject m_handCapacityWindow;
    public bool m_tutoIsOn;// from checking if tuto is activated
    public bool m_isFirstFight = true;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        ShowTuto();
        m_handCapacityWindow.SetActive(true);
    }

    public void ShowTuto()
    {
        if(m_tutoIsOn)
        {
            //objective
            m_objectiveWindow.SetActive(Sc_GameManager.Instance.m_turnCount == 0);

            //draw
            m_drawWindow.SetActive(Sc_GameManager.Instance.m_turnCount <= 2 && Sc_GameManager.Instance.m_turnPhase == Sc_GameManager.eTurnPhase.Draw);

            //hand cards

            //change character

        }

    }

}
