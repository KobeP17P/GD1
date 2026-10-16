using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    Rigidbody2D rb;
    
    float movementX;
    float movementY;
    public int diamonds = 0;
    bool isTouchingGround;
    [SerializeField] float speed = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BallController player = collision.GetComponent<BallController>();
            player.diamonds++;
            Debug.Log("Coin Collected!");
            Destroy(gameObject);
        }
    }
}

