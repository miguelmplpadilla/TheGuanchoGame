using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float movementSpeed;
    [SerializeField] private float speedMin;
    [SerializeField] private float speedMax;
    [SerializeField] private float speedAir;

    [SerializeField] private float distanciaComprobarSuelo = 0.2f;
    [SerializeField] private float maxDistanciaComprobarSuelo = 0.2f;
    [SerializeField] private float minDistanciaComprobarSuelo = 0.2f;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float jumpForce;
    [SerializeField] private float slopeCheckDistance;
    [SerializeField] private float maxSlopeAngle;
    
    [SerializeField] private float maxVerticalSpeed = 100;
    [SerializeField] private float minVerticalSpeed = -10;
    
    private float xInput;
    private float slopeDownAngle;
    private float slopeSideAngle;
    private float lastSlopeAngle;
    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private PhysicsMaterial2D noFriction;
    [SerializeField] private PhysicsMaterial2D fullFriction;

    private int facingDirection = 1;

    public bool isGrounded;
    private bool inicioIsGrounded = true;
    
    [SerializeField] private bool isOnSlope;
    private bool isJumping;
    private bool canWalkOnSlope;
    private bool canJump;
    public bool mov = true;

    private Vector2 newVelocity;
    private Vector2 newForce;
    private Vector2 capsuleColliderSize;
    private Vector2 slopeNormalPerp;
    
    private Vector2 posicionParticularCorrerInstanciada;

    private Rigidbody2D rb;
    private CapsuleCollider2D cc;
    public Animator animator;
    private PlayerGanchoController playerGanchoController;
    private PausaController pausaController;
    private PlayerHurtController playerHurtController;

    public GameObject posicionLanzarRayCast;
    [SerializeField] private GameObject puntoCreacionparticulasCorrer;
    
    [SerializeField] private GameObject particulasAterrizar;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
        playerGanchoController = GetComponent<PlayerGanchoController>();
        playerHurtController = GetComponentInChildren<PlayerHurtController>();
    }

    private void Start()
    {
        capsuleColliderSize = cc.size;
        pausaController = GameObject.Find("PausaManager").GetComponent<PausaController>();
    }

    private void Update()
    {
        if (!pausaController.pausado && !playerHurtController.muerto)
        {
            animator.SetBool("isGrounded", isGrounded);
            CheckInput();  
        }
    }

    private void FixedUpdate()
    {
        if (!pausaController.pausado && !playerHurtController.muerto)
        {
            animator.SetFloat("horizontalVelocity", movementSpeed);
            
            SlopeCheck();
            CheckGround();
            ApplyMovement();
        }
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

        if (isGrounded && inicioIsGrounded && rb.velocity.y < -10)
        {
            crearParticulaAterrizar();
            inicioIsGrounded = false;
        }

        if (isOnSlope)
        {
            distanciaComprobarSuelo = maxDistanciaComprobarSuelo;
        }
        else
        {
            distanciaComprobarSuelo = minDistanciaComprobarSuelo;
        }

        if (!isGrounded)
        {
            RaycastHit2D hitGround = Physics2D.Raycast(groundCheck.position, Vector2.down, Mathf.Infinity, whatIsGround);
            
            if (hitGround.collider != null)
            {
                float distancia = Vector2.Distance(hitGround.point, groundCheck.position);

                if (distancia < distanciaComprobarSuelo)
                {
                    isGrounded = true;
                }
                
                Debug.DrawRay(groundCheck.position, Vector2.down, Color.green);
            }
            else
            {
                Debug.DrawRay(groundCheck.position, Vector2.down, Color.red);
            }

            if (!isGrounded)
            {
                inicioIsGrounded = true;
            }
        }

        if(rb.velocity.y <= 0.0f)
        {
            isJumping = false;
        }

        if(isGrounded && !isJumping && slopeDownAngle <= maxSlopeAngle)
        {
            canJump = true;
        }

    }

    public void crearParticulaAterrizar()
    {
        Instantiate(particulasAterrizar, puntoCreacionparticulasCorrer.transform.position, Quaternion.identity);
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
            if (hit.collider.CompareTag("Rampa"))
            {
                isOnSlope = true;
            }
            else
            {
                isOnSlope = false;
            }
        }
        else
        {
            isOnSlope = false;
        }

        if (xInput == 0.0f)
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
                if (isGrounded && !isJumping) //if not on slope
                {
                    newVelocity.Set(movementSpeed * xInput, 0.0f);
                    rb.velocity = newVelocity;
                }
                else if (isGrounded && canWalkOnSlope && !isJumping) //If on slope
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
                    if (Input.GetAxisRaw("Sprint") > 0)
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
            transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        }
        else if (xInput < 0f)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}

