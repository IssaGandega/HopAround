using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Toad : MonoBehaviour
{
    [SerializeField]private List<Vector3> followTr;
    [SerializeField] private float offset;
    [SerializeField] private float playerToadDistance;
    [SerializeField] private float targetToadDistance;
    [SerializeField] private Transform playerTr;
    [SerializeField] private float speed = 0.1f;
    private bool followPlayer;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.name == "Player") && (!followPlayer))
        {
            playerTr = other.gameObject.transform;
            LVLManager.instance.AddToad();
            followPlayer = true;
        }
    }

    private void Update()
    {
        if (followPlayer)
        {
            playerToadDistance = Vector3.Distance(gameObject.transform.position, playerTr.position);

            if ((playerToadDistance > offset) && (followTr.Count > 0))
            {
                Move();
            }
            else
            {
                Record();
            }
        }
    }

    private void Record()
    {

        if (followTr.Count == 0)
        {
            followTr.Add(playerTr.position);
        }
        else
        {
            if (followTr[followTr.Count-1] != playerTr.position)
            {
                followTr.Add(playerTr.position);
            }
        }
        
   
      
    }

    private  void Move()
    {
        if (followTr.Count != 0)
        {
            targetToadDistance = Vector3.Distance(transform.position, followTr[0]);
            transform.position = Vector3.MoveTowards(transform.position, followTr[0], speed);

            if (targetToadDistance < 0.05f)
            {
                followTr.Remove(followTr[0]);
            }
        }
    }
}
