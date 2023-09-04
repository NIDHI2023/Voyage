using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;
    public Transform groundCheckSpot;
    public float groundCheckRadius;
    public LayerMask whatLayerIsGrounded;
    public bool isGrounded;
    public bool isClimbing;
    public bool onLadder;

    public float knockbackForce;
    public float knockbackFrames;
    public float invincibleFrames;
    public GameObject bounceBox;
    private float knockbackCounter;
    private float invincibleCounter;
    public Vector3 respawnPos;
    public Rigidbody2D myRB;
    public bool canMove;
    private LevelManager levelManager;
    private float initialGravity;
    private Animator myAnimator;

    private float originalSpeed;
    public bool canDoubleJump;


    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        respawnPos = transform.position;
        initialGravity = myRB.gravityScale;
        levelManager = FindObjectOfType<LevelManager>();
        //canMove = true;
        originalSpeed = moveSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager.leadingPlayer.name.Equals(gameObject.name))
        {
            canMove = true;
        } else
        {
            canMove = false;
        }
        isGrounded = Physics2D.OverlapCircle(groundCheckSpot.position, groundCheckRadius, whatLayerIsGrounded);
        if (knockbackCounter <= 0f && canMove)
        {
            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                myRB.velocity = new Vector3(moveSpeed, myRB.velocity.y, 0f);
                transform.localScale = new Vector3(1f, 1f, 1f);
                Sprint();


            } 
            else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                myRB.velocity = new Vector3(-moveSpeed, myRB.velocity.y, 0f);
                transform.localScale = new Vector3(-1f, 1f, 1f);
                Sprint();
            }
            else
            {
                //this eliminates fox slide. but w/o this it's good for ice levels
                myRB.velocity = new Vector3(0f, myRB.velocity.y, 0f);
            }
            //while there are Axes for Jump we want to use GetButtonDown for jump to be instant
            //GetButtonDown will jump right as button is pressed and then stop even if held down
            //GetButtonUp would only jump when we let go of space, which is a good idea for charging up a super jump
            //use Axes nmae for "jump" instead of KeyCode
            if (Input.GetButtonDown("Jump") && !onLadder)
            {
                if (canDoubleJump)
                {
                    myRB.velocity = new Vector3(myRB.velocity.x, jumpHeight, 0f);
                    if (gameObject.tag == "Player" && !isGrounded)
                    {
                        canDoubleJump = false;
                    }
                }
                else
                {
                    if (isGrounded)
                    {
                        myRB.velocity = new Vector3(myRB.velocity.x, jumpHeight, 0f);
                        if (gameObject.tag == "Player")
                        {
                            canDoubleJump = true;
                        }
                    }
                }
            }

            if (onLadder)
            {
                isClimbing = true;
                if (Input.GetAxisRaw("Vertical") < 0f)
                {
                    myRB.velocity = new Vector3(myRB.velocity.x, -moveSpeed, 0f);
                }
                else if (Input.GetAxisRaw("Vertical") > 0f)
                {
                    myRB.velocity = new Vector3(myRB.velocity.x, moveSpeed, 0f);
                }
                else
                {
                    myRB.velocity = new Vector3(0f, 0f, 0f);
                }
            }

            if (myRB.velocity.y == 0 && isGrounded)
            {
                isClimbing = false;
                onLadder = false;
            }

            if (!onLadder)
            {
                isClimbing = false;
                if (Input.GetAxisRaw("Vertical") < 0f)
                {
                    //isCrouched = true;
                }
                else if (Input.GetAxisRaw("Vertical") > 0f)
                {
                    //isCrouched = false;
                }
            }

        }

        if (knockbackCounter > 0f)
        {
            knockbackCounter -= Time.deltaTime;
            if (transform.localScale.x > 0f)
            {
                myRB.velocity = new Vector3(-knockbackForce, knockbackForce, 0f);
            }
            else
            {
                myRB.velocity = new Vector3(-knockbackForce, knockbackForce, 0f);
            }
        }
        //if (levelManager.invincibilityFrames)
        //{
        //    if (invincibleCounter <= 0f)
        //    {
        //        levelManager.invincibilityFrames = false;
        //    }
        //    else if (invincibleCounter > 0f)
        //    {
        //        invincibleCounter -= Time.deltaTime;
        //    }

        //}

        myAnimator.SetFloat("speed", Mathf.Abs(myRB.velocity.x));
        myAnimator.SetFloat("height", myRB.velocity.y);
        myAnimator.SetBool("grounded", isGrounded);
        //myAnimator.SetBool("crouched", isCrouched);
        //myAnimator.SetBool("climbing", isClimbing);

        if (myRB.velocity.y < 0f)
        {
            //activate only when falling down
            bounceBox.SetActive(true);
        }
        else
        {
            bounceBox.SetActive(false);
        }

    }//end of update

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "KillPlane")
        {
            transform.position = respawnPos;
            levelManager.Respawn();
        }

        if (other.tag == "Checkpoint")
        {
            respawnPos = other.transform.position;
        }

        if (other.tag == "Finish")
        {
            //Destroy(GameObject.FindWithTag("BossBridge"));
        }
    }

    public void Knockback()
    {
        knockbackCounter = knockbackFrames;
        invincibleCounter = invincibleFrames;
        //levelManager.invincibilityFrames = true;
    }

    void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = originalSpeed * 2f;
        } else
        {
            moveSpeed = originalSpeed;
        }



    }
}
