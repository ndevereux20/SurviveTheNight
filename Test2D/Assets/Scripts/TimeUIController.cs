using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeUIController : MonoBehaviour
{
    public Text hours;
    public Text minutes;
    public Text AmPm;
    private float secondTimer; 
    private int minuteCount = 0; 
    private int hourCount = 21;

    public bool youWin;

    public static TimeUIController instance; 

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        hours.text = "0" + (hourCount - 12) + ":";
        AmPm.text = "pm";
    }

    // Update is called once per frame
    void Update()
    {
        if(PauseMenu.instance.gameIsPaused == false)
        {
            clock();
        }
        else
        {

        }
    }
    void clock()
    {
        secondTimer += Time.deltaTime * 240;
        if (secondTimer >= 60)
        {
            minuteCount++;
            if (minuteCount < 10)
            {
                minutes.text = "0" + minuteCount;
            }
            else if (minuteCount >= 10 && minuteCount < 60)
            {
                minutes.text = "" + minuteCount;
            }
            else if (minuteCount == 60)
            {
                minuteCount = 0;
                minutes.text = "0" + minuteCount;

                hourCount++;
                if (hourCount < 6)
                {
                    hours.text = "0" + hourCount + ":";
                    AmPm.text = "am";
                }
                if (hourCount == 6)
                {
                    youWin = true;
                    SceneManager.LoadScene("EndGame");
                }
                else if (hourCount >= 22 && hourCount < 24)
                {
                    hours.text = "" + (hourCount - 12) + ":";
                    AmPm.text = "pm";
                }
                else if (hourCount == 24)
                {
                    hours.text = "" + (hourCount - 12) + ":";
                    AmPm.text = "am";
                    hourCount = 0;
                }
                minuteCount = 0;
            }
            secondTimer = 0;
        }
    }
}
