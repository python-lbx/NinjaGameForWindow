using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public Joystick joystick;
    public float speed;
    public float horizontalMove = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        joystickMove();
    }

    void joystickMove()
    {
        print(joystick.Horizontal);
        rb.velocity = new Vector2(horizontalMove,rb.velocity.y);
        if(joystick.Horizontal >= 0.2f)
        {
            horizontalMove = speed;
        }
        else if(joystick.Horizontal <= -0.2f)
        {
            horizontalMove = -speed;
        }
        else
        {
            horizontalMove = 0f;
        }
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x,speed);
    }
}
