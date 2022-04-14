using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Transform lastCheckpointPos;

    private void OnCollisionEnter2D(Collision2D other)
    {
       
        if (other.gameObject.CompareTag("FallZone"))
        {
            player.transform.position = lastCheckpointPos.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            lastCheckpointPos = other.gameObject.transform;
            other.gameObject.SetActive(false);
        }
    }
}
