using System.Collections.Generic;
using UnityEngine;

public class Toad : MonoBehaviour
{
    static public List<Toad> toadsFollowingPlayer = new List<Toad>();
    [SerializeField] private Transform playerTr;
    private bool followPlayer;
    private bool recordStart;
    public float moveSpeed = 5f;
    private ToadTarget currTarget;
    private int index;
    private Vector3 pt;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Player")) && (!followPlayer))
        {
            playerTr = other.gameObject.transform;
            LVLManager.instance.AddToad();
            followPlayer = true;
            currTarget = playerTr.GetComponent<ToadTarget>();
            toadsFollowingPlayer.Add(this);
        }
    }
    

   private void Update()
   {
       if (currTarget != null)
       {
           index = currTarget.points.Count - (toadsFollowingPlayer.IndexOf(this) + 1);
           if (index >= 0 && index < currTarget.points.Count)
           {
               pt = currTarget.points[index];
               //transform.position = Vector3.Lerp(transform.position, pt, Time.deltaTime * lerpSpeed);
               transform.position = Vector3.MoveTowards(transform.position, pt, Time.deltaTime * moveSpeed);
           }
       }
   }
}
