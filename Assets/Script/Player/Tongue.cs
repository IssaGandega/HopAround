using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    private Transform objectMovePose;
    private Vector3 newPoint;
    private bool tongueReachedPoint;
    private bool tongueCd;
    public bool pointCanMove;
    public bool pointIsAnInteractable;
    public LayerMask obstacleLayer;
    private float distance;

    #region External objects

    [Header("Objects")]
    [Space(5)]
    
    [SerializeField] private Transform pointTr;
    [SerializeField] private Transform playerContainer;
    [SerializeField] private Player player;
    public LineRenderer line;
    [SerializeField] private Rigidbody2D rb;

    #endregion

    #region Parameters
    
    [Header("Setters")]
    [Space(5)]
    
    [SerializeField] private float range = 10;
    [SerializeField] private float timeToReachPoint =0.5f;
    [SerializeField] private Vector2 jumpOutVector;
    [SerializeField] private float inertiaStrength;
    [SerializeField] private AudioClip frogTongue;


    #endregion

    #region External Infos

    [Header("Informations")]
    [Space(5)]
    
    public Transform touchedObj;
    public bool isGrabing;
    public bool frogReachedPoint;

    #endregion

    #region Sounds

    [Header("Sounds")] 
    [Space(5)] 
    
    [SerializeField] private AudioClip tongueMecanism;

    #endregion
    
    private void OnEnable()
    {
        line.enabled = false;;
    }
    

    public bool TongueStart(Transform tObj)
    {
        if (!tongueCd)
        {
            SoundManager.instance.PlaySound(frogTongue);
            if (isGrabing == false)
            {
                touchedObj = tObj;
                var distanceCheck = Vector3.Distance(transform.position, touchedObj.position) < range;
                var lineCheck = !Physics2D.Linecast(player.transform.position, touchedObj.position, obstacleLayer);
                //Debug.Log("distanceCheck=" + distanceCheck + " lineCheck=" + lineCheck);
                if (distanceCheck && lineCheck)
                {
                    distance = 999;
                    pointTr.position = touchedObj.position;
                    if (Vector3.Dot(transform.forward, pointTr.position - transform.position) < 0)
                    {
                        player.Flip();
                    }
                    StartCoroutine(DoTongue());
                }
            }
        }

        return false;
    }

    private IEnumerator DoTongue()
    {
        var twSpeed = 22.2f;
        PlayerAnimatorManager.instance.AnimatorStateChange(5,true);

        //Debug.Log("DoTongue");
        isGrabing = true;
        line.enabled = true;
        pointTr.position = line.transform.position;

        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        rb.gravityScale = 0f;
        
        //Debug.Log("pointTr.DOMove");
        var inTw = pointTr.DOMove(touchedObj.position, twSpeed).SetSpeedBased(true);
        while (inTw.IsPlaying()) yield return null;
        
        pointIsAnInteractable = false;
        touchedObj.SendMessage("Tongued",this,SendMessageOptions.DontRequireReceiver);
        if (pointIsAnInteractable == false)
        {
            //Debug.Log("player.transform.DOMove");
            var outTw = player.transform.DOMove(pointTr.position, twSpeed).SetSpeedBased(true);
            while (outTw.IsPlaying()) yield return null;

            // attendre un touch pour sortir du grab
            while (true)
            {
               // Debug.Log("attendre un touch pour sortir du grab");
               line.enabled = false;
               player.transform.position = touchedObj.position;
                
               yield return null;
                
               if (Input.touches.Count(t => t.phase == TouchPhase.Began) > 0)
               {
                   break;
               }
            }
        }
        
        line.enabled = false;
        rb.isKinematic = false;
        rb.gravityScale = 1f;
        isGrabing = false;
        
        if (pointCanMove)
        {
            var launchForce = new Vector2(jumpOutVector.x * player.xAxisAccel * inertiaStrength, jumpOutVector.y);
            //Debug.Log("Launching Frog: " + launchForce);
            player.rb.AddForce(launchForce);
            pointCanMove = false;
        }
        
        //Debug.Log("EndTongue");
    }

    private void Update()
    {
        if (line.enabled)
        {
            //diviser z et x par ce qu'il faut pour aligner points
            Vector3 target = pointTr.position;
            target.z = target.x - transform.position.x;
            target.y -= transform.position.y; 
            target.x = 0;

            if (!player.isFacingRight)
            {
                target.z *= -1;
            }

            line.SetPosition(4, target);
            line.SetPosition(3, target * 0.75f);
            line.SetPosition(2, target * 0.5f);
            line.SetPosition(1, target * 0.25f);
        }
    }
}
