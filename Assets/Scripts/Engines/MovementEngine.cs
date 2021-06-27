using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementEngine : MonoBehaviour
{
    public float horizontalSpeed;//The horizontal move speed of this object
    public float jumpForce;//The jump force of this game object

    Rigidbody2D rigidBody;//This game object's rigid body 2d componenet
    Animator animator;//This game object's animator componenet
    public bool isGrounded;//Is this game object grounded
    Timer jumpTimer;//A timer between jumps

    // Start is called before the first frame update
    void Awake()
    {
        jumpTimer = gameObject.AddComponent<Timer>();
        ResetValues();      
    }
    void ResetValues()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isGrounded = true;
        jumpTimer.SetTimer(0);

        //Defines the public values if there are not defined
        if (horizontalSpeed == 0)
            horizontalSpeed = 10f;
        if (jumpForce == 0)
            jumpForce = 100f;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void HorizontalMovement(float input)
    {
        //Changes the direction of the object based on its moving direction
        ChangeDir(input);

        //Set the isMoving parameter to true or false based on the horizontal movement
       if (input != 0)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);

        //Moves the object horizontaly
        transform.position += new Vector3(input * horizontalSpeed * Time.deltaTime, 0);
    }
    public void Jump()
    {
        //if the object is grounded add force to make it jump and apply the jump animation
        if(isGrounded&&jumpTimer.time==0)
        {
            rigidBody.AddForce(new Vector2(0, jumpForce));
            //Set a timer to prevent player from jumping on walls
            jumpTimer.SetTimer(0.5f);
        }
    }   
    public void ChangeDir(float input)
    {
        //Defines the scale of the object based on its movement direction *input supposed to be 1 or -1
        if(input==1||input==-1)
            transform.localScale = new Vector3(input*Mathf.Abs(transform.localScale.x), transform.localScale.y);
       
    }
    //Returns 1 or -1 based on the direction the object facing
    public float FacingtDir()
    {
        return transform.localScale.x / Mathf.Abs(transform.localScale.x);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            animator.SetBool("isGrounded",true);
        }          
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);
        }
           
    }
}
