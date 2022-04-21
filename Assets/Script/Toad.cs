using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toad : MonoBehaviour
{
    static public List<Toad> toadsFollowingPlayer = new List<Toad>();
    [SerializeField] private Transform playerTr;
    private bool recordStart;
    public float moveSpeed = 5f;
    private ToadTarget currTarget;
    private int index;
    private float distance;
    private Vector3 pt;
    [SerializeField] private Animator animator;
    [SerializeField] private Material walk;
    [SerializeField] private Material idle;
    [SerializeField] private MeshRenderer mR;
    [SerializeField] private GameObject particleSystem;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Player")) && (currTarget == null))
        {
            playerTr = other.gameObject.transform;
            currTarget = playerTr.GetComponent<ToadTarget>();
            particleSystem.SetActive(false);

            animator.SetBool("Go",true);
            StartCoroutine(WaitForOpening(other.gameObject.transform));
        }
    }

    public IEnumerator WaitForOpening(Transform tr)
    {
        yield return new WaitForSeconds(0.5f);
        LVLManager.instance.AddToad();
        toadsFollowingPlayer.Add(this);
    }


   private void Update()
   {
       if (currTarget != null)
       {
           index = currTarget.points.Count - (toadsFollowingPlayer.IndexOf(this) + 1);
           if (index >= 0 && index < currTarget.points.Count)
           {
               distance = Vector3.Distance(transform.position, pt);
               pt = currTarget.points[index];
               transform.position = Vector3.MoveTowards(transform.position, pt, Time.deltaTime * moveSpeed);
               if (distance > 0.3f)
               {
                   mR.material = walk;
               }
               else
               {
                   mR.material = idle;

               }
           }
       }
   }


}
