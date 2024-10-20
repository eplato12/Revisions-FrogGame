using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class FrogScript : MonoBehaviour
{

    public float lilyPadColliderWidth;
    public int starCounter = 0; // for level 5 portal activation

    [Header("Audio Clips")]
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip splashSound;
    [SerializeField] AudioClip portalSound;
    [SerializeField] AudioClip ribbitSound;

    [Header("Other Scripts")]
    [SerializeField] AdvanceScene advanceScene;
    [SerializeField] lily lilly;
    [SerializeField] ParticleSystem splashParticleSystem;
    [SerializeField] SpawnLilyPads spawner;

    [Header("GameObjects")]
    [SerializeField] GameObject firstLily;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject portal;

    [Header("Level Indicator")]
    [SerializeField] int level;

    private AudioSource frogAudio;
    private SpriteRenderer frogSprite;
    private Animator frogAnimator;
    private Coroutine randomSoundCoroutine;
    private float jumpDistance = 1f;
    private float newSpeed = 2.0f;


    void Start()
    {
        frogSprite = GetComponent<SpriteRenderer>();
        frogAudio = GetComponent<AudioSource>();
        frogAnimator = GetComponent<Animator>();
        frogSprite.enabled = true;
        transform.position = firstLily.transform.position;
        frogAnimator.SetBool("isJumping", false);
        randomSoundCoroutine = StartCoroutine(PlaySoundRandomly(ribbitSound));

        if (level == 5)
        {
            portal.SetActive(false);
        }
    }

    void Update()
    {
        if (transform.position != firstLily.transform.position)
        {
            firstLily.SetActive(false);
        }

        if (starCounter == 2 && level == 5)
        {
            portal.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();

            if (IsPortal(transform.position))
            {
                Invoke("HandlePortal", 1.0f);
            }

            else
            {
                IsLily(transform.position);
                if (level == 5)
                {
                    IsStar();
                }
            }
        }

    }

    public void GoToDeathScene()
    {
        advanceScene.toLevel("Frog Die");
    }

    private void Jump()
    {
        frogAnimator.SetBool("isJumping", true);
        transform.position += transform.up * jumpDistance;
        PlayAudio(jumpSound);
        StartCoroutine(HandleLanding());
    }

    private bool IsPortal(Vector2 gridPosition)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gridPosition, 0.2f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("portal"))
            {
                PlayAudio(portalSound);
                HideFrogAndArrow();
                return true;
            }
        }
        return false;
    }

    private void HideFrogAndArrow()
    {
        frogSprite.enabled = false;
        arrow.SetActive(false);
    }

    private void HandlePortal()
    {
        //SceneTransitionInfo.NextSceneName = GetNextScene(SceneManager.GetActiveScene().name);
        //advanceScene.toLevel("LoadingScene");
        advanceScene.LoadNextScene();
    }

    private IEnumerator HandleLanding()
    {
        yield return new WaitForSeconds(frogAnimator.GetCurrentAnimatorStateInfo(0).length);
        frogAnimator.SetBool("isJumping", false);
    }

    private void IsLily(Vector2 gridPosition)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gridPosition, lilyPadColliderWidth);
        bool isOnLily = false;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("lily") || collider.CompareTag("starLily1") || collider.CompareTag("starLily2") || collider.CompareTag("evilLily"))
            {
                transform.parent = collider.gameObject.transform;
                transform.localPosition = Vector3.zero;
                isOnLily = true;
                break;
            }

        }
        if (!isOnLily)
        {
            HideFrogAndArrow();
            PlayAudio(splashSound);
            Instantiate(splashParticleSystem, transform.position, Quaternion.identity);
            advanceScene.Invoke("ReloadScene", 0.5f);
        }
    }

    private void PlayAudio(AudioClip clip)
    {
        frogAudio.PlayOneShot(clip);
    }

    private IEnumerator PlaySoundRandomly(AudioClip sound)
    {
        while (true)
        {
            float minSeconds = 5;
            float maxSeconds = 15;
            yield return new WaitForSeconds(Random.Range(minSeconds, maxSeconds));
            PlayAudio(ribbitSound);
        }
    }

    private void IsStar()
    {
        if (transform.parent.CompareTag("starLily1"))
        {
            starCounter++;
            Destroy(spawner.star1);
            transform.parent.tag = "lily";
        }


        if (transform.parent.CompareTag("starLily2"))
        {
            starCounter++;
            Destroy(spawner.star2);
            transform.parent.tag = "lily";
        }
    }


}


