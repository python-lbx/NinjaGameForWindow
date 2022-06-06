using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    public GameObject dashobj;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            rb.AddForce(transform.right,ForceMode2D.Impulse);
            anim.SetTrigger("dash");
            dashobj.SetActive(true);
        }
    }
}
