using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int levelsUnlocked;
    public int boss_levelsUnlocked;
    public Button[] buttons;
    public Button[] boss_buttons;

    // Start is called before the first frame update
    void Start()
    {
        //普通關卡
        //levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked",1);
        boss_levelsUnlocked = PlayerPrefs.GetInt("boss_levelsUnlocked");

        /*
            for(int i=0; i<buttons.Length;i++)
            {
                buttons[i].interactable = false;
            }

            for(int i=0; i<levelsUnlocked;i++)
            {
                buttons[i].interactable = true;
            }
        */

        //首領戰
        for(int i=0;i<boss_buttons.Length;i++)
        {
            boss_buttons[i].interactable = false;
        }

        for(int i=0;i<boss_levelsUnlocked;i++)
        {
            boss_buttons[i].interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void DeleteAllDate()
    {
        PlayerPrefs.DeleteAll();
    }
}
