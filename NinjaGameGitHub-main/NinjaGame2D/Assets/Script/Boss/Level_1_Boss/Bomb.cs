using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D coll;
    SpriteRenderer SR;
    Animator anim;

    public float force;

    public int damage;

    PlayerHealthController playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        SR = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        playerHealth = GameObject.FindObjectOfType<PlayerHealthController>();


        rb.AddForce(transform.up*force , ForceMode2D.Impulse);

    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.name == "Ground")
        {
            anim.SetTrigger("Start");
        }
        else if(other.gameObject.name == "Player")
        {
            anim.SetTrigger("Explore");
            FindObjectOfType<AVmanager>().Play("Bomb");

            playerHealth.Health_Current -= damage;
            gameObject.layer = LayerMask.NameToLayer("Invincible");
        }
    }

    void ChangeRed()
    {
        FindObjectOfType<AVmanager>().Play("Bomb");

        Destroy(this.gameObject);
    }

    void destroy()
    {
        Destroy(this.gameObject);
    }
}
