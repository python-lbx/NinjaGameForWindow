using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTest : MonoBehaviour
{
    public enum Status{Idle,Patrol,DashAttack,Strike,FireBall,Trans,Spell};
    [Header("當前階段")]
    public Status current_State;
    [Header("下個階段")]
    public Status next_State;

    [Header("階段時間")]
    public float PhaseTime;
    [Header("技能施放次數")]
    public float SKillTime;
    [Header("第幾技能")]
    public int Status_num;
    // Start is called before the first frame update
    void Start()
    {
        current_State = Status.Idle;
        Status_num = 3;
        PhaseTime = 5f;
    }

    // Update is called once per frame
    void Update()
    {


        //施法後動作
        if(Status_num == 0)
        {
            next_State = Status.DashAttack;         
        }
        else if(Status_num == 1)
        {
            next_State = Status.Strike;
        }
        else if(Status_num == 2)
        {
            next_State = Status.FireBall;
        }
        else if(Status_num == 3)
        {
            next_State = Status.Patrol;
        }
        
        switch(current_State)
        {
            case Status.Idle:
            if(PhaseTime >0 )
            {
                PhaseTime -= Time.deltaTime;
            }
            else if(PhaseTime <=0 )
            {
                if(Status_num == 3)
                {
                    Status_num = 0;
                    current_State = Status.Patrol;
                    PhaseTime = 10f;
                }
                else
                {
                    current_State = Status.Spell;
                    PhaseTime = 8f;
                }
            }
            break;

            case Status.Patrol:
            if(PhaseTime >0 )
            {
                PhaseTime -= Time.deltaTime;
            }
            else if(PhaseTime <=0 )
            {
                current_State = Status.Spell;
                PhaseTime = 8f;
            }
            break;

            case Status.Spell:
            if(PhaseTime >0 )
            {
                PhaseTime -= Time.deltaTime;
            }
            else if(PhaseTime <= 0)
            {
                current_State = next_State;
                PhaseTime = 2f;
            }
            break;

            case Status.DashAttack:
                if(SKillTime < 4)
                {
                    //施放技能
                    if(PhaseTime > 0)
                    {
                        PhaseTime -= Time.deltaTime;
                    }
                    else if(PhaseTime <= 0)
                    {
                        PhaseTime = 2f;
                        SKillTime ++;
                    }
                }
                else if(SKillTime == 4)
                {
                    Status_num ++;
                    SKillTime = 0;
                    PhaseTime = 5f;
                    current_State = Status.Idle;
                }
            break;

            case Status.Strike:
                if(SKillTime < 4)
                {
                    //施放技能
                    if(PhaseTime > 0)
                    {
                        PhaseTime -= Time.deltaTime;
                    }
                    else if(PhaseTime <= 0)
                    {
                        PhaseTime = 2f;
                        SKillTime ++;
                    }
                }
                else if(SKillTime == 4)
                {
                    Status_num ++;
                    SKillTime = 0;
                    PhaseTime = 5f;
                    current_State = Status.Idle;
                }
            break;

            case Status.FireBall:
                if(SKillTime < 4)
                {
                    //施放技能
                    if(PhaseTime > 0)
                    {
                        PhaseTime -= Time.deltaTime;
                    }
                    else if(PhaseTime <= 0)
                    {
                        PhaseTime = 2f;
                        SKillTime ++;
                    }
                }
                else if(SKillTime == 4)
                {
                    Status_num ++;
                    SKillTime = 0;
                    PhaseTime = 5f;
                    current_State = Status.Idle;
                }
            break;

            case Status.Trans:
            if(current_State == Status.FireBall)
            {
                print("0,3");
            }
            else if(current_State == Status.Strike)
            {
                print("0,1,2,3");
            }
            break;
        }
    }
}
