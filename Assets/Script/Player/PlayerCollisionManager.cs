using System;
using System.Collections;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Transform lastCheckpointPos;
    private Vector3 spawnPoint;
    
    [SerializeField] private AudioClip frogDeath;
    [SerializeField] private AudioClip frogContact;
    [SerializeField] private AudioClip checkpoint;
    

    private void Start()
    {
        spawnPoint = player.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
       
        if (other.gameObject.CompareTag("FallZone"))
        {
            SoundManager.instance.PlaySound(frogDeath);
            PlayerAnimatorManager.instance.AnimatorStateChange(3);
            player.enabled = false;
            StartCoroutine(PlayerRestart());

        }
        else if (other.gameObject.layer == 6)
        {
            SoundManager.instance.PlaySound(frogContact);
            player.isJumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            SoundManager.instance.PlaySound(checkpoint);
            lastCheckpointPos = other.gameObject.transform;
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            other.gameObject.GetComponentInChildren<Animator>().SetBool("Go",true);
        }
    }

    public IEnumerator PlayerRestart()
    {
        player.targetSpeed = 0;
        player.currentSpeed = 0;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(1.4f);
        player.enabled = true;
        if (player.transform.position.y != 0)
        {
            player.Flip();
        }
        PlayerAnimatorManager.instance.AnimatorStateChange(0);
        
        if (lastCheckpointPos != null)
        {
            player.transform.position = lastCheckpointPos.position;
        }
        else
        {
            player.transform.position = spawnPoint;
        }
    }
}
