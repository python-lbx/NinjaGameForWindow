using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer_Score : MonoBehaviour
{
    public Text TimerText;
    public float startTime;
    public string minutes;
    public string seconds;

    public float new_t;
    public float old_t;

    public enum Status{Ready,Go,End};
    public Status Timer_status;
    BossHealthController bosshp;
    PlayerHealthController playerhp;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;

        bosshp = GameObject.FindObjectOfType<BossHealthController>();
        playerhp = GameObject.FindObjectOfType<PlayerHealthController>();
        
        Timer_status = Status.Go;

        old_t = PlayerPrefs.GetFloat("old_Timer",old_t);
        new_t = PlayerPrefs.GetFloat("new_Timer",new_t);
    }

    // Update is called once per frame
    void Update()
    {   
        switch(Timer_status)
        {
            case Status.Ready:

            break;
            case Status.Go:
            if(bosshp.Died)
            {
                Timer_status = Status.End;
            }
            else
            {
                new_t = Time.time - startTime;

                minutes = ((int)new_t / 60).ToString("00");
                seconds = (new_t % 60).ToString("00");

                TimerText.text = minutes + ":" + seconds;
            }
            break;
            case Status.End:
            if(new_t < old_t)
            {
                PlayerPrefs.SetFloat("Last_Timer",new_t);
            }
            else
            {
                PlayerPrefs.SetFloat("Last_Timer",old_t);
            }
            break;
        }
    }
}
