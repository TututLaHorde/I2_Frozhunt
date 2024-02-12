using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sc_TutorialManager : MonoBehaviour
{
    public static Sc_TutorialManager Instance { get; private set; }

    public GameObject m_objectiveWindow;
    public GameObject m_drawWindow;
    public GameObject m_confirmAttackWindow;
    public GameObject m_handCapacityWindow;
    public GameObject m_eventTuto;
    public GameObject m_AttackSelectTuto;

    public Image m_panelToFade;
    [SerializeField] private TextMeshProUGUI m_textToFade;
    private Color m_imageEndColor;

    public bool m_tutoIsOn;// from checking if tuto is activated
    public bool m_isFirstFight = true;

    public float m_timeToFade;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        ShowTuto();
        m_handCapacityWindow.SetActive(true);
    }

    private void Start()
    {
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

    public void ShowEventTuto(bool state)
    {
        StartCoroutine(FadeCanvas(m_panelToFade, m_textToFade, 1f, state));
    }


    IEnumerator FadeCanvas(Image image, TextMeshProUGUI text, float duration, bool state) //true : fade in, false : fade out
    {
        Debug.Log(" my state is " + state);
        if(state)
            m_eventTuto.SetActive(true);


        float elapsedTime = 0f;

        Color imageStartColor = image.color;
        Color imageEndColor = image.color;
        
        Color textStartColor = image.color;
        Color textEndColor = text.color;

        imageStartColor.a = 0f;
        textStartColor.a = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            if (state)
            {
                image.color = Color.Lerp(imageStartColor, imageEndColor, elapsedTime / duration);
                text.color = Color.Lerp(imageStartColor, textStartColor, elapsedTime / duration);
            }
            else
            {
                image.color = Color.Lerp(imageEndColor, imageStartColor, elapsedTime / duration);
                text.color = Color.Lerp(textEndColor, textStartColor, elapsedTime / duration);
            }
            
            yield return null;
        }

        image.color = imageEndColor;
        text.color = textEndColor;
        
        if(!state)
            m_eventTuto.SetActive(false);

    }

}
