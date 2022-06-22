using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_Boss : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D coll;
    Collider2D obj; //沒用
    [SerializeField]Transform meleedetectbox;
    [SerializeField]Vector2 boxsize;
    [SerializeField]LayerMask playerLayer;
    [SerializeField]bool hit;
    [SerializeField]int direction;
    Animator anim;

    [Header("角色面向")]
    public bool faceright;
    [Header("移動速度")]
    public float speed;

    [Header("近戰攻擊間隔")]
    public float meleeAttackCoolDown;
    public float meleeAttackLastTime;

    [Header("衝刺攻擊間隔")]
    public float DashCoolDown;
    public float LastDashTime;
    public float dashSpeed;
    public bool Striking;
    [Header("火球術攻擊間隔")]
    public bool shooting;
    public int shootingTime;
    public float FireballCoolDown;
    public float LastFireballTime;

    [Header("傳送間隔")]
    public float TransfarCoolDown;
    public float LastTransfar;
    public int RanPos;
    public Transform[] TransPoint;
    public Transform AirTransPoint;

    [Header("預置物")]
    public GameObject bullet;
    public Transform shootpoint;
    public GameObject dashwind;

    [Header("記錄玩家位置")]
    public PlayerPosTest Target;

    [Header("移動範圍")]
    public Transform leftPoint;
    public Transform rightPoint;

    [Header("階段")]
    public bool inorethis;
    public enum Status{Idle,Patrol,Spell,DashAttack,FireBall,Strike,trans};
    public Status current_Status;
    public Status Next_Skill_Status;
    



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();

        Target = GameObject.FindObjectOfType<PlayerPosTest>();

    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed",Mathf.Abs(rb.velocity.x) );
        anim.SetBool("Strike",Striking);

        if(Input.GetKeyDown(KeyCode.Q))
        {
            //Instantiate(bullet,shootpoint.transform.position,transform.rotation);
            anim.SetTrigger("Fireshoot");
        }
        //Movement();
        //hit = Physics2D.OverlapBox(new Vector3(meleedetectbox.position.x * direction,meleedetectbox.position.y,meleedetectbox.position.z),boxsize,0,playerLayer);


        Collider2D obj = Physics2D.OverlapBox(meleedetectbox.transform.position,boxsize,0,playerLayer); //顯示你碰撞到什麼需要用到這行代碼
        hit = Physics2D.OverlapBox(meleedetectbox.transform.position,boxsize,0,playerLayer); //判斷你有沒有碰到物件

        
       /* if(Time.time >= (TransfarCoolDown + LastTransfar))
        {
            LastTransfar = Time.time;

            anim.SetTrigger("FirstTrans");
        }
        */
        //FireBallSkill();
    }

    private void FixedUpdate() 
    {
        //DashMeleeAttack();
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = hit? Color.red:Color.green;
        //Gizmos.DrawWireCube(new Vector3(meleedetectbox.position.x * direction,meleedetectbox.position.y,meleedetectbox.position.z),boxsize);
        Gizmos.DrawWireCube(meleedetectbox.transform.position,boxsize);
    }

    /*void firstTrans()
    {
        transform.position = AirTransPoint.position;
    }*/

    void secondTrans()
    {
        if(Next_Skill_Status == Status.Strike)
        {
            RanPos = Random.Range(0,4);
        }
        else if(Next_Skill_Status == Status.FireBall)
        {
            RanPos = Random.Range(0,2);
        }

        for(var i=0;i<4;i++)
        {
            if(i == RanPos)
            {
                transform.position = TransPoint[i].transform.position;
            }
        }
    }

    void SkillAfterTrans()
    {
        if(Next_Skill_Status == Status.FireBall)
        {
            shooting = true;
        }
        else if(Next_Skill_Status == Status.Strike)
        {
            Strike();
        }
    }

    void FireBallshoot() //動畫用到
    {
        Instantiate(bullet,shootpoint.transform.position,transform.rotation);
    }

    void FireBallSkill()
    {   
        if(shooting)
        {
            if(shootingTime == 5)
            {
                shootingTime = 0;
                anim.SetTrigger("FirstTrans");
            }
            else
            {
                if(Time.time >= (FireballCoolDown + LastFireballTime))
                {
                    LastFireballTime = Time.time;
                    anim.SetTrigger("Fireshoot");
                    shootingTime ++;
                }
            }
        }
        if(transform.position == TransPoint[0].transform.position)
        {
            //print("0");
            if(!faceright)
            {
                flip();
            }
        }
        else
        {
            //print("1");
            if(faceright)
            {
                flip();
            }
        }
    }

    void Strike() 
    {
        Striking = true;
        if(Striking)
        {
            dashSpeed = 25f;
            dashwind.SetActive(true);

            if(faceright) //面向右 >>>>
            {
                if(transform.position.x > Target.lastPos) //敵人,我
                {
                    flip();
                    rb.velocity = new Vector2(-dashSpeed,rb.velocity.y);
                }
                else if(transform.position.x < Target.lastPos) //我,敵人
                {
                    rb.velocity = new Vector2(dashSpeed,rb.velocity.y);
                }
            }
            else if(!faceright) //面向左 <<<<
            {
                if(transform.position.x > Target.lastPos) //敵人,我
                {
                    rb.velocity = new Vector2(-dashSpeed,rb.velocity.y);
                }
                else if(transform.position.x < Target.lastPos) //我,敵人
                {
                    flip();
                    rb.velocity = new Vector2(dashSpeed,rb.velocity.y);
                }
            }
        }
        else
        {
            dashSpeed = 0f;
            rb.velocity = new Vector2(0,0);
        }
    }

    void flip()
    {
        faceright = !faceright;
        transform.Rotate(0f,180f,0f);
    }

    
    void Movement()
    {
        if(faceright)
        {
            rb.velocity = new Vector2(speed,rb.velocity.y);
            if(transform.position.x > rightPoint.position.x)
            {
                flip();
            }
        }
        else
        {
            rb.velocity = new Vector2(-speed,rb.velocity.y);
            if(transform.position.x < leftPoint.position.x)
            {
                flip();
            }
        }
    }

    //需要接著coll obj; 碰撞攻擊;
    void meleeAttackDetect()
    {
        if(obj != null && obj.gameObject.name == "Player")
        {   
            //print(obj.gameObject.name);
            if(Time.time >= (meleeAttackLastTime + meleeAttackCoolDown))
            {
                if(faceright)
                {
                    if(transform.position.x > obj.gameObject.transform.position.x)
                    {
                        flip();
                    }
                }
                else
                {
                    if(transform.position.x < obj.gameObject.transform.position.x)
                    {
                        flip();
                    }
                }
                rb.velocity = new Vector2(0,0);
                meleeAttackLastTime = Time.time;
                anim.SetTrigger("MeleeAttack");
            }
        }
        else
        {
            Movement();
        }
    }

    //衝刺攻擊 毋須碰撞
    void DashMeleeAttack()
    {
        if(Mathf.Abs(transform.position.x - Target.lastPos) < 1.5f)
        {
            dashSpeed = 0f;
            rb.velocity = new Vector2(0,0);
            if(Time.time >= (meleeAttackLastTime + meleeAttackCoolDown))
            {
                meleeAttackLastTime = Time.time;
                anim.SetTrigger("MeleeAttack");
            }
        }
        else if(Mathf.Abs(transform.position.x - Target.lastPos) > 1.5f)
        {   
            //(Mathf.Abs(transform.position.x - Target.currentPos));
            dashSpeed = 50f;
            if(Time.time >= (DashCoolDown+LastDashTime))
            {
                LastDashTime = Time.time;
                dashwind.SetActive(true);
                //print("My"+transform.position);
                //print("T"+Target.transform.position);
                /*if(transform.position.x > Target.lastPos) //敵人,我
                {
                    flip();
                    rb.velocity = new Vector2(-dashSpeed,rb.velocity.y);
                }
                else if(transform.position.x < Target.lastPos) //我,敵人
                {
                    flip();
                    rb.velocity = new Vector2(dashSpeed,rb.velocity.y);
                }*/

                if(faceright) //面向右 >>>>
                {
                    if(transform.position.x > Target.lastPos) //敵人,我
                    {
                        flip();
                        rb.velocity = new Vector2(-dashSpeed,rb.velocity.y);
                    }
                    else if(transform.position.x < Target.lastPos) //我,敵人
                    {
                        rb.velocity = new Vector2(dashSpeed,rb.velocity.y);
                    }
                }
                else if(!faceright) //面向左 <<<<
                {
                    if(transform.position.x > Target.lastPos) //敵人,我
                    {
                        rb.velocity = new Vector2(-dashSpeed,rb.velocity.y);
                    }
                    else if(transform.position.x < Target.lastPos) //我,敵人
                    {
                        flip();
                        rb.velocity = new Vector2(dashSpeed,rb.velocity.y);
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        print(other.gameObject.tag);
        if(other.gameObject.tag == "AirWall")
        {
            Striking = false;
        }
    }
}
