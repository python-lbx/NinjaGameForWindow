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

    public bool faceright;
    public float speed;

    public float meleeAttackCoolDown;
    public float meleeAttackLastTime;

    public float DashCoolDown;
    public float LastDashTime;
    public float dashSpeed;
    public bool Striking;

    public float TransfarCoolDown;
    public float LastTransfar;
    public int RanPos;
    public Transform[] TransPoint;
    public Transform AirTransPoint;

    public GameObject bullet;
    public Transform shootpoint;

    public GameObject dashwind;

    public PlayerPosTest Target;

    [Header("移動範圍")]
    public Transform leftPoint;
    public Transform rightPoint;



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
            Instantiate(bullet,shootpoint.transform.position,transform.rotation);
        }
        //Movement();
        //hit = Physics2D.OverlapBox(new Vector3(meleedetectbox.position.x * direction,meleedetectbox.position.y,meleedetectbox.position.z),boxsize,0,playerLayer);


        Collider2D obj = Physics2D.OverlapBox(meleedetectbox.transform.position,boxsize,0,playerLayer); //顯示你碰撞到什麼需要用到這行代碼
        hit = Physics2D.OverlapBox(meleedetectbox.transform.position,boxsize,0,playerLayer); //判斷你有沒有碰到物件

        if(Time.time >= (TransfarCoolDown + LastTransfar))
        {
            LastTransfar = Time.time;
            RanPos = Random.Range(0,4);
            anim.SetTrigger("FirstTrans");
        }
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

    void firstTrans()
    {
        transform.position = AirTransPoint.position;
    }

    void secondTrans()
    {
        for(var i=0;i<4;i++)
        {
            if(i == RanPos)
            {
                transform.position = TransPoint[i].transform.position;
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
