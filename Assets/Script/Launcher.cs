using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public Animator animator;
    public float force;
    [SerializeField] private float speedInTime;
    [SerializeField] private Transform launcherTransform;
    [SerializeField]private Transform[] waypoints;
    private Transform currentWaypoint;
    private int currentWaypointNo;
    private float distance;
    private bool cD;
    [SerializeField] private bool move = false;
    [SerializeField] private Transform playerAnimPos;
    public GameObject player;
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && (cD == false))
        {
            move = false;
            player = col.gameObject;
            launcherTransform.DOKill(false);
            player.transform.DOMove(playerAnimPos.position,0.4f);
            ChangeState();
            animator.SetInteger("State" ,1);
            StartCoroutine(CD());
        }
    }



    public void Tongued()
    {
        animator.SetInteger("State" ,2);
        ChangeState();
        StartCoroutine(CD());
    }


    public IEnumerator CD()
    {
        cD = true;
        yield return new WaitForSeconds(1f);
        animator.SetInteger("State" ,0);
        cD = false;
        move = true;
    }
    
    private void OnEnable()
    {
        ChangeWaypoint();
    }

    private void FixedUpdate()
    {
        if (move)
        {
            launcherTransform.DOMove(currentWaypoint.position, speedInTime);
        }

        distance = Vector3.Distance(currentWaypoint.position, transform.position);

        if (distance < 1)
        {
            if (currentWaypointNo == waypoints.Length-1)
            {
                currentWaypointNo = 0;
                ChangeWaypoint();
            }
            else
            {
                currentWaypointNo++;
                ChangeWaypoint();
            }
        }
    }
    
    private void ChangeWaypoint()
    {
        currentWaypoint = waypoints[currentWaypointNo];
        if (currentWaypoint.transform.position.x < transform.position.x)
        {
            launcherTransform.DORotate(new Vector3(0,90,0),0.1f);
        }
        else
        {
            launcherTransform.DORotate(new Vector3(0,-90,0),0.1f);
        }
 
    }

    public void ChangeState()
    {
        move = !move;
    }
  



}
