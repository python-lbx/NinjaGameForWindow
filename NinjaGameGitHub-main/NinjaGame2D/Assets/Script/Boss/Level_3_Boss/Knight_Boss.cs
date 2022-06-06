using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_Boss : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D coll;
    [SerializeField]Transform meleedetectbox;
    [SerializeField]Vector2 boxsize;
    [SerializeField]LayerMask playerLayer;
    [SerializeField]bool hit;
    [SerializeField]int direction;
    Animator anim;

    public bool faceright;
    public float speed;

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
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(bullet,shootpoint.transform.position,transform.rotation);
        }
        Movement();
        //hit = Physics2D.OverlapBox(new Vector3(meleedetectbox.position.x * direction,meleedetectbox.position.y,meleedetectbox.position.z),boxsize,0,playerLayer);
        hit = Physics2D.OverlapBox(meleedetectbox.transform.position,boxsize,0,playerLayer);
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

}
