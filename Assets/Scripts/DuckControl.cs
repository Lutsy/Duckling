using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// My character movement script...
public class DuckControl : MonoBehaviour
{
    Animator animator;

    public float jumpForce = 7f;
    public float flightPower = 5f;
    private float playerSpeed = 3.0f;

    private float horizontal;
    private float vertical;

    private Rigidbody2D rb;
    private GameObject duckKnifeSprite;

    public LayerMask groundLayer;
    public Transform groundCheck;

    private bool isGrounded;

    public float wasUnderwaterTimer = 2f;
    public float flightDelay = 0.3f;
    private float lastKeyPressTime;



    private bool isUnderwater;
    public bool IsUnderwater
    {
        get { return isUnderwater; }
    }

    private bool hasKnife = false;// field
    public bool HasKnife   // property
    {
        get { return hasKnife; }   // get method
    }

    private void Start()
    {
        Transform characterModel = transform.Find("MotherDuck");
        animator = characterModel.GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();

        duckKnifeSprite = GameObject.Find("PlayerKnife");
        if (duckKnifeSprite != null)
            duckKnifeSprite.SetActive(false);
    }

    void Update()
    {
        if (!isUnderwater)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        }

        bool isWalking = Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0;

        //if (isWalking)
        //{
        //    animator.CrossFade("Walk", 0.1f);
        //}
        //else
        //{
        //    animator.CrossFade("Idle", 0.1f);
        //}

        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isFlying", !isGrounded);

        SetupFlight();

        SetupFlip();
    }

    private void FixedUpdate()
    {
        Vector2 direction = Vector3.right * horizontal;
        Vector2 swimming = Vector3.up * vertical;

        if (isUnderwater)
        {
            if (swimming.y < 0)
            {
                rb.gravityScale = 0;
            }
            else
            {
                rb.gravityScale = -0.2f;
            }
            direction += swimming;
        }

        rb.position += direction * Time.deltaTime * playerSpeed;
        //transform.position += direction * Time.deltaTime * playerSpeed;
    }

    private void SetupFlip()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (horizontal > 0)
        {
            //going right
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (horizontal < 0)
        {
            //going left
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void SetupFlight()
    {
        if (!isUnderwater &&
                Time.time - lastKeyPressTime > flightDelay &&
                (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
        {
            if (isGrounded)
            {
                Jump(jumpForce);
                Debug.Log("just jump");

                if (animator != null)
                {
                    animator.CrossFade("Jump", 0.1f);
                }
            }
            else
            {
                Jump(flightPower);
                Debug.Log("activate wings");

                if (animator != null)
                {
                    animator.SetTrigger("Fly");
                }
                //change sprite to flight mode here
            }

            lastKeyPressTime = Time.time;
        }
    }

    private void Jump(float powerLevel)
    {
        // Apply upward force
        rb.velocity = new Vector2(rb.velocity.x, 0); // Reset vertical velocity to prevent additional force stacking
        rb.AddForce(Vector2.up * powerLevel, ForceMode2D.Impulse);

    }

    //displays knife duck is holding
    public void ShowKnife()
    {
        if (duckKnifeSprite != null)
            duckKnifeSprite.SetActive(true);
        hasKnife = true;
    }

    //Swimming trigger 
    public void ToggleSwim(bool isSwimming)
    {
        isUnderwater = isSwimming;

        if (rb != null && isSwimming)
        {

            rb.gravityScale = -0.2f;
            rb.drag = 1f;
        }
        else
        {
            rb.gravityScale = 1f;
            rb.drag = 0;
        }
    }

}