using TMPro;
using UnityEngine;
public class Coundown : MonoBehaviour
{
    public float TimeLeft;
    public bool TimeOn = false;

    public TextMeshProUGUI TimerTxt;

    public GameOver gameOver;
    public void Start()
    {
        TimeOn = true;
    }

    public void Update()
    {
        if (TimeOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                gameOver.gameOver();
                TimeLeft = 0;
                TimeOn = false;
            }
        }
    }
    public void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

}
