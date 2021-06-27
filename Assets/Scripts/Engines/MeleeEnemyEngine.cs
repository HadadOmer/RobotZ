using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyEngine : MonoBehaviour
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
            patrolDistance = 4f;

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
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<EnemyEngine>());
        }

    }
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
        MeleeAttack();

    }
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
    void MoveToPlayerLocation()
    {
        if (Mathf.Abs(distanceToPlayer) < 0.1)
            horizontalInput = 0;
        else if (distanceToPlayer > 0)
            horizontalInput = 1;
        else
            horizontalInput = -1;
        moveEngine.HorizontalMovement(horizontalInput);

    }
    void MeleeAttack()
    {
        if (Mathf.Abs(distanceToPlayer) < 1)        
            animator.SetTrigger("Attack");
        
    }
    void Jump()
    {
        moveEngine.Jump();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Make a jump if stuck on ground
        if (collision.gameObject.tag == "Ground")
            Jump();
    }
}
