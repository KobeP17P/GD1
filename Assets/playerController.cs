using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer sprite;
    float movementX;
    float movementY;
    public int diamonds = 0;
    bool isTouchingGround;
    [SerializeField] float speed = 5.0f;
    bool isMovingRight;
     
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       rb = GetComponent<Rigidbody2D>(); 
       animator = GetComponent<Animator>();
       sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isJumping", !isTouchingGround);
        animator.SetBool("isRunning", MathF.Abs(rb.linearVelocityX) >0.01f);
        if(isMovingRight && movementX < 0)
        {
            isMovingRight = false;
            sprite.flipX = true;
        } else if(!isMovingRight && movementX > 0)
        {
            isMovingRight = true;
            sprite.flipX = false;
        }
    }


    void OnCollisionEnter2D(Collision2D collision){
        if (collision.collider.CompareTag("Ground")){
            isTouchingGround = true;
        }
    }


    void OnJump(){
        if (isTouchingGround){
            rb.AddForceY (5, ForceMode2D.Impulse);
        }
    }

    void OnMove(InputValue value){ //get the input of the player
        Vector2 v = value.Get<Vector2>(); //assign it to the vector 2

        movementX = v.x; //x value of the vector
        movementY = v.y; // y value of the vector
    }

    void FixedUpdate(){
        float xMoveDistance = movementX * speed * Time.fixedDeltaTime;
        float yMoveDistance = movementY * speed * Time.fixedDeltaTime;
        transform.position = new Vector2(transform.position.x + xMoveDistance, transform.position.y +yMoveDistance);
    }
    private void OnCollisionExit2D(Collision2D col){
        if (col.collider.gameObject.CompareTag("Ground")){
            isTouchingGround = false;
        }
        
    }


}

