using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialEngine : MonoBehaviour
{
    //members
    [Header("Characters")]
    public GameObject player;
    public GameObject maleZombie;
    public GameObject femaleZombie;
    public GameObject nest;
    bool jumpOneTime, meleeOneTime, shootOneTime, gJOTime,mZOTime,fmZOTime,nestOneTime; //To make sure the UI objects only turn on once.
    [Header("Instructions")]
    public GameObject moveInstruction; //the movement arrows UI
    public GameObject jumpInstruction; // the jump arrow UI
    public GameObject meleeInstruction; // the melee instruction text
    public GameObject shootInstruction; // the shoot UI
    public GameObject goodJobInstruction; // after he is done with the first 3 show good job UI
    public GameObject maleZombieInstruction; // intrudoction to the male zombie
    public GameObject femaleZombieInstruction; // intrudoction to the female zombie
    public GameObject nestInstruction; //intrudoction to the nest
    [Header("Menu Handler")]
    public InGameMenuHandler menuHandler;//The in game menu handler

    // Start is called before the first frame update
    void Start()
    {
        ResetValues();
    }

    // Update is called once per frame
    void Update()
    {
        DisableInstruction();
    }
    void ResetValues()
    {
        jumpOneTime = true;
        meleeOneTime = true;
        shootOneTime = true;
        gJOTime = true;
        mZOTime = true;
        fmZOTime = true;
        nestOneTime = true;
        jumpInstruction.gameObject.SetActive(false);
        meleeInstruction.gameObject.SetActive(false);
        shootInstruction.gameObject.SetActive(false);
        goodJobInstruction.gameObject.SetActive(false);
        maleZombieInstruction.gameObject.SetActive(false);
        femaleZombieInstruction.gameObject.SetActive(false);
        nestInstruction.gameObject.SetActive(false);
        menuHandler = GameObject.Find("InGameMenu").GetComponent<InGameMenuHandler>();
    }
    public void DisableInstruction()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) //movement instruction completed
        {
            moveInstruction.gameObject.SetActive(false);
            if (jumpOneTime)
            {
                jumpInstruction.SetActive(true);
                jumpOneTime = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space) && jumpOneTime==false) // jumping instruction completed
        {
            jumpInstruction.gameObject.SetActive(false);
            if (meleeOneTime)
            {
                meleeInstruction.SetActive(true);
                meleeOneTime = false;
            }
        }
        if(Input.GetKeyUp(KeyCode.F) && meleeOneTime == false) // melee instruction completed
        {
            meleeInstruction.gameObject.SetActive(false);
            if (shootOneTime)
            {
                shootInstruction.SetActive(true);
                shootOneTime = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.E) && shootOneTime == false) // shooting instruction completed 
        {
            shootInstruction.gameObject.SetActive(false);
            if (gJOTime)
            {
                goodJobInstruction.SetActive(true);
                gJOTime = false;
            }
        }
        if (player.transform.position.x > 15 &&mZOTime && gJOTime==false) //reached a zombie
        {
            goodJobInstruction.SetActive(false);
            mZOTime = false;
            maleZombieInstruction.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Z) || player.transform.position.x>30) //male zombie instruction completed
        {
            maleZombieInstruction.SetActive(false);
        }
        if (player.transform.position.x > 31 && fmZOTime)
        {
            femaleZombieInstruction.gameObject.SetActive(true);
            fmZOTime = false;
        }
        if (Input.GetKeyDown(KeyCode.Z) || player.transform.position.x>45) // female zombie instruction completed
        {
            femaleZombieInstruction.SetActive(false);
        }
        if(player.transform.position.x>45 && nestOneTime) 
        {
            nestInstruction.SetActive(true);
            nestOneTime = false;
        }
        if (!player.GetComponent<HealthEngine>().alive)
            menuHandler.GameLost();
        
    }
    public void NextLevelBTN()
    {
        SceneManager.LoadScene("Level1");
    }
}
