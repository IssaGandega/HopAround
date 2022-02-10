using UnityEngine;
 
public class FollowGyro : MonoBehaviour
{
    [SerializeField] private Vector3 startEulerAngles;
    [SerializeField] private Vector3 startGyroAttitudeToEuler;
    private Vector3 deltaEulerAngles;
    private void Start()
    {
        Input.gyro.enabled = true;
        startEulerAngles = transform.eulerAngles;
        startGyroAttitudeToEuler = Input.gyro.attitude.eulerAngles;
    }
 
    private void Update()
    {
        deltaEulerAngles = Input.gyro.attitude.eulerAngles - startGyroAttitudeToEuler;
        //Debug.Log(Input.gyro.attitude.eulerAngles);
        deltaEulerAngles.x = 0.0f;
        deltaEulerAngles.y = 0.0f;
        //deltaEulerAngles.z = 0.0f;


        transform.eulerAngles = startEulerAngles - deltaEulerAngles;
    }
}
 

