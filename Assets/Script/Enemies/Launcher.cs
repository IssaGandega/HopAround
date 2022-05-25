using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Launcher : MonoBehaviour, ITonguable
{
    public Animator animator;
    public float force;
    [SerializeField] private float speedInTime;
    [SerializeField] private Transform launcherTransform;
    [SerializeField]private Transform[] waypoints;
    [SerializeField] private AudioClip launcherSound;
    [SerializeField] private Collider2D tongueCollider;
    
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
            SoundManager.instance.PlaySound(launcherSound);
            move = false;
            player = col.gameObject;
            launcherTransform.DOKill(false);
            player.transform.DOMove(playerAnimPos.position,0.4f);
            ChangeState();
            animator.SetInteger("State" ,1);
        }
    }

    public void Tongued(Tongue tongue)
    {
        tongue.pointIsAnInteractable = true;
        move = false;
        launcherTransform.DOKill(false);
        animator.SetInteger("State" ,2);
        tongueCollider.enabled = false;
        StartCoroutine(CD());
    }


    public IEnumerator CD()
    {
        cD = true;
        yield return new WaitForSeconds(1f);
        animator.SetInteger("State" ,0);
        tongueCollider.enabled = true;
        cD = false;
        move = true;
    }
    
    private void Start()
    {
        ChangeWaypoint();
    }

    private void FixedUpdate()
    {
        if (move)
        {
            launcherTransform.DOMove(currentWaypoint.position, speedInTime).SetEase(Ease.Linear);
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
            //SoundManager.instance.PlaySound(launcherWalking);
            launcherTransform.DORotate(new Vector3(0,90,0),0.1f);
        }
        else
        {
            //SoundManager.instance.PlaySound(launcherWalking);
            launcherTransform.DORotate(new Vector3(0,-90,0),0.1f);
        }
 
    }

    public void ChangeState()
    {
        move = !move;
    }
  



}
