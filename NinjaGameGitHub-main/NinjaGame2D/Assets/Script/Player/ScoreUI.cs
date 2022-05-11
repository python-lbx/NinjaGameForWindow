using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Image Score_Imgae;
    public Text Score_Text;
    public int Score_Max;
    public int Score_Current; //目前分數

    TranslateDoor DoorRecord;

    void Start()
    {
        DoorRecord = GameObject.FindObjectOfType<TranslateDoor>();
    }
    // Update is called once per frame
    void Update()
    {   
        Score_Current = DoorRecord.New_LevelScore;
        
        if(Score_Current >= 100)
        {
            Score_Current = 100;
        }
        Score_Imgae.fillAmount = (float)Score_Current / (float)Score_Max;
        Score_Text.text = Score_Current.ToString() +"/" + Score_Max.ToString();
    }
}
