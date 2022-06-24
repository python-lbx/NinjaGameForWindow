using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public string SceneName;
    public GameObject Panel;

    public bool Escape;

    PlayerHealthController playerhp;
    // Start is called before the first frame update
    void Start()
    {
        playerhp = FindObjectOfType<PlayerHealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerhp.Health_Current <=0 )
        {
            Panel.SetActive(true);
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
    }

}
