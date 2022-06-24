using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer_Score : MonoBehaviour
{
    public Text TimerText;
    public float startTime;
    public string minutes;
    public string seconds;

    public float new_t;
    public float old_t;

    public enum Status{Ready,Go,End,GameOver};
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

        //old_t = PlayerPrefs.GetFloat("BossOne");

        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            old_t = PlayerPrefs.GetInt("BossOne",100000);
        }
        else if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            old_t = PlayerPrefs.GetInt("BossTwo",100000);
        }
        else if(SceneManager.GetActiveScene().buildIndex ==3)
        {
            old_t = PlayerPrefs.GetInt("BossThree",1000000);
        }
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
            else if(playerhp.Health_Current <= 0)
            {
                Timer_status = Status.GameOver;
            }
            else
            {
                new_t = Time.time - startTime;

                minutes = ((int)new_t / 60).ToString("00");
                seconds = (new_t % 60).ToString("00");

                TimerText.text = "''" +minutes + ":" + seconds + "''";
            }
            break;
            case Status.End:
            if(new_t < old_t)
            {
                //PlayerPrefs.SetFloat("Level_Time",new_t);
                if(SceneManager.GetActiveScene().buildIndex == 1)
                {
                    PlayerPrefs.SetFloat("BossOne",new_t);
                }
                else if(SceneManager.GetActiveScene().buildIndex == 2)
                {
                    PlayerPrefs.SetFloat("BossTwo",new_t);
                }
                else if(SceneManager.GetActiveScene().buildIndex == 3)
                {
                    PlayerPrefs.SetFloat("BossThree",new_t);
                }
            }
            break;
            case Status.GameOver:
            break;
        }
    }
}
