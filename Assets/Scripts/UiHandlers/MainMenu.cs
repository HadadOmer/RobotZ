using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ShowScreen("MainScreen");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Sets active false all the screens in the main menu
    void HideAllScreens()
    {
        foreach (Transform t in transform)
            t.gameObject.SetActive(false);
    }
    //Hides all the screen and than displayes the screen which matches the screenName value
    public void ShowScreen(string screenName)
    {
        HideAllScreens();
        transform.Find(screenName).gameObject.SetActive(true);
    }
    //Loads the scene that is defined in the value sceneName
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        print("Game Quit");
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
