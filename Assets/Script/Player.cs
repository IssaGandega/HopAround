using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool isGrounded;
    private Camera cam;
    private Vector3 point;
    [SerializeField] private bool isTouched;
    [SerializeField] private float groundCheckLength;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private List<Collider2D> collider;
    private Tongue tongue;
    private float xAxisAccel;

    private void OnEnable()
    {
        cam = Camera.main;
        tongue = gameObject.GetComponent<Tongue>();
    }

    private void FixedUpdate()
    {
        xAxisAccel = Mathf.Clamp(Input.acceleration.x, -0.5f, 0.5f);
        if ((xAxisAccel < -0.2f && xAxisAccel > 0.2f) || (isTouched))
        {
            rb.velocity = Vector3.zero;
        }

        if ((!tongue.isGrabing) && (!isTouched))
        {
            rb.velocity = new Vector3(xAxisAccel * speed, rb.velocity.y, 0);
        }

        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckLength,layer);
    }

    private void Update()
    {
        CheckTouch();
    }


    private void CheckTouch()
    {
        if (Input.touchCount <= 0) return;
        if ((Input.GetTouch(0).phase == TouchPhase.Began) && (isGrounded))
        {
            point = cam.ScreenPointToRay(Input.GetTouch(0).position).GetPoint(10);
            point.z = transform.position.z;
              
            collider.Add(Physics2D.OverlapCircle(point,1f));

            if (collider.Count != 0)
            {
                foreach (Collider2D col in collider)
                {
                
                    if (col.gameObject == gameObject)
                    {
                        isTouched = true;
                    }
                }
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
            rb.AddForce(Vector3.up*jumpForce);
        }
    }
}
