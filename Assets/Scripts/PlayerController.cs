using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{


    private BaseStateMachine attackStateMachine;


    // Awake
    void Start()
    {
        attackStateMachine = new BaseStateMachine();
        attackStateMachine.Awake();
        attackStateMachine.CurrentState = new IdleState(this);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        attackStateMachine.Update();
    }

    private void FixedUpdate()
    {
        // Physics calculations
        Move();
    }

    private void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY);


        float attack1 = Input.GetAxis("Fire1");
        if (attack1 == 1)
        {
            attackStateMachine.AttemptTransition("Attack");
        }
        float attack2 = Input.GetAxis("Fire2");
        if (attack2 == 1)
        {
            // DASH
            attackStateMachine.AttemptTransition("Attack2");
        }
        

    }

    private void Move()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(moveDirection.x * moveSpeed,
            moveDirection.y * moveSpeed);
    }

    private void OnAnimatorMove()
    {

    }
}
