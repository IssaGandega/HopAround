using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    #region externalObjects
    
    [Header("Object Setters")]
    public Rigidbody2D rb;
    public GameObject mesh;
    
    [SerializeField] private Transform groundedCheckerPos;
    [SerializeField] private LayerMask layer;
    [SerializeField] private AnimationCurve accelCurve;
    [SerializeField] private List<Collider2D> colliders;
    
    #endregion

    #region externalValues
    
    [Space]
    [Header("Value Setters")]
    
    [Range(1, 10)] 
    public int jumpForce;
    [Range(0, 0.5f)] 
    public float coyoteTime;
    [Range(0, 0.5f)] 
    public float bufferTime;

    public float fallMultiplier;
    public float lowJumpMultiplier;
    
    #endregion

    #region Visualizers

    [Space]
    [Header("Visualizers")]
    public bool isGrounded;
    public bool isTouched;
    public float currentSpeed;
    public bool isFacingRight;
    public bool isJumping;

    [Space(5)] 
    public float targetSpeed;
    public float speedDif;
    public float accelRate;
    public float velPower;
    public float movement;
    

    #endregion

    #region Move

    [Header("Run")]
    public float maxSpeed;
    public float speedAccel;
    public float speedDeccel;
    [Range(0, 1)] public float accelInAir;
    [Range(0, 1)] public float deccelInAir;
    [Space(5)]
    [Range(.5f, 2f)] public float accelPower;   
    [Range(.5f, 2f)] public float stopPower;
    [Range(.5f, 2f)] public float turnPower;

    #endregion

    #region Sounds

    [Space] [Header("Sounds")] 
    [SerializeField] private AudioClip frogStart;
    [SerializeField] private AudioClip frogDeath;
    [SerializeField] private AudioClip frogJump;
    [SerializeField] private AudioClip frogWalking;
    [SerializeField] private AudioClip frogContact;
    [SerializeField] private AudioClip frogTongue;
    [SerializeField] private AudioClip frogMecanism;
    
    #endregion
    
    private Vector3 point;
    private Camera cam;
    private Tongue tongue;
    private RaycastHit2D hit;
    
    private float xAxisAccel;
    //private bool isTakingAHit;
    private float coyoteTimeCounter;
    private float bufferTimeCounter;

    private void OnEnable()
    {
        cam = Camera.main;
        isFacingRight = true;
        cam.GetComponent<CameraController>().playerController = gameObject;
        tongue = gameObject.GetComponent<Tongue>();
        SoundManager.instance.PlaySound(frogStart);
    }

    private void FixedUpdate()
    {
        CheckTouch();
        Jump();
        Move();

        if (!tongue.isGrabing)
        {
            JumpForces();
        }

        xAxisAccel = Mathf.Clamp(Input.acceleration.x, -1f, 1f);
        bufferTimeCounter -= Time.deltaTime;

        if (!isJumping)
        {
            isGrounded = Physics2D.OverlapCircle(groundedCheckerPos.position, 0.3f, layer);
        }
    }

    private void Move()
    {
        if (isGrounded)
        {
            //Reset CoyoteTime value
            coyoteTimeCounter = coyoteTime;
            
            //Not moving if not tilted enough / Touched
            if (xAxisAccel > -0.2f && xAxisAccel < 0.2f || isTouched)
            {
                rb.velocity = Vector3.zero;
                currentSpeed = 0;
                PlayerAnimatorManager.instance.AnimatorStateChange(0);
            }
        
            //Moving animation
            else 
            {
                PlayerAnimatorManager.instance.AnimatorStateChange(1);
            }
        }

        else
        {
            //Decrease CoyoteTime
            coyoteTimeCounter -= Time.deltaTime;
        }

        //Stop moving if grabing
        if (tongue.isGrabing)
        {
            currentSpeed = 0;
        }
        
        if ((!tongue.isGrabing) && (!isTouched))
        {
            //Return speed after being to 0
            if (currentSpeed == 0)
            {
                currentSpeed = 0.1f;
            }
            
            //Reset speed and turn
            if ((isFacingRight && xAxisAccel < -0.1 || !isFacingRight && xAxisAccel > 0.1))
            {
                if (isGrounded)
                {
                    currentSpeed = Mathf.Lerp(currentSpeed, 0f, 0.3f);
                }
                
                Flip();
            }

            targetSpeed = xAxisAccel * maxSpeed;
            speedDif = targetSpeed - rb.velocity.x;

            #region Acceleration Rate
            
            if (coyoteTimeCounter > 0)
            {
                accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? speedAccel : speedDeccel;
            }
            else
            {
                accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? speedAccel * accelInAir : speedDeccel * deccelInAir;
            }

            if (((rb.velocity.x > targetSpeed && targetSpeed > 0.01f) || (rb.velocity.x < targetSpeed && targetSpeed < -0.01f)))
            {
                accelRate = 0;
            }
            
            #endregion
            
            #region Velocity Power
            
            //Stopping
            if (Mathf.Abs(targetSpeed) < 0.01f)
            {
                velPower = stopPower;
            }
            //Turning
            else if (Mathf.Abs(rb.velocity.x) > 0 && (Mathf.Sign(targetSpeed) != Mathf.Sign(rb.velocity.x)))
            {
                velPower = turnPower;
            }
            //Accelerating
            else
            {
                velPower = accelPower;
            }

            #endregion

            movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
            movement = Mathf.Lerp(rb.velocity.x, movement, 1);
            
            rb.AddForce((movement * Vector2.right));
        }
    }

    public void Flip()
    {
        Vector3 localRotate = transform.localEulerAngles;
        isFacingRight = !isFacingRight;
        localRotate.y += 180f;
        transform.localEulerAngles = localRotate;
    }


    private void CheckTouch()
    {
        if (Input.touchCount <= 0) return;
        //Reset Tongue
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            point = cam.ScreenPointToRay(Input.GetTouch(0).position).GetPoint(10);
            hit = Physics2D.Raycast(transform.position, point-transform.position,7,layer);
            //Debug.DrawRay(transform.position,(point-transform.position).normalized * 7,Color.magenta,3f);
            
            point.z = transform.position.z;
            if (hit != false && hit.collider != null)
            {
                if (hit.collider.gameObject.layer == 7)
                {
                    SoundManager.instance.PlaySound(frogTongue);
                    tongue.TongueStart(hit);
                }
                else if (coyoteTimeCounter > 0f || isJumping)
                {
                    //Buffer the jump
                    bufferTimeCounter = bufferTime;
                }
                else if (tongue.isGrabing)
                {
                    tongue.StartCoroutine(tongue.TongueReset());
                }
                else
                {
                    CheckPlayerTouch();
                }
            }
            else if (coyoteTimeCounter > 0f || isJumping)
            {
                //Buffer the jump
                bufferTimeCounter = bufferTime;
            }
            else
            {
                CheckPlayerTouch();
            }
        }

        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            isTouched = false;
            colliders.Clear();
        }
    }

    private void Jump()
    {
        if (coyoteTimeCounter > 0f && bufferTimeCounter > 0f && tongue.isGrabing == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            isJumping = true;
            isGrounded = false;
            
            SoundManager.instance.PlaySound(frogJump);
            rb.velocity = Vector2.up*jumpForce;

            bufferTimeCounter = 0;
            coyoteTimeCounter = 0;
            
            PlayerAnimatorManager.instance.AnimatorStateChange(2);
        }
    }
    
    private void JumpForces()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime; 
        }
        
        else if (rb.velocity.y > 0 && Input.touchCount <= 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime; 
        }
    }

    public void AddForceToPlayer(Vector2 dir, float strength)
    {
        //isTakingAHit = true;
        rb.AddForce(dir*strength);
        StartCoroutine(TakingHitCd());
    }

    private IEnumerator TakingHitCd()
    {
        yield return new WaitForSeconds(0.3f);
        //isTakingAHit = false;
    }

    private void CheckPlayerTouch()
    {
        colliders.Add(Physics2D.OverlapCircle(point,1f));
        if (colliders.Count != 0)
        {
            foreach (Collider2D col in colliders)
            {
                if (col != null)
                {
                    if (col.gameObject == gameObject)
                    {
                        isTouched = true;
                        break;
                    }
                }
            }
        }
    }
}
