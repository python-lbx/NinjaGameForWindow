using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossABehavior : MonoBehaviour
{
    BoxCollider2D boxcoll;
    Rigidbody2D rb;
    PlayerHealthController playerHealth;

    public Animator anim;

    public BossBbehaviour BossB;

    public Transform transformPoint;
    public int damage;

    [Header("克隆體")]
    //public GameObject BossAClone;
    public GameObject[] Clones;
    public int RangePos;
    public int i;

    public float timer;
    public float timerstart;
    public int time;

    public int IsEyeOpen;

    [Header("巡邏")]
    public float speed;
    public float startWaitTime;
    public float WaitTime;

    public Transform movePos;
    public Transform leftDownPos;
    public Transform rightUpPos;
    
    [Header("圓運動")]
    public Transform Center;


    [Header("階段")]
    public bool phase;
    public float phaseTime;
    public float StartphaseTime;
    public enum Status {Prepare,patrol,Fall,CircleMove,Transform,Transform_I,Transform_II,Death};
    public Status BossA_Status;
    
    // Start is called before the first frame update
    void Start()
    {
        boxcoll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        BossB = GameObject.Find("BossB").GetComponent<BossBbehaviour>();
        playerHealth = GameObject.FindObjectOfType<PlayerHealthController>();
        
        //隨機點與等待時間重置
        movePos.position =  GetRandomPos();
        WaitTime = startWaitTime;
        //靜止
        //phaseTime = 5; 
        //BossA_Status = Status.Idle;
    }

    // Update is called once per frame
    void Update()
    {            
        var bossHP = GameObject.FindObjectOfType<BossHealthController>();

        anim.SetInteger("IsEyeOpen",IsEyeOpen);
    
        switch (BossA_Status)
        {
            case Status.Prepare:
            if(bossHP.Health_Current == bossHP.Health_Max)
            {
                gameObject.layer = LayerMask.NameToLayer("BossUnCollable");

                phaseTime = 15; //巡邏階段時間
                BossA_Status = Status.patrol;
                BossB.BossB_Status = BossBbehaviour.Status.patrol;
                BossB.anim.SetBool("BattleStart",true);
                IsEyeOpen = 2;
            }
            else
            {   
                gameObject.layer = LayerMask.NameToLayer("BossInvincible");
            }
            break;

            case Status.Transform: //什麼也不做
                transform.position = transformPoint.position; //時間內固定在傳送點
                IsEyeOpen = 2;
            break;

            case Status.Transform_I:
                if(phaseTime > 0) //2秒後墜落
                {
                    rb.velocity = new Vector2(0,0);
                    transform.position = transformPoint.position; //時間內固定在傳送點
                    phaseTime -= Time.deltaTime;
                }
                else if(phaseTime <= 0)
                {                    
                    ChangePos();
                    BossA_Status = Status.Fall;
                }
            break;

            case Status.patrol:
            damage = 2;
            gameObject.layer = LayerMask.NameToLayer("BossCollable");
            if(phaseTime>0)
            {
                RndPatrol();
                phaseTime -= Time.deltaTime;
            }
            else if(phaseTime<=0)
            {
                //重置數值
                WaitTime = startWaitTime;
                phaseTime = 2;
                //狀態切換
                //BossA_Status = Status.Transform_I;
                anim.SetTrigger("Trans");
                BossB.anim.SetTrigger("Trans");
            }
            break;

            case Status.Fall:
            gameObject.layer = LayerMask.NameToLayer("BossUnCollable");
            transform.localScale = new Vector3(2,2,1);//變大
            if(time == 4)
            {
                //重置數值
                time = 0;
                phaseTime = 15;
                CancelInvoke("ChangePos");
                Invoke("trans",2f); //2秒後傳送待機
            }
            break;

            case Status.CircleMove:
            damage = 2;
            gameObject.layer = LayerMask.NameToLayer("BossCollable");
            rb.velocity = new Vector2(0,0);
            transform.position = Center.position;
            rb.gravityScale = 0;
            break;

            case Status.Death:
            rb.velocity = new Vector2(0,0);
            rb.gravityScale = 1;
            break;
        }

        if(playerHealth.Health_Current <= 0)
        {
            rb.velocity = new Vector2(0,0);
            this.enabled = false;
        }

        if(bossHP.Died)
        {   
            transform.position = movePos.position;
            BossA_Status = Status.Death;
        }
    }

    void ChangePos()
    {
        rb.gravityScale = 1;
        RangePos = Random.Range(0,5);
        IsEyeOpen = Random.Range(0,2);

        for(i=0;i<5;i++)
        {
            if(i == RangePos)
            {
                transform.position = Clones[i].transform.position;
                Clones[i].SetActive(false);
            }
            else
            {
                Clones[i].SetActive(true);
            }
        }
    }

    void RndPatrol()
    {
        rb.gravityScale = 0;
        transform.position = Vector2.MoveTowards(transform.position,movePos.position,speed * Time.deltaTime);

        if(Vector2.Distance(transform.position,movePos.position) < 0.1f)
        {
            if(WaitTime <= 0)
            {
                movePos.position =  GetRandomPos();
                WaitTime = startWaitTime;
            }
            else
            {
                WaitTime -= Time.deltaTime;
            }
        }
    }

    void trans()
    {
        transform.localScale = new Vector3(1,1,1); //變小
        BossA_Status = Status.Transform;
        BossB.phaseTime = 2f; //2秒後橫衝
        BossB.BossB_Status = BossBbehaviour.Status.HRush;
        BossB.transform.position = BossB.point[BossB.BossBRndPos].transform.position;
        CancelInvoke("trans");
    }

    void firsttrans()
    {
        BossA_Status = Status.Transform_I;
        IsEyeOpen = 0;
    }

    Vector2 GetRandomPos()
    {
        Vector2 rndPos = new Vector2(Random.Range(leftDownPos.position.x,rightUpPos.position.x),Random.Range(leftDownPos.position.y,rightUpPos.position.y));
        return rndPos;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Ground")
        {
            if(BossA_Status == Status.Fall && time < 4)
            {
                Invoke("ChangePos",2f);
                time++;
            }
        }

        else if(other.gameObject.tag == "Player")
        {
            playerHealth.Health_Current -= damage;
        }
    }
}
