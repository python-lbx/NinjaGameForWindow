using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public string SceneName;
    public GameObject Panel;

    public bool Escape;

    public PlayerHealthController playerhp;
    // Start is called before the first frame update
    void Start()
    {
        playerhp = FindObjectOfType<PlayerHealthController>();

        if(SceneManager.GetActiveScene().buildIndex == 5)
        {
            FindObjectOfType<AVmanager>().Play("TutorialBGM");
        }
        else if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            FindObjectOfType<AVmanager>().Play("Boss_One_BGM");
        }
        else if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            FindObjectOfType<AVmanager>().Play("Boss_Two_BGM");
        }
        else if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            FindObjectOfType<AVmanager>().Play("Boss_Three_BGM");
        }


    }

    // Update is called once per frame
    void Update()
    {
        if(playerhp.Health_Current <=0 )
        {
            Panel.SetActive(true);
            Time.timeScale = 0;
        }
        else if(playerhp.Health_Current > 0)
        {
            Panel.SetActive(Escape);

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Escape = !Escape;
            }

            if(Escape)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }       
        }
    }
    

    public void BTM()
    {
        SceneManager.LoadScene(SceneName);

        FindObjectOfType<AVmanager>().Stop("TutorialBGM");
        FindObjectOfType<AVmanager>().Stop("Boss_One_BGM");
        FindObjectOfType<AVmanager>().Stop("Boss_Two_BGM");
        FindObjectOfType<AVmanager>().Stop("Boss_Three_BGM");
    }

    public void QTG()
    {
        Application.Quit();
    }

}
