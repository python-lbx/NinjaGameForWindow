using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int Score_Max;

    [Header("LevelOneScore")]
    public Image LV1_Score_Image;
    public Text LV1_Score_Text;
    public int LV1_Score;

    [Header("LevelTwoScore")]
    public Image LV2_Score_Image;
    public Text LV2_Score_Text;
    public int LV2_Score;


    // Start is called before the first frame update
    void Start()
    {
        //LV1
        LV1_Score = PlayerPrefs.GetInt("LV1_Score");
        LV1_Score_Image.fillAmount = (float)LV1_Score/(float)Score_Max;
        LV1_Score_Text.text = LV1_Score.ToString()+"/"+Score_Max.ToString();

        //LV2
        LV2_Score = PlayerPrefs.GetInt("LV2_Score");
        LV2_Score_Image.fillAmount = (float)LV2_Score/(float)Score_Max;
        LV2_Score_Text.text = LV2_Score.ToString()+"/"+Score_Max.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        LV1_Score_Image.fillAmount = (float)LV1_Score/(float)Score_Max;
        LV1_Score_Text.text = LV1_Score.ToString()+"/"+Score_Max.ToString();

        LV2_Score_Image.fillAmount = (float)LV2_Score/(float)Score_Max;
        LV2_Score_Text.text = LV2_Score.ToString()+"/"+Score_Max.ToString();
    }
}
