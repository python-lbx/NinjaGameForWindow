using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    public KeyCode left{get;set;}
    public KeyCode right{get;set;}
    public KeyCode crouch{get;set;}
    public KeyCode jump{get;set;}
    public KeyCode attack{get;set;}
    public KeyCode fireball{get;set;}
    public KeyCode dash{get;set;}
    public KeyCode shoot{get;set;}
    public KeyCode escape{get;set;}

    void Awake() 
    {
        //唯一
        if(GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else if(GM != this)
        {
            Destroy(gameObject);
        }

        //按鍵預存
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey","A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey","D"));
        crouch = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("crouchKey","S"));
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey","Space"));
        attack = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("attackKey","Z"));
        fireball = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("fireballKey","F"));
        dash = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("dashKey","C"));
        shoot = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("shootKey","X"));
        escape = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("escapeKey","Escape"));

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
