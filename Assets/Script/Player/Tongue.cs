using System;
using System.Collections;
using System.Security.Permissions;
using DG.Tweening;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    private Transform objectMovePose;
    private Vector3 newPoint;
    public bool isGrabing;
    [SerializeField] private float range = 10;
    [SerializeField] private LineRenderer line;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float timeToReachPoint =0.5f;
    [SerializeField] private Transform pointTr;
    [SerializeField] private Transform playerContainer;
    [SerializeField] private Player player;
    public Transform touchedObj;
    private bool tongueReachedPoint;
    private bool tongueCd;
    private bool pointCanMove;
    private bool pointIsAnInteractable;
    public bool frogReachedPoint;
    private float distance;

    private void OnEnable()
    {
        line.enabled = false;;
    }

    public void FixedUpdate()
    {
        if (isGrabing)
        {
            pointTr.transform.position = touchedObj.transform.position;
            line.enabled = true;
            if ((tongueReachedPoint) && (!frogReachedPoint))
            {
                distance = Vector3.Distance(transform.position, pointTr.transform.position);
                UpdateLR();
                
                if ((distance > 1f) && (!pointIsAnInteractable))
                {
                    transform.DOMove(pointTr.transform.position, timeToReachPoint);
                }
                else
                {
                    frogReachedPoint = true;
                }
            }
            else
            {
                UpdateLR();
                pointTr.DOMove(pointTr.transform.position, 0.2f);
                StartCoroutine(WaitForTongue());
            }
            
            if (frogReachedPoint && !pointIsAnInteractable)
            {
                rb.gravityScale = 0;
                line.enabled = false;
                if (pointCanMove)
                {
                    transform.parent = touchedObj;
                    transform.position = touchedObj.position;
                    rb.gravityScale = 0;
                }
                else
                {
                    rb.velocity = Vector2.zero;
                }
            }
        }

        if (Input.touchCount <= 0) return;
        if ((Input.GetTouch(0).phase == TouchPhase.Began) && (isGrabing) && (frogReachedPoint))
        {
            StartCoroutine(TongueReset());
            StopCoroutine(WaitForTongue());
        }
    }

    private void CheckPosition()
    {
        if (Vector3.Dot(transform.forward, pointTr.position - transform.position) < 0)
        {
            player.Flip();
        }
    }

    public void TongueStart(RaycastHit2D hit)
    {
        if (!tongueCd)
        {
            line.enabled = true;
            if (isGrabing == false)
            { 
                touchedObj = hit.transform;
                isGrabing = true;


                if (Vector3.Distance(transform.position, hit.point) < range)
                {
                    distance = 999;
                    pointTr.position = touchedObj.transform.position;
                    CheckPosition();
                    
                    if (touchedObj.GetComponent<Switch>())
                    {
                        line.enabled = true;
                        pointIsAnInteractable = true;
                    }
                    else if (hit.collider.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
                    {
                        pointCanMove = true;
                    }
                }
            }
        }
    
    }

    public IEnumerator TongueReset()
    {
        isGrabing = false;
        tongueReachedPoint = false;
        line.enabled = false;
        
        frogReachedPoint = false;
        pointIsAnInteractable = false;
        
        player.transform.parent = playerContainer;
        rb.gravityScale = 1f;
        
        pointTr.position = player.transform.position;
        
        if (pointCanMove)
        {
            Vector3 inertia = touchedObj.gameObject.GetComponentInChildren<ObjMovement>().rb.velocity;
            
            player.rb.AddForce((player.jumpForce*Vector2.up)* (20 * Math.Abs(inertia.x)));
            pointCanMove = false;
            touchedObj = null;
        }

        tongueCd = true;
        yield return new WaitForSeconds(0.05f);
        tongueCd = false;
    }
    

        private IEnumerator WaitForTongue()
    {
        yield return new WaitForSeconds(0.2f);
        tongueReachedPoint = true;
        if (pointIsAnInteractable)
        {
            touchedObj.GetComponent<Switch>().TongueTouched();
            StartCoroutine(TongueReset());
        }
    }
        private void UpdateLR()
        {
            Vector3 target = pointTr.position;
            // diviser z et x par ce qu'il faut pour aligner points
            target.z = target.x - transform.position.x;
            target.y -= transform.position.y; 
            target.x = 0;

            if (!player.isFacingRight)
            {
                target.z *= -1;
            }
            
            line.SetPosition(4, target);
            line.SetPosition(3, target * 0.75f);
            line.SetPosition(2, target * 0.5f);
            line.SetPosition(1, target * 0.25f);
        }
}
