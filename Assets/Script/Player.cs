using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] public bool isGrounded;
    private Camera cam;
    [SerializeField] private Transform groundedCheckerPos;
    private Vector3 point;
    [SerializeField] private bool isTouched;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float speed;
    [SerializeField] private AnimationCurve accelCurve;
    public float curentSpeed;
    [SerializeField] private Vector2 speedMinMax;
    public float jumpForce;
    public float jumpTimeCounter;
    private float originalJumpTimeCounter;
    [SerializeField] private List<Collider2D> collider;
    private Tongue tongue;
    [SerializeField] private float xAxisAccel;
    public bool wallLeftTouch;
    public bool wallRightTouch;
    private bool dirL;
    public bool isJumping;
    private RaycastHit2D hit;

    private void OnEnable()
    {
        cam = Camera.main;
        cam.GetComponent<CameraController>().playerController = gameObject;
        tongue = gameObject.GetComponent<Tongue>();
        originalJumpTimeCounter = jumpTimeCounter;
    }

    private void FixedUpdate()
    {
        CheckTouch();
        xAxisAccel = Mathf.Clamp(Input.acceleration.x, -1f, 1f);
        if ((xAxisAccel > -0.2f && xAxisAccel < 0.2f || isTouched) && isGrounded)
        {
            rb.velocity = Vector3.zero;
            curentSpeed = 0;
            PlayerAnimatorManager.instance.AnimatorStateChange(0);
        }
        else
            PlayerAnimatorManager.instance.AnimatorStateChange(1);

        if (!isGrounded)
        {
            PlayerAnimatorManager.instance.AnimatorStateChange(2);
        }

        if (tongue.isGrabing)
        {
            curentSpeed = 0;
        }

        if ((!tongue.isGrabing) && (!isTouched))
        {
            if (curentSpeed == 0)
            {
                curentSpeed = 0.1f;
            }

            if (((curentSpeed > 3f) && (xAxisAccel < 0) || (curentSpeed < -3f) && (xAxisAccel > 0)) && (isGrounded))
            {
                curentSpeed = Mathf.Lerp(curentSpeed, 0f, 0.3f);
            }
            
            if ((xAxisAccel < 0) && (!wallLeftTouch))
            {
                curentSpeed -= accelCurve.Evaluate(Mathf.Abs(xAxisAccel)) * speed;
            }

            else if ((xAxisAccel > 0) && (!wallRightTouch))
            {
                curentSpeed += accelCurve.Evaluate(xAxisAccel) * speed;
            }

            curentSpeed = Mathf.Clamp(curentSpeed,speedMinMax.x,speedMinMax.y);
            rb.velocity = new Vector3(curentSpeed, rb.velocity.y, 0);
        }

        if (!isJumping)
        {
            isGrounded = Physics2D.OverlapCircle(groundedCheckerPos.position, 0.3f, layer);
            if (isGrounded)
            {
                jumpTimeCounter = originalJumpTimeCounter;
            }
        }
    }
    

    private void CheckTouch()
    {
        if (Input.touchCount <= 0) return;
        
        if (tongue.isGrabing && tongue.frogReachedPoint)
        {
            tongue.StartCoroutine(tongue.TongueReset());
        }
        
        if (isTouched == false)
        {
            point = cam.ScreenPointToRay(Input.GetTouch(0).position).GetPoint(10);
            hit = Physics2D.Raycast(transform.position, point-transform.position,10,layer);
            //Debug.DrawRay(transform.position,point-transform.position,Color.magenta,3f);
            
            point.z = transform.position.z;
            if (hit != null && hit.collider != null)
            {
                if (hit.collider.gameObject.layer == 7)
                {
                    tongue.TongueStart(hit);
                }
                else if (isGrounded || isJumping)
                {
                    Jump();
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
            else if (isGrounded || isJumping)
            {
                Jump();
            }
            else
            {
                CheckPlayerTouch();
            }
        }
        
        
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            isTouched = false;
            collider.Clear();
        }
    }

    private void Jump()
    {
        if ((isGrounded) && (tongue.isGrabing == false))
        {
            isJumping = true;
            isGrounded = false;
            
            if (xAxisAccel > 0)
            {
                dirL = false;
            }
            else
            {
                dirL = true;
            }
            rb.AddForce(Vector3.up*jumpForce);
            PlayerAnimatorManager.instance.AnimatorStateChange(2);
        }
        
        else if ((Input.GetTouch(0).phase == TouchPhase.Stationary
             ||Input.GetTouch(0).phase == TouchPhase.Moved) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.AddForce(Vector3.up*jumpForce/5);
                jumpTimeCounter -= Time.deltaTime;
            }
        }
    }

    private void CheckPlayerTouch()
    {
        collider.Add(Physics2D.OverlapCircle(point,1f));
        if (collider.Count != 0)
        {
            foreach (Collider2D col in collider)
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
