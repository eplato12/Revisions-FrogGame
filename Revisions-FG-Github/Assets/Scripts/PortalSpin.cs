using UnityEngine;


public class PortalSpin : MonoBehaviour
{
    public float spinSpeed = 100f;  

    void Update()
    {
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
    }
}
