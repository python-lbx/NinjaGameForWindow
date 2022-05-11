using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{
    public void Pass()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        if(currentLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
        {
            PlayerPrefs.SetInt("levelsUnlocked",currentLevel+1);
        }

        Debug.Log("Level"+PlayerPrefs.GetInt("levelsUnlocked")+"Unlocked");
        SceneManager.LoadScene(0);
    }

    public void DeleteDate()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log(PlayerPrefs.GetInt("levelsUnlocked"));
        SceneManager.LoadScene(0);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
