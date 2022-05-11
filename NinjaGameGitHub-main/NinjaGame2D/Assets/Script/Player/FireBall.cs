using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D coll;

    public int speed;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

        rb.velocity = transform.right * speed;

        Destroy(this.gameObject,1f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Boss"))
        {
            FindObjectOfType<BossHealthController>().Health_Current -= damage;
            coll.enabled = false;
        }
    }
}
