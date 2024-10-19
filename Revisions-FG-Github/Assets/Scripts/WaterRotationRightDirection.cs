using UnityEngine;

public class WaterRotationRightDirection : MonoBehaviour
{
    private float speed;

    void Start()
    {
        speed = Random.Range(10, 20);

    }


    void Update()
    {
        float angle = speed * Time.deltaTime;
        transform.rotation *= Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
