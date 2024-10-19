using UnityEngine;


public class UIFrogScript : MonoBehaviour
{

    private float leftBoundary = -12f;  
    private float rightBoundary = 12f;  
    private float jumpDistance = 2f;  
    private float moveDirection = 1f;  
    private SpriteRenderer frogSR;
    private float jumpInterval = 1f;  
    private float jumpTimer;  

    void Start()
    {
        frogSR = GetComponent<SpriteRenderer>();
        jumpTimer = jumpInterval;  
    }

    void Update()
    {
        jumpTimer -= Time.deltaTime;  

        if (jumpTimer <= 0f)
        {
            MoveFrog();
            jumpTimer = jumpInterval;  
        }
    }

    void MoveFrog()
    {
        Vector3 newPosition = transform.position + Vector3.right * jumpDistance * moveDirection;

        if (newPosition.x >= rightBoundary || newPosition.x <= leftBoundary)
        {
            moveDirection *= -1;
            FlipSprite();
        }
        else
        {
            transform.position = newPosition;
        }

    }

    private void FlipSprite()
    {
        frogSR.flipX = !frogSR.flipX;
    }

}
