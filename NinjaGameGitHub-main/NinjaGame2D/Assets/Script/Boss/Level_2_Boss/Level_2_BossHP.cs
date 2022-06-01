using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_2_BossHP : MonoBehaviour
{
    public float Health_Max;
    public float Health_Current;
    public Image HP_Image;
    public Text HP_Text;

    public enum Status {Ready,Go};
    public Status HP_Status;

    public GameObject BossA;
    public GameObject BossB;

    // Start is called before the first frame update
    void Start()
    {        
        HP_Status = Status.Ready;
        Health_Current = 1;
    }

    // Update is called once per frame
    void Update()
    {
        HP_Image.fillAmount = (float)Health_Current/(float)Health_Max;
        HP_Text.text = Health_Current.ToString() + "/" + Health_Max.ToString();

        switch(HP_Status)
        {
            case Status.Ready:
                if(Health_Current <= 100)
                {
                    Health_Current += Time.time/10 ;
                }
                else if(Health_Current >= 100)
                {
                    Health_Current = 100;
                    HP_Status = Status.Go;
                }
            break;
            case Status.Go:
                if(Health_Current <= 0)
                {
                    Destroy(BossA);
                    Destroy(BossB);
                }
            break;
        }
    }
}
