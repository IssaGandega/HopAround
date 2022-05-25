﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float smoothing;
    [SerializeField] private Vector2 maxPosition;
    [SerializeField] private Vector2 minPosition;
    [SerializeField] private Vector2 baseOffset;
    private Vector3 targetPosition;
    private Vector3 velocity;
    private Vector2 currentOffset;
    public GameObject playerController;
    public List<Transform> waypointShowLevel;
    private bool camIsOverTarget;
    private float distance;
    private void Start()
    {
        StartCoroutine(FollowTrajectory(waypointShowLevel));
        currentOffset = baseOffset;
    }

    IEnumerator FollowTrajectory(List<Transform> waypoints)
    {
        playerController.SetActive(false);

        for (int x = 0; x < waypoints.Count; x++)
        {
            target = waypoints[x];
            yield return new WaitUntil(() => camIsOverTarget == true);
        }
        
        playerController.SetActive(true);
        target = playerController.transform;
    }

    public void ChangeOffset(Vector2 vector)
    {
        currentOffset = vector;
    }

    public void ResetOffset()
    {
        currentOffset = baseOffset;
    }


    void FixedUpdate()
    {
        if (transform.position != target.position)
        {
            targetPosition = new Vector3(target.position.x + currentOffset.x, target.position.y + currentOffset.y, transform.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition,ref velocity, smoothing);
            //transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
            distance = Vector3.Distance(transform.position, targetPosition);
        }

        if (distance < 0.2f)
        {
            camIsOverTarget = true;
        }
        else
        {
            camIsOverTarget = false;
        }
    }
}
