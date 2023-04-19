using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float movementSpeed;
    [SerializeField] private float speedMin;
    [SerializeField] private float speedMax;
    [SerializeField] private float speedAir;

    [SerializeField]
    private float groundCheckRadius;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float slopeCheckDistance;
    [SerializeField]
    private float maxSlopeAngle;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private PhysicsMaterial2D noFriction;
    [SerializeField]
    private PhysicsMaterial2D fullFriction;

    private float xInput;
    private float slopeDownAngle;
    private float slopeSideAngle;
    private float lastSlopeAngle;
    
    [SerializeField] private float maxVerticalSpeed = 100;
    [SerializeField] private float minVerticalSpeed = -10;

    private int facingDirection = 1;

    public bool isGrounded;
    private bool isOnSlope;
    private bool isJumping;
    private bool canWalkOnSlope;
    private bool canJump;
    public bool mov = true;

    private Vector2 newVelocity;
    private Vector2 newForce;
    private Vector2 capsuleColliderSize;

    private Vector2 slopeNormalPerp;

    private Rigidbody2D rb;
    private CapsuleCollider2D cc;
    public Animator animator;
    private PlayerGanchoController playerGanchoController;

    public GameObject posicionLanzarRayCast;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
        playerGanchoController = GetComponent<PlayerGanchoController>();
    }

    private void Start()
    {
        capsuleColliderSize = cc.size;
    }

    private void Update()
    {
        animator.SetBool("isGrounded", isGrounded);
        CheckInput();     
    }

    private void FixedUpdate()
    {
        animator.SetFloat("horizontalVelocity", movementSpeed);
        
        CheckGround();
        SlopeCheck();
        ApplyMovement();
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        Flip();

        if (Input.GetButtonDown("Jump"))
        {
            if (canJump)
            {
                jump();
            }
        }

    }
    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if(rb.velocity.y <= 0.0f)
        {
            isJumping = false;
        }

        if(isGrounded && !isJumping && slopeDownAngle <= maxSlopeAngle)
        {
            canJump = true;
        }

    }

    private void SlopeCheck()
    {
        Vector2 checkPos = posicionLanzarRayCast.transform.position - (Vector3)(new Vector2(0.0f, capsuleColliderSize.y / 2));

        //SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance, whatIsGround);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance, whatIsGround);

        if (slopeHitFront)
        {
            isOnSlope = true;

            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);

        }
        else if (slopeHitBack)
        {
            isOnSlope = true;

            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }
    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {      
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, whatIsGround);
        
        if (hit)
        {
            
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;            

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if(slopeDownAngle != lastSlopeAngle)
            {
                isOnSlope = true;
            }                       

            lastSlopeAngle = slopeDownAngle;
           
            Debug.DrawRay(hit.point, slopeNormalPerp, Color.blue);
            Debug.DrawRay(hit.point, hit.normal, Color.green);

        }

        if (slopeDownAngle > maxSlopeAngle || slopeSideAngle > maxSlopeAngle)
        {
            canWalkOnSlope = false;
        }
        else
        {
            canWalkOnSlope = true;
        }

        canWalkOnSlope = true;

        if (isOnSlope && canWalkOnSlope && xInput == 0.0f)
        {
            rb.sharedMaterial = fullFriction;
        }
        else
        {
            rb.sharedMaterial = noFriction;
        }
    }

    public void jump()
    {
        animator.SetTrigger("jump");
        canJump = false;
        isJumping = true;
        newVelocity.Set(0.0f, 0.0f);
        rb.velocity = newVelocity;
        newForce.Set(0.0f, jumpForce);
        rb.AddForce(newForce, ForceMode2D.Impulse);
    }

    private void ApplyMovement()
    {
        if (mov)
        {
            float y = Mathf.Clamp(rb.velocity.y, minVerticalSpeed, maxVerticalSpeed);
            
            if (!playerGanchoController.ganchoEnganchado)
            {
                if (isGrounded && !isOnSlope && !isJumping) //if not on slope
                {
                    newVelocity.Set(movementSpeed * xInput, 0.0f);
                    rb.velocity = newVelocity;
                }
                else if (isGrounded && isOnSlope && canWalkOnSlope && !isJumping) //If on slope
                {
                    newVelocity.Set(movementSpeed * slopeNormalPerp.x * -xInput, movementSpeed * slopeNormalPerp.y * -xInput);
                    rb.velocity = newVelocity;
                }
                else if (!isGrounded) //If in air
                {
                    newVelocity.Set(movementSpeed * xInput, y);
                    rb.velocity = newVelocity;
                }
            }
            else
            {
                newVelocity.Set(movementSpeed * xInput, 0.0f);
                transform.Translate(newVelocity.normalized * Time.deltaTime);
            }

            if (xInput != 0)
            {
                if (isGrounded)
                {
                    if (Input.GetButton("Sprint"))
                    {
                        movementSpeed = speedMax;
                    }
                    else
                    {
                        movementSpeed = speedMin;
                    }
                }
                else
                {
                    movementSpeed = speedAir;
                }

                animator.SetBool("run", true);
            }
            else
            {
                movementSpeed = 0;

                animator.SetBool("run", false);
            } 
        }
    }

    private void Flip()
    {
        if (xInput > 0f)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else if (xInput < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

}

