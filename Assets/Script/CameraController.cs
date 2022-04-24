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
    private Vector3 targetPosition;
    private Vector3 velocity;


    void FixedUpdate()
    {
        if (transform.position != target.position)
        {
            targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition,ref velocity, smoothing);
            //transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
