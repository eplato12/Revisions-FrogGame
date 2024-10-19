using UnityEngine;

public class PortalManager : MonoBehaviour
{
    [SerializeField] GameObject frog;
    [SerializeField] GameObject arrow;
    [SerializeField] int level;
    [SerializeField] AudioClip portalSound;
    [SerializeField] AdvanceScene advanceScene;

    public bool reachedPortal = false;
    public int starCounter = 0;

    //private int starCounter = 0;
    private SpriteRenderer portalSR;
    private CircleCollider2D portalCollider;
    private AudioSource portalAudio;

    void Start()
    {
        portalSR = GetComponent<SpriteRenderer>();
        portalCollider = GetComponent<CircleCollider2D>();
        portalAudio = GetComponent<AudioSource>();

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
        if (starCounter == 2)
        {
            portalSR.enabled = true;
            portalCollider.enabled = true;
        }

        if (reachedPortal)
        {
            HideFrogAndArrow();
        }
    }


    private void HideFrogAndArrow()
    {
        frog.SetActive(false);
        arrow.SetActive(false);
    }
}
