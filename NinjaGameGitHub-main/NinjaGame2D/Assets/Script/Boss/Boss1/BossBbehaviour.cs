using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBbehaviour : MonoBehaviour
{
    public Animator anim;
    public Transform transformPoint;
    public BossABehavior BossA;
    [Header("巡邏")]
    public float Patrolspeed;
    public float startWaitTime;
    public float WaitTime;

    public Transform movePos;
    public Transform leftDownPos;
    public Transform rightUpPos;

    [Header("橫衝")]
    public Transform[] point;
    public int BossBRndPos;
    public Transform MovePos;
    public int Rushspeed;
    public int time;

    [Header("階段")]
    public bool phase;
    public float phaseTime;
    public float StartphaseTime;
    public enum Status {Idle,patrol,CircleMove,HRush,Transform,Transform_I};
    public Status BossB_Status;

    
    [Header("圓運動")]
    public Transform RotationCenter;
    public float AngularSpeed,RotationRaduis;
    float posX,posY,angle;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        BossA = GameObject.Find("BossA").GetComponent<BossABehavior>();
        BossBRndPos = Random.Range(0,4); //隨機點設定

        movePos.position =  GetRandomPos();
        WaitTime = startWaitTime;

        //靜止
        phaseTime = 5; //靜止階段時間

        BossB_Status = Status.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch(BossB_Status)
        {
            case Status.Idle:
                gameObject.layer = LayerMask.NameToLayer("BossUnCollable");
            break;
            
            case Status.Transform: //轉送待機
                transform.position = transformPoint.position; //時間內固定在傳送點
            break;

            case Status.Transform_I: 
                if(phaseTime >0) //5秒後圓運動
                {
                    phaseTime -= Time.deltaTime;
                    transform.position = transformPoint.position; //時間內固定在傳送點
                }
                else if(phaseTime <= 0)
                {
                    phaseTime=5;//5秒圓運動

                    BossB_Status = Status.CircleMove;
                    BossA.BossA_Status = BossABehavior.Status.CircleMove;
                }
            break;

            case Status.patrol:
                gameObject.layer = LayerMask.NameToLayer("BossCollable");
                RndPatrol(); //時間內巡邏
            break;

            case Status.HRush:
                gameObject.layer = LayerMask.NameToLayer("BossCollable");
                if(time == 4)
                {
                    time = 0;
                    phaseTime =5;
                    BossB_Status = Status.Transform_I;
                }
                else
                {
                    if(phaseTime<=0)
                    {
                        HorizontalRush();
                    }
                    else
                    {
                        phaseTime -= Time.deltaTime;
                    }
                }
            break;

            case Status.CircleMove:
                gameObject.layer = LayerMask.NameToLayer("BossCollable");
                if(phaseTime >0)
                {
                    phaseTime -= Time.deltaTime;
                    circle();
                    RotationRaduis += Time.deltaTime;
                }
                else if(phaseTime <= 0)
                {
                    RotationRaduis = 5f;
                    transform.position = movePos.position;
                    BossA.transform.position = BossA.movePos.position;

                    BossB_Status = Status.patrol;
                    BossA.BossA_Status = BossABehavior.Status.patrol;

                    phaseTime = 5;
                    BossA.phaseTime = 15;//巡邏時間
                }
            break;
            
        }
 
    }

    void RndPatrol()
    {
        transform.position = Vector2.MoveTowards(transform.position,movePos.position,Patrolspeed * Time.deltaTime);

        if(Vector2.Distance(transform.position,movePos.position) < 0.1f)
        {
            if(WaitTime <= 0)
            {
                movePos.position =  GetRandomPos();
                WaitTime = startWaitTime;
            }
            else
            {
                WaitTime -= Time.deltaTime;
            }
        }
    }

    Vector2 GetRandomPos()
    {
        Vector2 rndPos = new Vector2(Random.Range(leftDownPos.position.x,rightUpPos.position.x),Random.Range(leftDownPos.position.y,rightUpPos.position.y));
        return rndPos;
    }

    void HorizontalRush()
    {
        transform.position = Vector2.MoveTowards(transform.position,MovePos.position,Rushspeed * Time.deltaTime);

        if(BossBRndPos == 0)
        {
            MovePos.position = point[1].position;
        }
        else if(BossBRndPos == 1)
        {
            MovePos.position = point[0].position;
        }
        else if(BossBRndPos == 2)
        {
            MovePos.position = point[3].position;
        }
        else if(BossBRndPos == 3)
        {
            MovePos.position = point[2].position;
        }


        if(Vector2.Distance(this.transform.position,MovePos.position)<0.1f)
        {
            time++;
            BossBRndPos = Random.Range(0,4);
            transform.position = point[BossBRndPos].transform.position;
        }
    }

    void circle()
    {
        posX = RotationCenter.position.x+Mathf.Cos(angle) * RotationRaduis;
        posY = RotationCenter.position.y+Mathf.Sin(angle) * RotationRaduis;
        transform.position = new Vector2(posX,posY);
        angle = angle + Time.deltaTime * AngularSpeed;

        if(angle >= 360)
        {
            angle = 0;
        }
    }

    void firstTrans()
    {
        BossB_Status = Status.Transform;
    }
}
