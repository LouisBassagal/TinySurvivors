using UnityEngine;

public class XPGatherManager : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("XP"))
            return;
        
        collision.gameObject.GetComponent<XPBehavior>().StartFollowPlayerAnimation(transform.parent);
    }
}
