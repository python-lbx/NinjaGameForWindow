using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DeleteDate : MonoBehaviour
{
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showPanel()
    {
        panel.SetActive(true);
    }

    public void UnshowPanel()
    {
        panel.SetActive(false);
    }

    public void Del()
    {
        PlayerPrefs.DeleteAll();
    }
}
