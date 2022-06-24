using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial_Boss : MonoBehaviour
{
    Collider2D coll;

    public string BossName;
    public int boss_levelsUnlocked;


    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            boss_levelsUnlocked = PlayerPrefs.GetInt("boss_levelsUnlocked",1);

            SceneManager.LoadScene(BossName);
        }
    }
}
