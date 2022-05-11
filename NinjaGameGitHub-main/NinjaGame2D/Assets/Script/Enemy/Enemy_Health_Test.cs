using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health_Test : MonoBehaviour
{
    public int Health;
    public int Damage;
    public int EnemyScore;

    TranslateDoor DoorRecord;
    // Start is called before the first frame update
    void Start()
    {
        DoorRecord = GameObject.FindObjectOfType<TranslateDoor>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Health<=0)
        {
            DoorRecord.New_LevelScore += EnemyScore;
            Destroy(this.gameObject);
        }
    }
}
