using UnityEngine;
using TMPro;


public class Timer : MonoBehaviour
{

    [SerializeField] float timeLeft;
    [SerializeField] TextMeshProUGUI timerText;
    public FrogScript frogScript;

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else if (timeLeft < 0)
        {
            timeLeft = 0;
            frogScript.Invoke("GoToDeathScene", 1.0f);
        }

        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

}
