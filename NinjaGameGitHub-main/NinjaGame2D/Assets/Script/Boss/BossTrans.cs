using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossTrans : MonoBehaviour
{
    Collider2D coll;

    public string BossName;
    public int currentBossLevel;
    public int nextBossLevel;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        currentBossLevel = PlayerPrefs.GetInt("boss_levelsUnlocked");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            if(nextBossLevel > currentBossLevel)
            {
                PlayerPrefs.SetInt("boss_levelsUnlocked",nextBossLevel);
            }

            SceneManager.LoadScene(BossName);
        }
    }
}
