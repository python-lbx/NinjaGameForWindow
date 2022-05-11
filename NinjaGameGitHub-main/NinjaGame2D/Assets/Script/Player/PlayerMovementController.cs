using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    Rigidbody2D rb;
    CapsuleCollider2D capsulecoll;
    Animator anim;
    PlayerHealthController PlayerHP;
    PlayerAttackController PlayerAC;

    [Header("移動參數")]
    public float speed;
    public float horizontalmove;

    [Header("跳躍參數")]
    public float jumpForce;
    public bool jumpPressed;
    public bool isJump;
    public int jumpTime;

    [Header("角色狀態")]
    public bool isCrouch;
    public bool isOnGround;
    public bool isHeadBlock;
    public bool faceright;
    public int facedirection;

    [Header("環境檢測")]
    public Transform GroundCheckPoint;
    public Transform TopCheckPoint;
    public LayerMask groundLayer;
    public float GroundCheckDistance;
    public float TopCheckDistance;
    
    //按鍵設置

    Vector2 colliderStandSize; //站立碰撞框尺寸
    Vector2 colliderStandOffset; //站立碰撞框位置
    Vector2 colliderCrouchSize; //下蹲碰撞框尺寸
    Vector2 colliderCrouchOffset; //下蹲碰撞框位置

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsulecoll = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        PlayerHP = GetComponent<PlayerHealthController>();
        PlayerAC = GetComponent<PlayerAttackController>();

        //獲取碰撞框參數
        colliderStandSize = capsulecoll.size; //站立
        colliderStandOffset = capsulecoll.offset;
        colliderCrouchSize = new Vector2(0.7667918f,0.7667921f); //下蹲
        colliderCrouchOffset = new Vector2(-0.07691693f,-0.5154546f);
        
        faceright = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(PlayerHP.isHurt || PlayerAC.Dashing)
        {
            return;
        }
        GroundMovement();
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_Ground"))
        {
            return;
        }
        else if(anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_Jump"))
        {
            return;
        }
        else if(anim.GetCurrentAnimatorStateInfo(0).IsName("Skill_Jump_Ground"))
        {   
            rb.velocity = new Vector2(0,0);
            rb.gravityScale = 0;
            return;
        }
        else if(anim.GetCurrentAnimatorStateInfo(0).IsName("Skill_FireBall_Ground"))
        {
            rb.velocity = new Vector2(0,0);
            rb.gravityScale = 0;
            return;
        }
        else
        {
            rb.gravityScale = 2;
            FilpDirection();
        }
    }
    void Update()
    {
        animController();
        PhysicalCheck();
        if(isHeadBlock&&isCrouch&&isOnGround || PlayerHP.isHurt)
        {
            return;
        }
        Jump();

        if(PlayerHP.isHurt)
        {
            PlayerAC.Dashing = false;
            rb.velocity = new Vector2(0,0);
        }
    }

    void GroundMovement()
    {
        horizontalmove = Input.GetAxisRaw("Horizontal");
        //print(horizontalmove);
        rb.velocity = new Vector2(horizontalmove * speed,rb.velocity.y);



        if(Input.GetButton("Crouch") && isOnGround)
        {
            Crouch();
        }
        else if(!Input.GetButton("Crouch") && isCrouch && !isHeadBlock || !isOnGround)
        {
            StandUp();
        }
    }
    void Jump()
    {
        if(Input.GetButtonDown("Jump") && isOnGround && jumpTime>0)
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
            jumpTime--;
        }
        else if (Input.GetButtonDown("Jump") && jumpTime>0)
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
            jumpTime--;
        }
        else if(isOnGround)
        {
            jumpTime = 1;
        }
    }

    void FilpDirection()
    {
        if(horizontalmove <0 && faceright)
        {   
            facedirection = -1;
            faceright = false;
            transform.Rotate(0,180,0);
        }
        else if(horizontalmove >0 && !faceright)
        {
            facedirection = 1;
            faceright = true;
            transform.Rotate(0,180,0);
        }
    }

    void Crouch()
    {
        isCrouch = true;
        capsulecoll.size = colliderCrouchSize;
        capsulecoll.offset = colliderCrouchOffset;
    }

    void StandUp()
    {
       isCrouch = false;
       capsulecoll.size = colliderStandSize;
       capsulecoll.offset = colliderStandOffset;
    }

    void PhysicalCheck()
    {
        RaycastHit2D groundRay = Raycast(GroundCheckPoint,Vector2.down,GroundCheckDistance,groundLayer);
        RaycastHit2D topRay = Raycast(TopCheckPoint,Vector2.up,TopCheckDistance,groundLayer);

        
        if(groundRay)
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }

        if(topRay)
        {
            isHeadBlock = true;
        }
        else
        {
            isHeadBlock = false;
        }
    }
    RaycastHit2D Raycast(Transform pointpos, Vector2 raydir, float raydis,LayerMask layer)
    {
        RaycastHit2D hit = Physics2D.Raycast(pointpos.position,raydir,raydis,layer);

        Color color = hit? Color.red:Color.green;

        Debug.DrawRay(pointpos.position,raydir*raydis,color);

        return hit;
    }

    void animController()
    {
        anim.SetFloat("Speed",Mathf.Abs(horizontalmove));
        anim.SetBool("IsCrouch",isCrouch);
        anim.SetBool("IsHurt",PlayerHP.isHurt);

        if(rb.velocity.y >0)
        {
            anim.SetBool("IsJump",true);
        }

        if(isOnGround)
        {
            anim.SetBool("IsJump",false);
            anim.SetBool("IsFall",true);
        }
    }
}
