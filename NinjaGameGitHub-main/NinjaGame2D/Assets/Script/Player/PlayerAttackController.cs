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
    public float Z_Attack_SCD; //Start Cool Down;

    [Header("火球術")]
    public float Fire_CD;
    public float Fire_SCD;

    [Header("衝刺")]
    public float dashTime;//dash時長
    private float dashTimeLeft; //dash剩余時間
    private float LastDash = -10f; //上一次dash時間點
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
        if(Input.GetKeyDown(KeyCode.Z) && Z_Attack_SCD <=0) //地面攻擊
        {   
            Z_Attack_SCD = Z_Attack_CD;
            anim.SetTrigger("IsAttack");
        }
        else if(Input.GetKeyDown(KeyCode.Z) && Z_Attack_SCD <=0 && !PlayerMovement.isOnGround) //空中攻擊
        {
            Z_Attack_SCD = Z_Attack_CD;
            anim.SetTrigger("IsAttack");
        }


        if(Input.GetKeyDown(KeyCode.F) && Fire_SCD <=0 && !PlayerMovement.isOnGround) //空中噴火
        {
            Fire_SCD = Fire_CD;
            anim.SetTrigger("JumpFire");
            Instantiate(FireBall,ShootPoint.position,transform.rotation);
        }
        else if(Input.GetKeyDown(KeyCode.F) && Fire_SCD <= 0) //地面噴火
        {
            Fire_SCD = Fire_CD;
            anim.SetTrigger("GroundFire");
            Instantiate(FireBall,ShootPoint.position,transform.rotation);
        }

        if(Z_Attack_SCD >0)
        {
            Z_Attack_SCD -= Time.deltaTime;
        }

        if(Fire_SCD >0)
        {
            Fire_SCD -= Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            if((Time.time >= (LastDash + dashCoolDown)) && PlayerMovement.horizontalmove !=0)
            {   
                ReadyToDash();
            }
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
                gameObject.layer = LayerMask.NameToLayer("Player");
            }
             
        }
    }
}
