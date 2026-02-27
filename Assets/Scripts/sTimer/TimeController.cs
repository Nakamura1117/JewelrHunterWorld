using UnityEngine;


public class TimeController : MonoBehaviour
{

    public bool isCountDown = true;
    public float gameTime = 0;
    bool isTimeOver = false;
    float displayTime = 0;
    float times = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isCountDown)
        {
            displayTime = gameTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isTimeOver == false)
        {
            times += Time.deltaTime;
            if (isCountDown)
            {
                displayTime = gameTime - times + 1;

                if (displayTime <= 0.0f)
                {
                    displayTime = 0.0f;
                    IsTimeOver();
                }
            }
        }
        else
        {
            displayTime = times - 1;
            if (displayTime >= gameTime)
            {
                displayTime = gameTime;
                IsTimeOver();
            }
        }
    }

    public void IsTimeOver()
    {
        isTimeOver = false;
    }

    public float GetDisplayTime()
    {
        return displayTime;
    }
}
