using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Transform menuPanel;
    public GameObject levelpackage;
    public GameObject settingpackage;
    
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AVmanager>().Play("LevelMenuBGM");

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
