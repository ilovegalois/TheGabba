using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class RoundTimer : MonoBehaviour
{
    [Tooltip("Clock Timer Text")]
    public TextMeshProUGUI digitClock;
    [Tooltip("Lenght of Round in seconds")]
    public float roundTime;

    int mintues;
    int seconds;
    int currentTime;
    // Start is called before the first frame update
    void Start()
    {
        roundTime = DataController.roundTime;
        currentTime = Mathf.FloorToInt(Time.time);
        mintues = Mathf.FloorToInt(roundTime / 60);
        seconds = Mathf.FloorToInt(roundTime % 60);

        digitClock.text = string.Format("{0:00} : {1:00}", mintues, seconds);
    }

    // Update is called once per frame
    void Update()
    {
        dClock();
        if(roundTime == 0)
        {
            DataController.cashAmount += RoundManager.roundCash;
            DataController.methAmount += RoundManager.roundMeth;

            SceneManager.LoadScene("c_SurvivedScene");
        }
    }
    void dClock()
    {
        if (currentTime < Mathf.FloorToInt(Time.time))
        {
            roundTime--;
            currentTime = (int)Time.time;
        }
        mintues = Mathf.FloorToInt(roundTime / 60);
        seconds = Mathf.FloorToInt(roundTime % 60);

        digitClock.text = string.Format("{0:00} : {1:00}", mintues, seconds);
    }
}
