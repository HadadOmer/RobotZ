using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEngine : MonoBehaviour
{
    public float damage;
    public GameObject player;
    float dir;
    // Start is called before the first frame update
    void Start()
    {
        ResetValues();
    }
    void ResetValues()
    {
        if (damage == 0)
            damage = 50;
        player = GameObject.Find("Player");
    }
    // Update is called once per frame
    void Update()
    {
        //Makes sure the melee game object is in front of the player
        dir = player.GetComponent<MovementEngine>().FacingtDir();
        transform.position = player.transform.position + new Vector3(dir * 0.05f, 0);
    }   
}
