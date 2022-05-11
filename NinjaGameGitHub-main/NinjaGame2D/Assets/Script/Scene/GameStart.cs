using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Ready()
    {
        Time.timeScale = 0;
    }

    public void GO()
    {
        Time.timeScale = 1;
    }
}
