using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Transform menuPanel;
    public GameObject levelpackage;
    public GameObject settingpackage;

    public Slider Audioslider;

    
    // Start is called before the first frame update
    void Awake()
    {
        //必須
        Audioslider.value =  PlayerPrefs.GetFloat("Audio");
    }
    void Start()
    {
        FindObjectOfType<AVmanager>().Play("LevelMenuBGM");
        FindObjectOfType<AVmanager>().Totalvolume = Audioslider.value; //顯示變化


        levelpackage.SetActive(true);
        settingpackage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        FindObjectOfType<AVmanager>().Totalvolume = Audioslider.value; //顯示變化

        if(settingpackage.activeSelf)
        {
            levelpackage.SetActive(false);
        }
        else
        {
            levelpackage.SetActive(true);
        }

               //開啟菜單
        if(Input.GetKeyDown(GameManager.GM.escape))
        {
            if(menuPanel.gameObject.activeSelf)
            {
                menuPanel.gameObject.SetActive(false);
            }
            else
            {
                menuPanel.gameObject.SetActive(true);
            }
        }
        
    }

    public void openSetting()
    {
        settingpackage.SetActive(true);
    }

    public void closeSetting()
    {        
        settingpackage.SetActive(false);
    }
}
