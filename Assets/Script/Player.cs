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
    [SerializeField] private List<Collider2D> collider;
    private Tongue tongue;
    [SerializeField] private float xAxisAccel;
    [HideInInspector] public Vector3 lastGroundedPos;
    public bool wallLeftTouch;
    public bool wallRightTouch;
    private bool dirL;
    private RaycastHit2D hit;

    private void OnEnable()
    {
        cam = Camera.main;
        tongue = gameObject.GetComponent<Tongue>();
    }

    private void FixedUpdate()
    {
        CheckTouch();
        xAxisAccel = Mathf.Clamp(Input.acceleration.x, -1f, 1f);
        if ((xAxisAccel < -0.3f && xAxisAccel > 0.3f) || (isTouched))
        {
            rb.velocity = Vector3.zero;
            
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
                //use lerp to switch direction
                curentSpeed = Mathf.Lerp(curentSpeed, 0f, 0.3f);
            }

        

            if ((xAxisAccel < 0) && (!wallLeftTouch) && (isGrounded))
            {
                curentSpeed -= accelCurve.Evaluate(Mathf.Abs(xAxisAccel)) * speed;
            }
            else if ((xAxisAccel < 0) && (!wallLeftTouch) && (dirL) && (!isGrounded))
            {
                curentSpeed -= (accelCurve.Evaluate(Mathf.Abs(xAxisAccel)) * speed)/1.5f;
            }
            
            if ((xAxisAccel > 0) && (!wallRightTouch) && (isGrounded))
            {
                curentSpeed += accelCurve.Evaluate(xAxisAccel) * speed;
            }
            else if ((xAxisAccel > 0) && (!wallRightTouch) && (!isGrounded) && (!dirL))
            {
                curentSpeed += (accelCurve.Evaluate(xAxisAccel) * speed)/1.5f;
            }

            curentSpeed = Mathf.Clamp(curentSpeed,speedMinMax.x,speedMinMax.y);
            //rb.velocity = new Vector3(xAxisAccel * speed, rb.velocity.y, 0);
            rb.velocity = new Vector3(curentSpeed, rb.velocity.y, 0);
        }

        isGrounded = Physics2D.OverlapCircle(groundedCheckerPos.position, 0.3f, layer);
    }
    

    private void CheckTouch()
    {
        if (Input.touchCount <= 0) return;
        if ((Input.GetTouch(0).phase == TouchPhase.Began))
        {
            point = cam.ScreenPointToRay(Input.GetTouch(0).position).GetPoint(10);
            hit = Physics2D.Raycast(transform.position, point-transform.position,10,layer);
            Debug.DrawRay(transform.position,point-transform.position,Color.magenta,3f);

            point.z = transform.position.z;
            if (hit != null)
            {
                if (hit.collider.gameObject.layer == 7)
                {
                    tongue.TongueStart(hit.collider);
                }
                else
                {
                    if (tongue.isGrabing)
                    {
                        tongue.StartCoroutine(tongue.TongueReset());
                    }
                    else
                    {
                        CheckPlayerTouch();
                    }
     
                }
            }
            else
            {
                CheckPlayerTouch();
            }
        }
        if ((isTouched == false) && (isGrounded))
        {
            Jump();
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
            if (xAxisAccel > 0)
            {
                dirL = false;
            }
            else
            {
                dirL = true;
            }
            rb.AddForce(Vector3.up*jumpForce);
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
