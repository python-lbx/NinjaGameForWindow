using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour
{
    public int Health_Max;
    public int Health_Current;
    
    public Image HP_Image;
    public Text HP_Text;
    
    // Start is called before the first frame update
    void Start()
    {
        Health_Current = Health_Max;
    }

    // Update is called once per frame
    void Update()
    {
        HP_Image.fillAmount = (float)Health_Current/(float)Health_Max;
        HP_Text.text = Health_Current.ToString() + "/" + Health_Max.ToString(); 

        if(Health_Current <= 0)
        {
            Health_Current = 0;
            FindObjectOfType<BossBehaviour>().BossStatus = BossBehaviour.Status.Death;
        }
    }
}
