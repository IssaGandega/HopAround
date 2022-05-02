using System;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Transform lastCheckpointPos;
    private Vector3 spawnPoint;

    private void Start()
    {
        spawnPoint = player.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
       
        if (other.gameObject.CompareTag("FallZone"))
        {
            if (lastCheckpointPos)
            {
                player.transform.position = lastCheckpointPos.position;
            }
            else
            {
                player.transform.position = spawnPoint;
            }
        }
        else if (other.gameObject.layer == 6)
        {
            player.isJumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            lastCheckpointPos = other.gameObject.transform;
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            other.gameObject.GetComponentInChildren<Animator>().SetBool("Go",true);
        }
    }
}