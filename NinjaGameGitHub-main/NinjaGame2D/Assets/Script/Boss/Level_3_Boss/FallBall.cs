using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBall : MonoBehaviour
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

        playerHealth = GameObject.FindObjectOfType<PlayerHealthController>();

        Destroy(this.gameObject,3.5f);
    }

    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.name == "Player")
        {
            playerHealth.Health_Current -= damage;
            coll.enabled = false;
            Destroy(this.gameObject);
        }
    }
}
