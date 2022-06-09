using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosTest : MonoBehaviour
{
    public float lastPos;
    public float currentPos;
    public float PosRecordCoolDown;
    public float LastPosRecordTime;
    // Start is called before the first frame update
    void Start()
    {
        lastPos = currentPos;
        currentPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {   currentPos = transform.position.x;
        if(Time.time >= (PosRecordCoolDown + LastPosRecordTime))
        {
            LastPosRecordTime = Time.time;
            lastPos = transform.position.x;
        }
    }
}
