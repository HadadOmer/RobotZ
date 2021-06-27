using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEngine : MonoBehaviour
{
    public GameObject gameCamera;

    float horizontalInput;
    MovementEngine moveEngine;
    HealthEngine healthEngine;
    ShootingEngine shootingEngine;
    InGameMenuHandler gameMenu;
    Hud hud;
    Animator animator;
    Timer shootingCooldown;
    Timer slideTimer;
    float bulletDir;

    AudioSource audioManager;
    public AudioClip[] audioClips = new AudioClip[2]; 
    // Start is called before the first frame update
    void Awake()
    {
        ResetValues();

    }
    void ResetValues()
    {
        gameCamera = GameObject.Find("Main Camera");
        moveEngine = GetComponent<MovementEngine>();
        healthEngine = GetComponent<HealthEngine>();
        shootingEngine = GetComponentInChildren<ShootingEngine>();
        animator = GetComponent<Animator>();
        shootingCooldown = gameObject.AddComponent<Timer>();
        gameMenu = GameObject.Find("InGameMenu").GetComponent<InGameMenuHandler>();
        hud = GameObject.Find("Hud").GetComponent<Hud>();
        slideTimer = gameCamera.AddComponent<Timer>();
        audioManager = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        CheckoutOfMap();
        CameraFollow();
        //If the player is alive allow input
        if (healthEngine.alive)
            GetInput();
        else
            gameMenu.GameLost();
    }

    void CameraFollow()
    {
        //Resets the camera location to player location
        gameCamera.transform.position = transform.position + new Vector3(0, 1, -10);
    }
    void GetInput()
    {
        //Moves the player horizontaly based on the horizontal nput
        horizontalInput = Input.GetAxisRaw("Horizontal");
        moveEngine.HorizontalMovement(horizontalInput);

        //Calls the jump function if space is pressed
        if (Input.GetKey(KeyCode.Space))
            moveEngine.Jump();
        //Enable shooting if player pressed E and cooldown is zero 
        if (Input.GetKey(KeyCode.E) && shootingCooldown.time == 0&&hud.Ammo>0)
        {
            animator.SetBool("shoot", true);
            ShootLaser();
        }
        else if (shootingCooldown.time == 0)
            animator.SetBool("shoot", false);

        //Melee attack if the player presses f
        if (Input.GetKey(KeyCode.F))
            animator.SetBool("Melee", true);
        else
            animator.SetBool("Melee", false);

        //Slides if the player presses c and the slide timer is on zero
        if (Input.GetKeyDown(KeyCode.C) && slideTimer.time == 0)
        {
            animator.SetBool("Slide", true);
            slideTimer.SetTimer(0.5f);
        }
        else if (slideTimer.time == 0)
            animator.SetBool("Slide", false);

    }
    void ShootLaser()
    {
        //Plays the shooting sound effect
        audioManager.clip = audioClips[0];
        audioManager.Play();
        //Shoots a laser projectile       
        bulletDir = moveEngine.FacingtDir();
        shootingEngine.ShootProjectile(new Vector2(30 * bulletDir, 0));
        hud.Ammo--;
        shootingCooldown.SetTimer(0.4f);
    } 
    void CheckoutOfMap()
    {
        //Kills the player if he falls out of the map
        if(transform.position.y<-50)
            healthEngine.Kill();
    }

    public void Addloot(string lootName,float amount)
    {
        //Adds loots to player stats *health is saved in the health engine and not in hud
        if (lootName == "Health")
            healthEngine.HP += amount;
        else
            hud.AddToValue(lootName, amount);
    }
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Takes down hp on collision with enemy
        if (collision.collider.gameObject.tag == "Enemy" && healthEngine.alive)
        {
            healthEngine.TakeDownHP(25);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //On collision with loot add it to player stats and destroy the loot game objects
        if(collision.gameObject.tag=="Loot")
        {
            audioManager.clip = audioClips[1];
            audioManager.Play();
            LootEngine loot = collision.gameObject.GetComponent<LootEngine>();
            Addloot(loot.lootName, loot.amount);
            Destroy(collision.gameObject);
        }
    }
}