using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuotrialSpawner : MonoBehaviour
{

    public GameObject tutorialEndPanel;
    public GameObject nestInstruction;

    InGameMenuHandler gameMenu;
    void Start()
    {
        Time.timeScale = 1;
        tutorialEndPanel.SetActive(false);
        gameMenu = GameObject.Find("InGameMenu").GetComponent<InGameMenuHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        TutorialCompleted();
    }
    public void TutorialCompleted()
    {
        if (!GetComponent<HealthEngine>().alive)
        {
            tutorialEndPanel.SetActive(true);
            nestInstruction.SetActive(false);
            gameMenu.paused = true;
        }
    }
}
