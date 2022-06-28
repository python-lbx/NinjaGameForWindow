using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject levelpackage;
    public GameObject settingpackage;
    
    // Start is called before the first frame update
    void Start()
    {
        levelpackage.SetActive(true);
        settingpackage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(settingpackage.activeSelf)
        {
            levelpackage.SetActive(false);
        }
        else
        {
            levelpackage.SetActive(true);
        }
    }

    void openSetting()
    {
        settingpackage.SetActive(true);
    }

    void closeSetting()
    {
        settingpackage.SetActive(false);
    }
}
