using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBullet : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D coll;
    Animator anim;

    public float speed;
    public int damage;

    PlayerHealthController playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        rb.velocity = transform.right * speed;

        Destroy(this.gameObject,2f);

        playerHealth = GameObject.FindObjectOfType<PlayerHealthController>();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.name == "Player")
        {
            anim.SetTrigger("Explore");
            playerHealth.Health_Current -= damage;
            coll.enabled = false;
        }
    }

    void destroy()
    {
        Destroy(this.gameObject);
    }
}
