using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;
    private Vector3 targetPosition;
    
     
    void LateUpdate()
    {
        if ((transform.position != target.position) && (target.name == "Player"))
        {
          
            targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
        else
        {
            targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
