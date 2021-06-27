using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEngine : MonoBehaviour
{
    public float HP;//The objects health points
    public bool alive;//Is the object alive 
    public float despawnTime;//The time this object takes to despawn
    Animator animator;
    Timer despawnTimer;//A timer for object destroy after death
    SpriteRenderer renderer;

    // Start is called before the first frame update
    void Awake()
    {
        ResetValues();
    }
    void ResetValues()
    {
        if (HP == 0)
            HP = 100;
        alive = HP>0;
        if (despawnTime == 0)
            despawnTime = 5;
        if(GetComponent<Animator>()!=null)
            animator = GetComponent<Animator>();
        renderer=transform.gameObject.GetComponent<SpriteRenderer>();
        
    }
    // Update is called once per frame
    void Update()
    {
        if (alive&&HP <= 0)
            Kill();
        if (despawnTimer != null && despawnTimer.time == 0)
            GameObject.Destroy(gameObject);
           
    }
    public void TakeDownHP(float value)
    {
        //Makes the object blink when it takes damage
        StartCoroutine(Blink());
        //Take down hp by the amount in value
        HP -= value;
    }
    public void Kill()
    {
        alive = false;
        HP = 0;
        //Activates the despawn timer
        despawnTimer = gameObject.AddComponent<Timer>();
        despawnTimer.SetTimer(despawnTime);
        //Triggers the death animation
        if(animator!=null)
            animator.SetTrigger("Dead");
        
    }

    IEnumerator Blink()
    {
        renderer.enabled = !renderer.enabled;
        yield return new WaitForSeconds(0.2f);
        renderer.enabled = !renderer.enabled;
    }
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Takes down hp from the object based on the hitter damage
        if (collision.gameObject.tag == "Projectile")
            TakeDownHP(collision.gameObject.GetComponent<ProjectileEngine>().damage);
        else if(collision.gameObject.tag == "Melee")
            TakeDownHP(collision.gameObject.GetComponent<MeleeEngine>().damage);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
            Kill();
    }
}
