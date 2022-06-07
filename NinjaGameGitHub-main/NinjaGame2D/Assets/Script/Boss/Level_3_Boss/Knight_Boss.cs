using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_Boss : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D coll;
    Collider2D obj;
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

    public GameObject bullet;
    public Transform shootpoint;

    [Header("移動範圍")]
    public Transform leftPoint;
    public Transform rightPoint;

    public float AttackCoolDown;
    public int damage;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed",Mathf.Abs(rb.velocity.x) );

        if(Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(bullet,shootpoint.transform.position,transform.rotation);
        }
        //Movement();
        //hit = Physics2D.OverlapBox(new Vector3(meleedetectbox.position.x * direction,meleedetectbox.position.y,meleedetectbox.position.z),boxsize,0,playerLayer);


        Collider2D obj = Physics2D.OverlapBox(meleedetectbox.transform.position,boxsize,0,playerLayer); //顯示你碰撞到什麼需要用到這行代碼
        hit = Physics2D.OverlapBox(meleedetectbox.transform.position,boxsize,0,playerLayer); //判斷你有沒有碰到物件

    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = hit? Color.red:Color.green;
        //Gizmos.DrawWireCube(new Vector3(meleedetectbox.position.x * direction,meleedetectbox.position.y,meleedetectbox.position.z),boxsize);
        Gizmos.DrawWireCube(meleedetectbox.transform.position,boxsize);
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

    //需要接著coll obj;
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
}
