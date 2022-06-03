using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_2_Boss_Bridge : MonoBehaviour
{
    BossHealthController bossHealth;
    Collider2D coll;

    public GameObject Bridge;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        bossHealth = GameObject.FindObjectOfType<BossHealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(Bridge);
            bossHealth.enabled = true;
        }
    }
}
