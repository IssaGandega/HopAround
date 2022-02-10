using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    private Vector3 point;
    private Transform objectMovePose;
    private Vector3 newPoint;
    public bool isGrabing;
    [SerializeField] private float range = 10;
    [SerializeField] private LineRenderer line;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask layer;
    private bool pointCanMove;
    private float distance;
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if ((Input.GetTouch(0).phase == TouchPhase.Began) && (isGrabing == false))
            {
                pointCanMove = false;
                point = cam.ScreenPointToRay(Input.touches[0].position).GetPoint(10);

                point.z = 0;
            
                RaycastHit2D hit = Physics2D.Raycast(transform.position, point-transform.position,range,layer);
                Debug.DrawRay(transform.position,point-transform.position,Color.green);
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider);
                    if (hit.collider.GetComponent<Rigidbody2D>())
                    {
                        pointCanMove = true;
                    }
        
                    if (Vector3.Distance(transform.position,hit.point) < range)
                    {
                        distance = 999;
                        isGrabing = true;
                        point = hit.point;
                    }
                }
            }
            
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                isGrabing = false;
            }
        }
        
        if (isGrabing)
        {
            if (pointCanMove)
            {
                //point + objectMovePoint;
            }
            line.gameObject.SetActive(true);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, point);
            transform.right = point - transform.position;
            if (distance > 0)
            {
                distance = Vector3.Distance(transform.position, point);
                transform.up = point - transform.position;
                rb.AddForce(transform.up*speed);
            }
        }
        else
        {
            line.gameObject.SetActive(false);
        }
        
        
    }
}
