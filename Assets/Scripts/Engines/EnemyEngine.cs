
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEngine : MonoBehaviour
{
    public bool isAlert;//If player is nearby it makes this value true
    public GameObject player;//The player's game object
    public Vector3 startPostion;//The Start position of the enemy object
    public float patrolDistance;//The distance the player patrols

    Animator animator;
    MovementEngine moveEngine;
    HealthEngine healthEngine;
    ShootingEngine shootingEngine;
    Timer shootingCooldown;
    float horizontalInput;
    float distanceToPlayer;//The distance of the enemy from player
    float bulletDir;
    RaycastHit2D hitPlayer;//A raycast which checks if the enemy is in line with player
    void Awake()
    {
        ResetValues();        
    }
    void ResetValues()
    {
        animator = GetComponent<Animator>();
        moveEngine = GetComponent<MovementEngine>();
        healthEngine = GetComponent<HealthEngine>();
        player = GameObject.Find("Player");
        startPostion = transform.position;

        if (patrolDistance == 0)
            patrolDistance = 2f;

        shootingEngine = GetComponentInChildren<ShootingEngine>();
        shootingCooldown = gameObject.AddComponent<Timer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (healthEngine.alive)
            Alive();
        else
        {
            animator.SetTrigger("Dead");
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<EnemyEngine>());
        }    
    }
    //enables the actions of the enemy
    void Alive()
    {
        moveEngine.HorizontalMovement(0);

        //If the enemy is in distance of 4 on x axis or less from player he becomes alert
        distanceToPlayer = player.transform.position.x - transform.position.x;
        isAlert = Mathf.Abs(distanceToPlayer) < 7.5;

        if (isAlert)
            MoveToPlayerLocation();
        else
            Patrol();


    }
    //Patrols in a defined area
    void Patrol()
    {

            //Changes the direction to fit the patrol area
            if (Mathf.Abs(transform.position.x - startPostion.x) > patrolDistance)
                moveEngine.ChangeDir(1);
            else if (startPostion.x < transform.position.x)
                moveEngine.ChangeDir(-1);

            //The object moves in the direction he is facing
            if (moveEngine.FacingtDir() > 0)
                moveEngine.HorizontalMovement(1);
            else
                moveEngine.HorizontalMovement(-1);    
    }
    //Movers to player location
    void MoveToPlayerLocation()
    {
        if (Mathf.Abs(distanceToPlayer) < 0.1)
            horizontalInput = 0;
        else if (distanceToPlayer > 0)
            horizontalInput = 1;
        else 
            horizontalInput = -1;       
        
        hitPlayer = Physics2D.Raycast(transform.Find("FireLocation").transform.position, new Vector2(moveEngine.FacingtDir(), 0), 6);
        if (hitPlayer && hitPlayer.collider.gameObject.tag == "Player" && shootingCooldown.time == 0)        
            ShootPlayer();
        else
            moveEngine.HorizontalMovement(horizontalInput);

    }
    //Jump if the jump collider hits the ground to prevent the enemy from getting stuck
    void Jump()
    {
        moveEngine.Jump();
    }
    //Shoot the player if he is in range 
    void ShootPlayer()
    {
        //Shoots a projectile 
        bulletDir = moveEngine.FacingtDir();
        shootingEngine.ShootProjectile(new Vector2(15 * bulletDir, 0));
        shootingCooldown.SetTimer(2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Make a jump if stuck on ground
        if (collision.gameObject.tag == "Ground")
            Jump();
    }
}
