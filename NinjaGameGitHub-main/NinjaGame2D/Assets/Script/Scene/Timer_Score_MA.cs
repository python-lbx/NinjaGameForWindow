using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer_Score_MA : MonoBehaviour
{
    public string[] minutes;
    public string[] seconds;

    public Text[] bosstext;
    public float[] bosstimer;

    // Start is called before the first frame update
    void Start()
    {
        //Boss_One_Timer = PlayerPrefs.GetFloat("BossOne");

        //minutes = (Boss_One_Timer / 60).ToString("00");
        //seconds = (Boss_One_Timer % 60).ToString("00");

        //Boss_One_Timer_Text.text = "''"+ minutes + ":" + seconds + "''";

        bosstimer[0] = PlayerPrefs.GetFloat("BossOne");
        bosstimer[1] = PlayerPrefs.GetFloat("BossTwo");
        bosstimer[2] = PlayerPrefs.GetFloat("BossThree");

        for(var i = 0 ; i < bosstimer.Length ; i++)
        {
            minutes[i] = (bosstimer[i] / 60).ToString("00");
            seconds[i] = (bosstimer[i] % 60).ToString("00");

            bosstext[i].text = "''" + minutes[i] + ":" + seconds[i] + "''";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
