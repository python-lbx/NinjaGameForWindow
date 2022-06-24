using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour
{
    public float Health_Max;
    public float Health_Current;
    
    public Image HP_Image;
    public Text HP_Text;
    public bool Died;

    public float rate;

    public GameObject door;

    public enum Status {Ready,Go};
    public Status HP_Status;

    private void Awake() 
    {
        HP_Status = Status.Ready;
        Health_Current = 1;    
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HP_Image.fillAmount = (float)Health_Current/(float)Health_Max;
        HP_Text.text = Health_Current.ToString() + "/" + Health_Max.ToString();

        switch(HP_Status)
        {
            case Status.Ready:
                if(Health_Current <= Health_Max)
                {
                    Health_Current += Time.time/rate ;
                }
                else if(Health_Current >= Health_Max)
                {
                    Health_Current = Health_Max;
                    HP_Status = Status.Go;
                }
            break;
            case Status.Go:
                if(Health_Current <= 0)
                {
                    Died = true;
                    Health_Current = 0;
                    door.SetActive(true);
                }
            break;
        }
    }
}
