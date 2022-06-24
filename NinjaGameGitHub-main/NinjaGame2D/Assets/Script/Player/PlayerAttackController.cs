using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    PlayerMovementController PlayerMovement;
    PlayerHealthController playerHealth;
    public GameObject Z_Attack_Box;

    public GameObject FireBall;
    public Transform ShootPoint;

    [Header("普通攻擊")]
    public float Z_Attack_CD;
    public float Z_Last_Time; //上次攻擊時間

    [Header("火球術")]
    public float Fire_CD;//冷卻時間
    public float Fire_Last_Time;//上次火球時間

    [Header("衝刺")]
    public float dashTime;//dash時長
    private float dashTimeLeft; //dash剩余時間
    public float LastDash = -10f; //上一次dash時間點
    public float dashCoolDown;
    public float dashSpeed;
    public bool Dashing;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        PlayerMovement = GetComponent<PlayerMovementController>();
        playerHealth = GetComponent<PlayerHealthController>();        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) && !PlayerMovement.isCrouch && !playerHealth.isHurt)
        {
            if(Time.time >= (Z_Attack_CD + Z_Last_Time))
            {
                if(PlayerMovement.isOnGround)
                {
                    Z_Last_Time = Time.time;
                    anim.SetTrigger("IsAttack");
                }
                else if(!PlayerMovement.isOnGround)
                {
                    Z_Last_Time = Time.time;
                    anim.SetTrigger("IsAttack");
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.F) && !PlayerMovement.isCrouch && !playerHealth.isHurt) //當按下F鍵
        {
            if(Time.time >= (Fire_CD + Fire_Last_Time)) //冷卻完成
            {
                if(PlayerMovement.isOnGround) //在地面
                {
                    Fire_Last_Time = Time.time;
                    anim.SetTrigger("GroundFire");
                    Instantiate(FireBall,ShootPoint.position,transform.rotation);
                }
                else if(!PlayerMovement.isOnGround) //在空中
                {
                    Fire_Last_Time = Time.time;
                    anim.SetTrigger("JumpFire");
                    Instantiate(FireBall,ShootPoint.position,transform.rotation);
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            if((Time.time >= (LastDash + dashCoolDown)) && PlayerMovement.horizontalmove !=0)
            {   
                ReadyToDash();
            }
        }

        if(playerHealth.isHurt)
        {
            return;
        }
        if(Time.time >=(LastDash + 0.5f))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }

    private void FixedUpdate() 
    {
        Dash();
    }

    void ZboxAttive()
    {
        Z_Attack_Box.SetActive(true);
    }

    void ZboxUnAttive()
    {
        Z_Attack_Box.SetActive(false);
    }

    void ReadyToDash()
    {
        Dashing = true;

        dashTimeLeft = dashTime;

        LastDash = Time.time;
    }

    void Dash()
    {
        if(Dashing)
        {
            if(dashTimeLeft >0)
            {
                gameObject.layer = LayerMask.NameToLayer("Invincible");
                rb.velocity = new Vector2(dashSpeed * PlayerMovement.facedirection,rb.velocity.y );
                dashTimeLeft -= Time.deltaTime;
                ShadowPool.instance.GetFormPool();
            }
            else if(dashTimeLeft <= 0)
            {
                Dashing = false;
            }
             
        }
    }
}
