using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public Animator animator;
    public float force;
    [SerializeField] private float speedInTime;
    [SerializeField]private Transform[] waypoints;
    private Transform currentWaypoint;
    private int currentWaypointNo;
    private float distance;
    [SerializeField] private bool move = false;
    public bool playerIsEjected;
    [SerializeField] private Transform playerAnimPos;
    [SerializeField] private JumperAnimatorEvent animatorEvent;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            ChangeState();
            animator.SetInteger("State" ,1);
            player = col.gameObject;
            playerIsEjected = true;
            animatorEvent.player = player;
        }
    }



    public void Tongued()
    {
        animator.SetInteger("State" ,2);
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

        if (playerIsEjected)
        {
            player.transform.position = playerAnimPos.position;
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
        }
        else
        {
            transform.rotation = new Quaternion(0,180,0,0);
        }
 
    }

    public void ChangeState()
    {
        move = !move;
    }



}
