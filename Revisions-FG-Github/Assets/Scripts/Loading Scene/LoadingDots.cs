using UnityEngine;
using TMPro;
using System.Collections;


public class LoadingDots : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; 
    public float dotAnimationDelay = 0.5f;

    private void Start()
    {
        StartCoroutine(AnimateLoadingDots());
    }

    private IEnumerator AnimateLoadingDots()
    {
        while (true) 
        {
            textMeshPro.text = "Loading.";
            yield return new WaitForSeconds(dotAnimationDelay);

            textMeshPro.text = "Loading..";
            yield return new WaitForSeconds(dotAnimationDelay);

            textMeshPro.text = "Loading...";
            yield return new WaitForSeconds(dotAnimationDelay);
        }
    }

}
