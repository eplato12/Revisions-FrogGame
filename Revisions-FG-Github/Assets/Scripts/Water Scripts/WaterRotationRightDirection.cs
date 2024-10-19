using UnityEngine;

public class WaterRotationRightDirection : MonoBehaviour
{
    private float speed;
    private float minRotationSpeed = 10;
    private float maxRotationSpeed = 20;

    void Start()
    {
        speed = Random.Range(minRotationSpeed, maxRotationSpeed);

    }


    void Update()
    {
        float angle = speed * Time.deltaTime;
        transform.rotation *= Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
