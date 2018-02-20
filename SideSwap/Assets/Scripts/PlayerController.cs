using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Controls player movement physics, appearance, Collider triggers and end-of-level UI

public class PlayerController : MonoBehaviour {

    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump;
    [HideInInspector] public bool is_player_screenwrapping;
    [HideInInspector] public bool can_player_move;

    public float moveSpeed = 20f;
    public float jumpForce = 1500f;
    public float maxGravity = 55f;
    public Text countText;
    public Text winText;

    private Rigidbody2D rb2d;
    private bool grounded = false;
    private int pickup_count;
    private GameManager gameManager;

    void Awake()
    {
        gameManager = new GameManager();
        rb2d = GetComponent<Rigidbody2D>();
        jump = false; //stops player jumping as they spawn
        is_player_screenwrapping = false; //player won't be spawned trying to screenwrap
        can_player_move = true; //used to freeze the player after completing a level
        pickup_count = 0;
        winText.text = "";
        setCountText();
    }

    void Update()
    {
        //x-axis screenwrap
        if (transform.position.x < -13.0 || transform.position.x > 13.0) //13.0x13.0 is the size of the game stage, could be altered for larger levels
        {
            float xPos = Mathf.Clamp(transform.position.x, 13, -13);
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
            is_player_screenwrapping = true;
        }

        //y-axis screenwrap
        if (transform.position.y < -13.0 || transform.position.y > 13.0)
        {
            float yPos = Mathf.Clamp(transform.position.y, 13, -13);
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
            is_player_screenwrapping = true;
        }

        //grounded check
        if (rb2d.velocity.y == 0) //no velocity on Y axis
            grounded = true;
        else grounded = false;

        //jump check
        if ((Input.GetButtonDown("Jump") && grounded)) //if jump is pressed AND player on the ground
            jump = true;

        //maximum gravity setting, Reference: http://answers.unity3d.com/questions/265810/limiting-rigidbody-speed.html
        //stops player accelerating constantly and breaking the physics
        if (rb2d.velocity.magnitude > maxGravity)
            rb2d.velocity = rb2d.velocity.normalized * maxGravity;
    }

    //Player movement physics
    void FixedUpdate () {
        if (can_player_move) //player hasnt reached the exit
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            rb2d.velocity = new Vector2(moveHorizontal * moveSpeed, rb2d.velocity.y); //moves the player only when a button is pressed, gets rid of the "ice effect"

            //make player face correct direction
            if (moveHorizontal > 0 && !facingRight)
                Flip();
            else if (moveHorizontal < 0 && facingRight)
                Flip();

            //actually jump
            if (jump)
            {
                rb2d.AddForce(new Vector2(0f, jumpForce));
                jump = false;
            }
        }
    }

    //Flips player sprite on X axis
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 flipScale = transform.localScale;
        flipScale.x *= -1; //flip the sprite
        transform.localScale = flipScale;
    }

    //Collider triggers for pickups and the exit
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickup")) //if item is a pickup
        { 
            other.gameObject.SetActive(false); //deactivate the pickup (pick it up)
            pickup_count = pickup_count + 1;
            setCountText(); //update count text
        }
        if (other.gameObject.CompareTag("Exit")) //player has reached the exit
        {
            rb2d.velocity = Vector3.zero; //stops all forces on player
            can_player_move = false;
            setCountText();
            Invoke("nextLevel", 2f); //pauses for 2secs before loading next level
        }
    }

    //Updates the count text for pickups
    void setCountText()
    {
        if (can_player_move == false) //end-of-level condition met
        {
            winText.text = "Level completed!";
            if (pickup_count >= 3){
                countText.text = "You collected all of the gems!";
            } else if (pickup_count <=0) {
                countText.text = "You know you're supposed to collect the gems, right?";
            } else {
                countText.text = "You collected " + pickup_count.ToString() + "/3 gems!";
            }
        }
    }

    void nextLevel()
    {
        gameManager.loadNextLevel(); //loading next level is handled by GameManager
    }
}
