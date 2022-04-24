using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetector : MonoBehaviour
{
    [SerializeField] private bool IamLeft;
    [SerializeField] private Player player;
    [SerializeField] private float distanceChecker = 0.5f;
    [SerializeField] private float bounceForce;
    [SerializeField] private Vector2 direction;
    [SerializeField] private LayerMask layer;

    private void OnEnable()
    {
        if (IamLeft)
        {
            direction = Vector2.left;
        }
        else
        {
            direction = Vector2.right;
        }
    }

    private void FixedUpdate()
    {
    
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.5f,layer);
        Debug.DrawRay(transform.position, direction, Color.red);
        if (hit.collider != null)
        {
            if (IamLeft)
            {
                Bounce(-direction);
                player.wallLeftTouch = true;
            }
            else
            {
                Bounce(-direction);
                player.wallRightTouch = true;
            }
        }
        else
        {
            if (IamLeft)
            {
                player.wallLeftTouch = false;
            }
            else
            {
                player.wallRightTouch = false;
            }
        }

    }

    private void Bounce(Vector2 dir)
    {
        player.rb.AddForce(dir *(player.curentSpeed* bounceForce));
        player.curentSpeed = 0f;
    }
}
