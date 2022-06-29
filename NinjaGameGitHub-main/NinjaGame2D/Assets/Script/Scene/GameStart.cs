using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public string levelname;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AVmanager>().Play("StartMenuBGM");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gamestart()
    {
        FindObjectOfType<AVmanager>().Stop("StartMenuBGM");
        SceneManager.LoadScene(levelname);
    }

    public void QTG()
    {
        Application.Quit();
    }
}
