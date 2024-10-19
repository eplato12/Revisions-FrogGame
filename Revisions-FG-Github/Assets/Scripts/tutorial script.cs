using UnityEngine;

public class tutorialscript : MonoBehaviour
{
    public GameObject UIElement;
    public KeyCode trigger;
    public GameObject[] tutorials;
    private static bool _hasInitialized = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(trigger))
        {
            Destroy(UIElement);
            _hasInitialized = true;

            foreach(GameObject tutorial in tutorials)
            {
                tutorial.SetActive(false);
            }
        }
    }

    private void Awake()
    {
        if (_hasInitialized)
        {
            UIElement.SetActive(false);
            foreach (GameObject tutorial in tutorials)
            {
                tutorial.SetActive(true);
            }
        }
    }
}
