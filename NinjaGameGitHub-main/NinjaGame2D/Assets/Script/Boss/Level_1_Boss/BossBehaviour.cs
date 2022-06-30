using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public Rigidbody2D rb;
    Animator anim;

    Collider2D coll;

    [Header("移動範圍")]
    public Transform leftPoint;
    public Transform rightPoint;

    [Header("炸藥")]
    public GameObject bombup;
    public GameObject bomodown;

    public Transform shootpoint;
    public GameObject bullet;
    public int shoottime;

    [Header("狀態數值")]
    public GameObject player;
    public bool IsOnGround;
    public bool faceright;
    public float speed;
    public float jumpForce;
    [Header("階段")]
    //技能時間
    public float Stimer;
    public float timer;
    //技能次數
    public int skilltime;
    //階段時間
    public float phaseTime;
    public float StartphaseTime;
    public enum  Status {Prepare,Patrol,Shoot,Dash,Jump,Death}
    public Status BossStatus;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();

        player = GameObject.Find("Player");

        //初始值
        faceright = false;
        StartphaseTime = 5;
        shoottime = 3;
        speed = 5f;
        jumpForce = 6f;
        skilltime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var bossHP = GameObject.FindObjectOfType<BossHealthController>();
        animController();

        switch(BossStatus)
        {   
            case Status.Prepare:
            if(bossHP.Health_Current == bossHP.Health_Max)
            {   
                anim.SetBool("Go",true);
                gameObject.layer = LayerMask.NameToLayer("BossUnCollable");
                BossStatus = Status.Patrol;
            }
            else
            {
                gameObject.layer = LayerMask.NameToLayer("BossInvincible");
            }
            break;
            case Status.Patrol:
            if(StartphaseTime > 0)
            {
                StartphaseTime -= Time.deltaTime;
                Movement();
            }
            else if(StartphaseTime <= 0)
            {  
                rb.velocity = new Vector2(0,0);
                BossStatus = Status.Shoot;
            }
            break;

            case Status.Shoot:
            if(skilltime == 3) //射擊次數達3次
            {
                timer = 1.4f; //丟炸彈時間
                Stimer = timer;
                StartphaseTime = 8.5f; //衝刺時間
                skilltime = 0; //重置
                BossStatus = Status.Dash;

                FindObjectOfType<AVmanager>().Play("BossOneSkill");
            }
            else
            {
                Shoot();
            }
            break;

            case Status.Dash:
            if(StartphaseTime > 0)
            {
                StartphaseTime -= Time.deltaTime;
                speed = 8;
                Movement();
                BombUp();
            }
            else if(StartphaseTime <= 0)
            {
                FindObjectOfType<AVmanager>().Play("BossOneSkill");

                timer = 1.3f;
                Stimer = timer;
                if(Vector2.Distance(transform.position,leftPoint.position)<0.1f)
                {
                    faceright = true;
                    transform.Rotate(0f,180f,0);
                    BossStatus = Status.Jump;

                }
                else if(Vector2.Distance(transform.position,rightPoint.position)<0.1f)
                {
                    transform.Rotate(0f,180f,0);
                    faceright = false;
                    BossStatus = Status.Jump;
                }
            }
            break;

            case Status.Jump:
            if(skilltime == 4)
            {   
                //初始值
                timer = 1.4f;
                Stimer = timer;
                jumpForce =6f;
                speed = 5f;
                skilltime = 0;
                StartphaseTime = 5f;
                BossStatus = Status.Patrol;
            }
            else
            {
                jumpForce = 11f;
                speed = 11f;
                BombDown();
                Jump();
            }
            break;

            case Status.Death:
            coll.enabled = false;
            anim.SetTrigger("Death");
            break;
        }

        if(FindObjectOfType<BossHealthController>().Died)
        {
            BossStatus = Status.Death;
        }

        if(FindObjectOfType<PlayerHealthController>().Health_Current <= 0)
        {   
            rb.velocity = new Vector2(0,0);
            anim.SetBool("GameOver",true);
            this.enabled = false;
        }
    }

    void BombUp()
    {
        if(Stimer >= 0)
        {
            Stimer -= Time.deltaTime;
        }
        else if(Stimer <=0)
        {
            Instantiate(bombup,transform.position,Quaternion.identity);
            Stimer = timer;
        }
    }
    void BombDown()
    {
        if(Stimer >= 0)
        {
            Stimer -= Time.deltaTime;
        }
        else if(Stimer <=0)
        {
            Instantiate(bomodown,transform.position,Quaternion.identity);
            Stimer = timer;
        }
    }

    void Shoot()
    {
        if(faceright)
        {
            if(player.transform.position.x < transform.position.x)
            {
                transform.Rotate(0f,180f,0);
                faceright = false;
            }
            }
            else
            {
            if(player.transform.position.x > transform.position.x)
            {
                transform.Rotate(0f,180f,0);
                faceright = true;
            }
        }

        if(shoottime == 3 && IsOnGround)
        {
            Instantiate(bullet,shootpoint.transform.position,transform.rotation);
            shoottime --;
            rb.velocity = new Vector2(0,jumpForce);
        }
        else if(shoottime == 2 && !IsOnGround && rb.velocity.y<0.1)
        {
            Instantiate(bullet,shootpoint.transform.position,transform.rotation);
            shoottime --;
        }
        else if(shoottime == 1 && IsOnGround)
        {
            Instantiate(bullet,shootpoint.transform.position,transform.rotation);
            shoottime --;
        }
        else if(shoottime == 0 && IsOnGround)
        {
            skilltime ++; //射擊次數
            shoottime = 3; //子彈
            StartphaseTime = 5; //巡邏時長
            BossStatus = Status.Patrol;
        }
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

    void flip()
    {
        faceright = !faceright;
        transform.Rotate(0f,180f,0f);
    }
    
    void Jump()
    {
        if(IsOnGround)
        {
            if(faceright)
            {
                rb.velocity = new Vector2(speed,jumpForce);
            }
            else
            {
                rb.velocity = new Vector2(-speed,jumpForce);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.name == "Ground" && BossStatus == Status.Jump)
        {
            skilltime ++;
            flip();
            IsOnGround = true;
        }
        else if(other.gameObject.name == "Ground")
        {
            IsOnGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.gameObject.name == "Ground")
        {
            IsOnGround = false;
        }
    }

    void animController()
    {
        if(rb.velocity.y > 0 && !IsOnGround)
        {
            anim.SetBool("Jump",true);
        }
        else if(rb.velocity.y < 0)
        {
            anim.SetBool("Jump",false);
            anim.SetBool("Fall",true);
        }
        else if(IsOnGround)
        {
            anim.SetBool("Fall",false);
        }
    }

    public void DeathAnim()
    {
        rb.velocity = new Vector2(0,5f);
        Destroy(this.gameObject,3f);
    }
}
