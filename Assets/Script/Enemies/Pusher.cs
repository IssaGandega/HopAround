using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Pusher : MonoBehaviour, ITonguable
{

    [SerializeField] private Animator animator;
    [SerializeField] private float force;
    
    [SerializeField] private float speedInTime;
    private bool isFacingLeft;

    [SerializeField]private Transform[] waypoints;
    private Transform currentWaypoint;
    private int currentWaypointNo;
    private float distance;
    [SerializeField] private bool move = false;
    [SerializeField] private AudioClip pusherSound;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            ChangeState();
            SoundManager.instance.PlaySound(pusherSound);
            animator.SetInteger("State" ,1);
            if (isFacingLeft)
            {
                col.GetComponent<Player>().AddForceToPlayer(Vector2.left, force);
            }
            else
            {
                col.GetComponent<Player>().AddForceToPlayer(Vector2.right, force);

                Debug.Log(Vector2.right*force);

            }
    
            StartCoroutine(CD());
        }
    }

    public void Tongued()
    {
        animator.SetInteger("State" ,2);
        transform.DOKill();
        ChangeState();
        StartCoroutine(CD());
    }


    private IEnumerator CD()
    {
        yield return new WaitForSeconds(1f);
        animator.SetInteger("State" ,0);
        ChangeState();
    }
    
    private void OnEnable()
    {
        ChangeWaypoint();
    }

    private void FixedUpdate()
    {
        if (move)
        {
            transform.DOMove(currentWaypoint.position, speedInTime);
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
            transform.rotation = new Quaternion(0,0,0,0);
            isFacingLeft = true;
        }
        else
        {
            isFacingLeft = false;
            transform.rotation = new Quaternion(0,180,0,0);
        }
 
    }

    public void ChangeState()
    {
        move = !move;
    }

}
