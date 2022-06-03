using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAClone : MonoBehaviour
{
    public BossABehavior bossA;
    BoxCollider2D boxcoll;
    Animator anim;
    PlayerHealthController playerHealth;
    public Vector3 startpos;

    public int damage;

    public float collPlayer_CDTime;
    public float collPlayer_LastCDTime;
    // Start is called before the first frame update
    void Awake() 
    {
        bossA = GameObject.Find("BossA").GetComponent<BossABehavior>();
        anim = GetComponent<Animator>();
        boxcoll = GetComponent<BoxCollider2D>();
        playerHealth = GameObject.FindObjectOfType<PlayerHealthController>();
        startpos = new Vector3(transform.position.x,5,transform.position.z);

        if(bossA.IsEyeOpen == 0)
        {
            anim.SetBool("IsEyeOpen",true);
        }
        else if(bossA.IsEyeOpen == 1)
        {
            anim.SetBool("IsEyeOpen",false);
        }    
    }
    void Start()
    {   

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= (collPlayer_LastCDTime + collPlayer_CDTime))
        {
            gameObject.layer = LayerMask.NameToLayer("BossItem");
        }

        if(bossA.IsEyeOpen == 0)
        {
            anim.SetBool("IsEyeOpen",true);
        }
        else if(bossA.IsEyeOpen == 1)
        {
            anim.SetBool("IsEyeOpen",false);
        }
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
        else if(other.gameObject.tag == "Player")
        {
            gameObject.layer = LayerMask.NameToLayer("BossUnCollable");
            playerHealth.Health_Current -= damage;
            collPlayer_LastCDTime = Time.time;
            
        }
    }
}
