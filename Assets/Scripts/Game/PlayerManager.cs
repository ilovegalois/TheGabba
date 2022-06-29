using System;
using UnityEngine;
public class PlayerManager : MonoBehaviour
{


    public float moveSpeed = 0.5f;

    private bool dead;
    private Rigidbody2D rb;
    private Animator anim;
    public Joystick joystick;
    private Vector2 movement;
    Health health;

    private void Start()
    {
        health = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("Walking", false);

        health.OnDie += OnDeath;
        dead = false;
    }
    void Update()
    {
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;


        if ((movement.x != 0 || movement.y != 0) && !dead)
        {
            movement = LinearMovement(movement);
            anim.speed = 1;
            anim.SetBool("Walking", true);
            Move();
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }

    public Vector2 LinearMovement(Vector2 directions)
    {
        Vector2 retDirect;
        retDirect.x = Math.Abs(directions.x)> Math.Abs(directions.y)?directions.x:0;
        retDirect.y = Math.Abs(directions.y)> Math.Abs(directions.x)?directions.y:0;
        if(Math.Abs(directions.x)==Math.Abs(directions.y))
        {
             retDirect.x = directions.x;
        }

        return retDirect;
    }

    private void Move()
    {
        anim.SetFloat("m_Hor", movement.x);
        anim.SetFloat("m_Vert", movement.y);

        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

    private void OnDeath()
    {
        dead = true;
    }
}