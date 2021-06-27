using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenuHandler : MonoBehaviour
{
    public bool gameLost;
    public bool paused;

    GameObject panel;
  
    // Start is called before the first frame update
    void Start()
    {       
        panel = transform.Find("Panel").gameObject;
        //Activates resume to make the game running and hide the pause panel
        Resume();
        gameLost = false;
    }
    private void Update()
    {
        if (paused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
            Pause();
        else if (Input.GetKeyDown(KeyCode.Escape) && paused && !gameLost)
            Resume();
        else if (gameLost)
            GameLost();

    }
    //Pauses the game and displays game paused on text
    void Pause()
    {
        paused = true;
        panel.SetActive(true);
        panel.GetComponentInChildren<Text>().text = "Game Paused";
    }
    //close the pause panel and resume the game
    void Resume()
    {      
        paused = false;
        panel.SetActive(false);
    }
    //Pauses the game and displays game lost on text
    public void GameLost()
    {
        gameLost = true;
        paused = true;
        panel.SetActive(true);
        panel.GetComponentInChildren<Text>().text = "You lost";
    }


    //Functions for buttons

    //Loads the scene with index 0 in build
    public void LoadMainScene()
    {
        SceneManager.LoadScene(0);
    }   
    //Loads current scene again
    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //Loads the panel specifed
  
}
