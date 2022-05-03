using System;
using System.Collections;
using System.Linq;
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
    private bool hasTongueScript;
    private bool tongueCd;
    private bool pointCanMove;
    public bool frogReachedPoint;
    private float distance;
    
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
                
                if ((distance > 0.5f) && (!hasTongueScript))
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
                line.enabled = false;;
                if (!hasTongueScript)
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
        if ((isGrabing) && (tongueReachedPoint))
        {
            StartCoroutine(TongueReset());
        }
    }

    public void TongueStart(RaycastHit2D hit)
    {
        if (!tongueCd)
        {
            line.enabled = true;
            if (isGrabing == false)
            {
                touchedObj = hit.collider.gameObject.transform;
                isGrabing = true;
                hasTongueScript = touchedObj.GetComponent<ITonguable>() != null;

                if (Vector3.Distance(transform.position, hit.point) < range)
                {
                    distance = 999;
                    pointTr.position = touchedObj.transform.position;
                    line.enabled = true;
                    //Lancer Tongued
                    if (hasTongueScript)
                    {
                        touchedObj.GetComponent<ITonguable>().Tongued();
                        StartCoroutine(TongueReset());
                    }
                }
            }
        }
    }

    public IEnumerator TongueReset()
    {
        isGrabing = false;
        tongueReachedPoint = false;
        touchedObj = null;
        frogReachedPoint = false;
        transform.parent = playerContainer;
        line.enabled = false;
        rb.gravityScale = 1f;
        pointTr.position = Vector3.zero;
        if (!hasTongueScript)
        {
            player.rb.AddForce((player.jumpForce*Vector2.up)*4);
        }

        tongueCd = true;
        yield return new WaitForSeconds(0.5f);
        tongueCd = false;
    }
    

    private IEnumerator WaitForTongue()
    {
        yield return new WaitForSeconds(0.2f);
        tongueReachedPoint = true;
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
        line.SetPosition(3, target * 0.75f);
        line.SetPosition(2, target * 0.5f);
        line.SetPosition(2, target * 0.25f);
    }
}
