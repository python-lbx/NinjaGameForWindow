using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Box_Controller : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            FindObjectOfType<PlayerHealthController>().Health_Current -= damage;
        }
    }
}
