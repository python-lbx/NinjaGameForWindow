using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAClone : MonoBehaviour
{
    BossABehavior bossA;
    BoxCollider2D boxcoll;
    public Vector3 startpos;
    // Start is called before the first frame update
    void Start()
    {   
        bossA = GameObject.Find("Square").GetComponent<BossABehavior>();
        boxcoll = GetComponent<BoxCollider2D>();
        startpos = new Vector3(transform.position.x,5,transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetPos()
    {
        transform.position = startpos;
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Ground")
        {
            ResetPos();
        }
    }
}
