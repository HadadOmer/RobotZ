using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEngine : MonoBehaviour
{
    //The shooting engine need to be a component of the fire location

    public GameObject projectilePrefab;
    GameObject temp;
    // Start is called before the first frame update 
    public void ShootProjectile(Vector2 velocity)
    {
        //Creates a new game object and give it the projectile prefab and its rotation and the fire location position
        temp=Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        //Give it the defined velocity
        temp.GetComponent<Rigidbody2D>().velocity = velocity;
    }
    
}
