using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public GameObject creditPanel;
    // Start is called before the first frame update
    void Start()
    {
        ResetValues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ResetValues()
    {
        creditPanel.SetActive(false);
    }
    public void CreditBTN()
    {
        creditPanel.SetActive(true);
    }
    public void BackBTN()
    {
        creditPanel.SetActive(false);
    }
    public void ExitBTN()
    {
        Application.Quit();
    }
    public void MainMenuBTN()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
