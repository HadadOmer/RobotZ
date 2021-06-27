using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWin : MonoBehaviour
{  
    //This script needs to be on the enemy spawners parent 


    void Update()
    {
        //Loads the next scene if all the enemy spawners are destroyed
        if (transform.childCount == 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
