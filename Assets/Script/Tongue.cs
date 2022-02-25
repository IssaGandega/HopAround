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
    [SerializeField] private float offsetFromPoint = 10;
    [SerializeField] private LineRenderer line;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Transform pointTr;
    private bool tongueReachedPoint;
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
                if (hit.collider != null)
                {
                    if (hit.collider.GetComponent<Rigidbody2D>())
                    {
                        pointCanMove = true;
                    }
        
                    if (Vector3.Distance(transform.position,hit.point) < range)
                    {
                        distance = 999;
                        isGrabing = true;
                        point = hit.point;
                        pointTr.position = gameObject.transform.position;
                    }
                }
            }
            
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                isGrabing = false;
                rb.gravityScale = 1f;
            }
        }
        
        if (isGrabing)
        {
            line.gameObject.SetActive(true);
            if (tongueReachedPoint)
            {
                if (pointCanMove)
                {
                    //point + objectMovePoint;
                } 
                distance = Vector3.Distance(transform.position, point);
                line.SetPosition(0, transform.position);
                line.SetPosition(1, point);
                transform.right = point - transform.position;
                if (distance > offsetFromPoint)
                {
                    transform.up = point - transform.position;
                    rb.AddForce(transform.up*speed);
                }
                else
                {
                    rb.velocity = Vector2.zero;
                    rb.gravityScale = 0;
                }
            }
            else
            {
                line.SetPosition(0, transform.position);
                line.SetPosition(1, pointTr.position);
                pointTr.DOMove(point, 0.2f);
                StartCoroutine(WaitForTongue());
            }

        }
        else
        {
            line.gameObject.SetActive(false);
            tongueReachedPoint = false;
        }
    }

    private IEnumerator WaitForTongue()
    {
        yield return new WaitForSeconds(0.2f);
        tongueReachedPoint = true;
    }
}
