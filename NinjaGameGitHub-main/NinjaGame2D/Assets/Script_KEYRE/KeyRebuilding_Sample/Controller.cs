using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //按鍵輸出

        if(Input.GetKey(GameManager.GM.left))
        {
            transform.position += Vector3.left * Time.deltaTime;
        }
        if(Input.GetKey(GameManager.GM.right))
        {
            transform.position += Vector3.right * Time.deltaTime;
        }
        if(Input.GetKey(GameManager.GM.jump))
        {
            transform.position += Vector3.up * Time.deltaTime;
        }
    }
}
