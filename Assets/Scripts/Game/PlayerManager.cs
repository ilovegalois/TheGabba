using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
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
        anim.SetBool("Dead", false);

        health.OnDie += OnDeath;
        dead = false;
    }
    void Update()
    {
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;


        if ((movement.x != 0 && movement.y != 0) && !dead)
        {
            anim.speed = 1;
            anim.SetBool("Dead", dead);
            Move();
        }
        else
        {
            anim.Play("Movement", 0, 0.9f);
            anim.speed = 0;
        }
    }

    private void Move()
    {
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);

        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

    private void OnDeath()
    {
        dead = true;
    }
}