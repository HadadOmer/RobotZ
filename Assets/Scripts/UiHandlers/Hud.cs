using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    //Members
    [Header ("Player Stats")]
    public float Health;//The health of the player 
    public float Money;//The amount of coins the player has collected
    public float Ammo;//The ammo of the player 

    HealthEngine playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        BaseValues();
        DisplayAllStats();
    }
    //Gives the members basic values
    void BaseValues()
    {
        if (Health == 0)
            Health = 100;
        Money = 0;
        if (Ammo == 0)
            Ammo = 15;

        playerHealth = GameObject.Find("Player").GetComponent<HealthEngine>();
    }
    private void Update()
    {
        Health = playerHealth.HP;
        DisplayAllStats();
    }
    //Displayes the updated value of a stat in the game object inserted
    public void DisplayStat(string statName,GameObject statText)
    {
        //Refernce the variable which name is the same as stat name and puts its int value into statValue
        float statValue = (float)this.GetType().GetField(statName).GetValue(this);
        statText.GetComponent<Text>().text =statValue.ToString() ;
    }
    //Displayes all the stats in stats game object
    void DisplayAllStats()
    {
        GameObject statsContainer = transform.Find("Stats").gameObject;
        foreach (Transform t in statsContainer.transform)
            DisplayStat(t.name,t.gameObject);
    }

    //Adds the addition to the value specified
    public void AddToValue(string valueName,float addition)
    {
        float value= (float)this.GetType().GetField(valueName).GetValue(this);
        this.GetType().GetField(valueName).SetValue(this,value + addition);
    }
}
