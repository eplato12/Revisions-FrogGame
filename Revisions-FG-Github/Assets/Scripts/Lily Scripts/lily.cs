using System.Collections;
using UnityEngine;

public class lily : MonoBehaviour
{
    [SerializeField] Sprite evilLily;
    [SerializeField] Sprite niceLily;
    [SerializeField] float rotationSpeed = 90f;

    private SpriteRenderer spriteRenderer;
    private int rotate;
    private bool isEvil = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rotate = Random.Range(0, 2) == 0 ? 1 : -1;
        UpdateSprite();
    }

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * rotate * Time.deltaTime);
    }

    public void speedIncrease(float num)
    {
        rotationSpeed *= num;
    }

    public void SetIsEvil(bool evil)
    {
        isEvil = evil;
        UpdateSprite();
    }

    public void increaseSpeedBy(float speedMultiple)
    {
        rotationSpeed *= speedMultiple;
    }

    void UpdateSprite()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (spriteRenderer == null)
        {
            return;
        }

        if (isEvil)
        {
            spriteRenderer.sprite = evilLily;
        }
        else
        {
            spriteRenderer.sprite = niceLily;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") && isEvil)
        {
            StartCoroutine(IncreaseRotationSpeedForSeconds(6f));
        }
    }

    private IEnumerator IncreaseRotationSpeedForSeconds(float speedMultiplier)
    {
        float targetSpeed = rotationSpeed;
        float currentSpeed=rotationSpeed * speedMultiplier;
        float lerpTime=5;
        float elapsedTime=0;

        while (elapsedTime < lerpTime)
        {
            rotationSpeed = Mathf.Lerp(currentSpeed, targetSpeed, elapsedTime / lerpTime);
            elapsedTime += Time.deltaTime;


            yield return null;

        }
        rotationSpeed = targetSpeed;
        yield return null;
    }

}

