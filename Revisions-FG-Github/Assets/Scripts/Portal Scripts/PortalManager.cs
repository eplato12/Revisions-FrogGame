using UnityEngine;

public class PortalManager : MonoBehaviour
{
    [SerializeField] FrogScript frog;
    [SerializeField] int level;

    private SpriteRenderer portalSR;
    private CircleCollider2D portalCollider;


    void Start()
    {
        portalSR = GetComponent<SpriteRenderer>();
        portalCollider = GetComponent<CircleCollider2D>();

        if (level == 5)
        {
            portalSR.enabled = false;
            portalCollider.enabled = false;
        }
        else
        {
            portalSR.enabled = true;
            portalCollider.enabled = true;
        }


    }

    void Update()
    {
        if (level == 5 && frog.starCounter == 2)
        {
            portalSR.enabled = true;
            portalCollider.enabled = true;
        }
    }
}


