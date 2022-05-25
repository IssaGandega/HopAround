using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region externalObjects
    
    [Header("Object Setters")]
    public Rigidbody2D rb;
    public GameObject mesh;
    
    [SerializeField] private Transform groundedCheckerPos;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask tongueLayer;
    [SerializeField] private AnimationCurve accelCurve;
    
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
    [Range(0,1)]public float rotateMaxValue;
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
    [SerializeField] private AudioClip frogJump;
    [SerializeField] private AudioClip frogWalking;
    [SerializeField] private AudioClip frogStart;
    
    #endregion
    
    private Vector3 point;
    private Camera cam;
    private Tongue tongue;
    private RaycastHit2D hit;
    
    public float xAxisAccel;
    private float coyoteTimeCounter;
    private float bufferTimeCounter;

    private void OnEnable()
    {
        cam = Camera.main;
        cam.GetComponent<CameraController>().playerController = gameObject;
        tongue = gameObject.GetComponent<Tongue>();
    }

    private void Start()
    {
        isFacingRight = true;
        SoundManager.instance.PlaySound(frogStart);
    }

    private void Update()
    {
        CheckTouch();
        Jump();
        Move();

        if (!tongue.isGrabing)
        {
            JumpForces();
        }

        xAxisAccel = Mathf.Clamp(Input.acceleration.x, -rotateMaxValue, rotateMaxValue);
        
        bufferTimeCounter -= Time.deltaTime;

        if (!isJumping)
        {
            isGrounded = Physics2D.OverlapCircle(groundedCheckerPos.position, 0.3f, groundLayer);
        }
    }

    private void Move()
    {
        if (isGrounded)
        {
            //Reset CoyoteTime value
            coyoteTimeCounter = coyoteTime;
            
            //Not moving if not tilted enough / Touched
            if (xAxisAccel > -0.2f*rotateMaxValue && xAxisAccel < 0.2f * rotateMaxValue)
            {
                rb.velocity = Vector3.zero;
                currentSpeed = 0;
                PlayerAnimatorManager.instance.AnimatorStateChange(0,false);
            }
        
            //Moving animation
            else 
            {
                PlayerAnimatorManager.instance.AnimatorStateChange(1,false);
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
        
        if ((!tongue.isGrabing))
        {
            //Return speed after being to 0
            if (currentSpeed == 0)
            {
                currentSpeed = 0.1f;
            }
            
            //Reset speed and turn
            if ((isFacingRight && xAxisAccel < -0.1 * rotateMaxValue || !isFacingRight && xAxisAccel > 0.1 * rotateMaxValue))
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
            var screenTouch = Input.GetTouch(0).position;
            
            var worldTouch = cam.ScreenToWorldPoint(screenTouch);
            
            RaycastHit rayHit;
            if (Physics.Raycast(cam.ScreenPointToRay(screenTouch), out rayHit, Mathf.Infinity, ~10))
            {
                worldTouch = rayHit.point;
            }
            
            Collider2D[] touchedColliders = Physics2D.OverlapCircleAll(worldTouch,1f,tongueLayer);

            // TODO: instantiate a nice VFX !!
            //Instantiate(laboule, point, Quaternion.identity);

            foreach (Collider2D touchedColl in touchedColliders)
            {
                //Debug.Log(touchedColl);
                if (tongue.TongueStart(touchedColl.transform))
                {
                    
                    break;
                }
            }
            
            if (coyoteTimeCounter > 0f || isJumping)
            {
                //Buffer the jump
                bufferTimeCounter = bufferTime;
            }
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
            
            PlayerAnimatorManager.instance.AnimatorStateChange(2,false);
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
}