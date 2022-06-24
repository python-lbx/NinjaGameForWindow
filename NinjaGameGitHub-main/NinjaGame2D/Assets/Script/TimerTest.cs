using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerTest : MonoBehaviour
{
    public Text TimerText;
    public float startTime;
    public string minutes;
    public string seconds;

    public float current_t;
    public float last_t;

    public enum Status{Ready,Go,End,GameOver};
    public Status Timer_status;
    BossHealthController bosshp;
    PlayerHealthController playerhp;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        last_t = PlayerPrefs.GetFloat("Last_Timer");

        bosshp = GameObject.FindObjectOfType<BossHealthController>();
        playerhp = GameObject.FindObjectOfType<PlayerHealthController>();

        Timer_status = Status.Ready;
    }

    // Update is called once per frame
    void Update()
    {
        switch(Timer_status)
        {
            case Status.Ready:
            if(bosshp.Health_Current == bosshp.Health_Max)
            {
                Timer_status = Status.Go;
            }
            break;
            case Status.Go:
            if(bosshp.Died)
            {
                Timer_status = Status.End;
            }
            else if(playerhp.Health_Current <= 0)
            {
                Timer_status = Status.GameOver;
            }
            else
            {
                current_t = Time.time - startTime;

                minutes = ((int)current_t / 60).ToString("00");
                seconds = (current_t % 60).ToString("00");

                TimerText.text = minutes + ":" + seconds;
            }
            break;
            case Status.End:
            if(current_t < last_t)
            {
                PlayerPrefs.SetFloat("Last_Timer",current_t);
            }
            break;
            case Status.GameOver:

            break;
        }
    }
}
