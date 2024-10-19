using UnityEngine;

public class evilLily : MonoBehaviour
{

    [SerializeField] float rotationSpeed = 30f;
    private int rotate;



    void Start()
    {
        rotate = Random.Range(0, 2) == 0 ? 1 : -1;
    }

    void Update()
    {

        transform.Rotate(0, 0, rotationSpeed * rotate * Time.deltaTime);
    }

}
