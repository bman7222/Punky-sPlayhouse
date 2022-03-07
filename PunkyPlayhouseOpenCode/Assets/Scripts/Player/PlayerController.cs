using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed, jumpForce;
        
    public float fallMultiplier=2.5f;

    public float lowJumpMultiplier = 2f;

    public Rigidbody theRB;

    private Vector2 moveInput;

    //player clone task
    public static PlayerController Instance;

    public LayerMask whatIsGround;
    public Transform groundPoint;
    private bool isGrounded,movingBackwards;

    public Animator anim,flipAnim;

    public SpriteRenderer theSR;

    public bool canMove;

    public float hangTime=0.2f;
    private float hangCount;

    public float jumpBufferLength = 0.1f;
    private float jumpBufferCount;


    //area load
    public string areaTransitionName;



    // Start is called before the first frame update
    void Start()
    {

        /*
    if (!Instance)
      {
      Instance = this;
      }

    DontDestroyOnLoad(gameObject);

    canMove = true;*/

        
        if (!Instance)
        {
            Instance = this;

        }

        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (canMove)
        {
        
            //theRB.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.velocity.y, Input.GetAxisRaw("Vertical") * moveSpeed);

            moveInput.x= Input.GetAxisRaw("Horizontal");
            moveInput.y= Input.GetAxisRaw("Vertical");

            moveInput.Normalize();

            theRB.velocity = new Vector3(moveInput.x * moveSpeed, theRB.velocity.y, moveInput.y * moveSpeed);

            /*float horizontalInput = Input.GetAxisRaw("Horizontal");

            float verticalInput = Input.GetAxisRaw("Vertical");

            Vector3 forwardMovement = transform.forward * verticalInput;

            Vector3 rightMovement = transform.right * horizontalInput;

            theRB.velocity=Vector3.ClampMagnitude(forwardMovement + rightMovement, 1.0f) * moveSpeed;
            */

            anim.SetFloat("moveSpeed", theRB.velocity.magnitude);

            //see grounded
            RaycastHit hit;

           if (Physics.Raycast(groundPoint.position, Vector3.down, out hit, .3f, whatIsGround))
            {
                isGrounded = true;

            }

            else
            {
                isGrounded = false;
            }

           //JUMP HANG TIME
            if (isGrounded)
            {
                hangCount = hangTime;
            }

            else
            {
                hangCount -= Time.deltaTime;
            }

            //JUMP BUFFER
            if (Input.GetButtonDown("Jump"))
            {
                jumpBufferCount = jumpBufferLength;
            }

            else
            {
                jumpBufferCount -= Time.deltaTime;
            }

            //JUMP CODE
            //hold
            if (jumpBufferCount>=0 && hangCount > 0f)
            {
                theRB.velocity = new Vector3(theRB.velocity.x, jumpForce, theRB.velocity.z);
                jumpBufferCount = 0;
            }

            //hold and let go
            if (Input.GetButtonUp("Jump") && theRB.velocity.y>0)
            {
                theRB.velocity = new Vector3(theRB.velocity.x, theRB.velocity.y*0.5f, theRB.velocity.z);
            }

            //FASTER FALL
            if (theRB.velocity.y < -1)
            {
                theRB.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

            }


            //TEST IN PHYSICS
            /*
             * 
             * //FASTER FALL
            if (theRB.velocity.y < -1)
            {
                theRB.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

           }

            else if (hangCount > 0 && !Input.GetButton("Jump"))
            {
                theRB.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }

            if (Input.GetButtonDown("Jump") && hangCount > 0)
            {
                theRB.velocity += new Vector3(0f, jumpForce, 0f);
            }*/

            anim.SetBool("onGround", isGrounded);


            if (!theSR.flipX && Input.GetAxisRaw("Horizontal") < 0 && isGrounded)
            {
                theSR.flipX = true;

                flipAnim.SetTrigger("Flip");
            }

            else if (theSR.flipX && Input.GetAxisRaw("Horizontal") > 0 && isGrounded)
            {
                theSR.flipX = false;
                flipAnim.SetTrigger("Flip");
            }

            if (!movingBackwards && Input.GetAxisRaw("Vertical") > 0)
            {
                movingBackwards = true;

            }

            else if (movingBackwards && Input.GetAxisRaw("Vertical") < 0)
            {
                movingBackwards = false;

            }

            anim.SetBool("movingBackwards", movingBackwards);
        }

        //if cannot move
        else
        {

       
            theRB.velocity = new Vector3(0, 0, theRB.velocity.z);
            anim.SetFloat("moveSpeed", theRB.velocity.magnitude);
        }
    }

    
}
