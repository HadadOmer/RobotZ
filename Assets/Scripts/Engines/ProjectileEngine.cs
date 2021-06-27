using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEngine : MonoBehaviour
{
    public float damage;
    public Vector2 velocity;
    // Start is called before the first frame update
    void Awake()
    {
        ResetValues();       
    }
    private void ResetValues()
    {
        if (damage == 0)
            damage = 50;
        if (velocity == Vector2.zero)
            velocity = new Vector2(15,0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject.Destroy(gameObject);
    }
}
