using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ToadTarget : MonoBehaviour
{
    public List<Vector3> points = new List<Vector3>();
    public int size = 5;
    public float radius = 1f;
    public Player player;
    public LayerMask groundLayer;
    private Vector3 actualPos;
    private RaycastHit2D hit;
    
    private void Update()
    {
 
        while (points.Count > size) 
            points.RemoveAt(0);

        actualPos = transform.position;
        if (player != null && player.isGrounded)
        {
            hit = Physics2D.CircleCast(transform.position, radius, -Vector3.up, 1000f, groundLayer);
            if (hit.collider != null)
            {
                actualPos = hit.point + Vector2.up * radius;
            }
        }
        
        if (points.Count == 0 || Vector3.Distance(actualPos, points.Last()) >= radius * 2f)
        {
            points.Add(actualPos);
        }
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            for (int i = 0; i < points.Count; i++)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(points[i], radius);
                UnityEditor.Handles.Label(points[i],i.ToString());
            }
        }
    }
    #endif
}