using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    [SerializeField] private Player player;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("FallZone"))
        {
            player.transform.position = player.lastGroundedPos;
        }
    }
}
