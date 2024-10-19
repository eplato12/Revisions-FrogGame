using UnityEngine;

public class tutorialscript : MonoBehaviour
{
    [SerializeField] GameObject UIElement;
    [SerializeField] KeyCode trigger;
    [SerializeField] GameObject[] tutorials;
    private static bool _hasInitialized = false;

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
