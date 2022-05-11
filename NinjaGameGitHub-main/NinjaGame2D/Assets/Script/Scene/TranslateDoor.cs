using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TranslateDoor : MonoBehaviour
{
    Collider2D coll;
    [Header("頭目戰")]
    public bool NextLevelIsBoss;
    public string BossName;
    public int currentLevel;
    public int nextLevel;

    public int Old_LevelScore;
    public int New_LevelScore;

    [Header("小怪戰")]
    public bool NextLevelIsEnemy;
    public string LevelName;
    public int currentBossLevel;
    public int nextBossLevel;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        currentLevel = PlayerPrefs.GetInt("levelsUnlocked");

        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            Old_LevelScore = PlayerPrefs.GetInt("LV1_Score");
        }
        else if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            Old_LevelScore = PlayerPrefs.GetInt("LV2_Score");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D Door) 
    {
        if(Door.gameObject.tag == "Player")
        {
            if(NextLevelIsBoss)
            {
                //關卡
                if(nextBossLevel > currentBossLevel)
                {
                    PlayerPrefs.SetInt("boss_levelsUnlocked",nextBossLevel);
                }

                //分數
                if(New_LevelScore>Old_LevelScore)
                {
                    if(SceneManager.GetActiveScene().buildIndex == 1)
                    {
                        PlayerPrefs.SetInt("LV1_Score",New_LevelScore);
                    }
                    else if(SceneManager.GetActiveScene().buildIndex == 2)
                    {
                        PlayerPrefs.SetInt("LV2_Score",New_LevelScore);
                    }
                }

                Debug.Log("BossLevel"+PlayerPrefs.GetInt("boss_levelsUnlocked")+"Unlocked");
                SceneManager.LoadScene(BossName);
            }
            
            if(NextLevelIsEnemy)
            {

                if(nextLevel > currentLevel )
                {
                    PlayerPrefs.SetInt("levelsUnlocked",nextLevel);
                }
            
                Debug.Log("Level"+PlayerPrefs.GetInt("levelsUnlocked")+"Unlocked");
                SceneManager.LoadScene(LevelName);
            }
        }
    }
}
