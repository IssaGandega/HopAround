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
    private bool frogReachedPoint;
    private float distance;
    

    public void FixedUpdate()
    {
        if (isGrabing)
        {
            pointTr.transform.position = touchedObj.transform.position;
            line.gameObject.SetActive(true);
            if ((tongueReachedPoint) && (!frogReachedPoint))
            {
                distance = Vector3.Distance(transform.position, pointTr.transform.position);
                UpdateLR();
                
                if ((distance > 0.5f) && (!pointIsAnInteractable))
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
            
            if (frogReachedPoint)
            {
                rb.gravityScale = 0;
                //line.gameObject.SetActive(false);
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
        if ((Input.GetTouch(0).phase == TouchPhase.Began) && (isGrabing) && (tongueReachedPoint))
        {
            StartCoroutine(TongueReset());
        }
    }

    public void TongueStart(Collider2D hit)
    {
        if (!tongueCd)
        {
            if (isGrabing == false)
            {
                touchedObj = hit.gameObject.transform;
                isGrabing = true;

                if (Vector3.Distance(transform.position, hit.gameObject.transform.position) < range)
                {
                    distance = 999;
                    pointTr.position = touchedObj.transform.position;
                    
                    if (touchedObj.GetComponent<Switch>())
                    {
                        line.gameObject.SetActive(true);
                        pointIsAnInteractable = true;
                    }
                    else if (hit.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
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
        line.gameObject.SetActive(false);
        touchedObj = null;
        frogReachedPoint = false;
        pointIsAnInteractable = false;
        transform.parent = playerContainer;
        rb.gravityScale = 1f;
        pointTr.position = Vector3.zero;
        if (pointCanMove)
        {
            player.rb.AddForce((player.jumpForce*Vector2.up)*4);
            pointCanMove = false;
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
            //line.transform.LookAt(pointTr);
            Vector3 target = pointTr.position;
            // diviser z et x par ce qu'il faut pour aligner points
            target.z = target.x - transform.position.x;
            target.y -= transform.position.y; 
            target.x = 0;
            line.SetPosition(4, target);
        }
}
