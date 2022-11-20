using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    public GameObject hands;

    private BaseStateMachine attackStateMachine;
    private List<Entity> enemies;
    
    // Awake
    protected void Start()
    {
        base.Start();
        attackStateMachine = new BaseStateMachine();
        attackStateMachine.Awake();
        attackStateMachine.CurrentState = new IdleState(this);

        enemies = new List<Entity>();
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

        if (moveDirection != Vector2.zero && moveDirection.magnitude > 0.1)
            facingDirection = moveDirection.normalized;

        float attack1 = Input.GetAxis("Fire1");
        if (attack1 == 1)
        {
            Debug.Log("ATACK!");
            attackStateMachine.AttemptTransition("Attack");
        }
        float attack2 = Input.GetAxis("Fire2");
        if (attack2 == 1)
        {
            // DASH
            attackStateMachine.AttemptTransition("Attack2");
        }

        float switch1 = Input.GetAxis("Target");
        //bool switch1 = Input.GetButtonDown("joystick button 5");
        if (Input.anyKeyDown && switch1 == 1 && target != null && enemies.Count > 1)
        {
            Debug.Log("SWITCH TARGET!");

            Vector2 targetVector = transform.position - target.transform.position;

            enemies.Sort(delegate (Entity a, Entity b)
            {
                return ((int)Vector2.SignedAngle(targetVector, (transform.position - a.transform.position)) + 180) -
                    ((int)Vector2.SignedAngle(targetVector, (transform.position - b.transform.position)) + 180);
                ;
            });
            Debug.Log(enemies.Count);
            Debug.Log("01 " + enemies[0].ToString());
            Debug.Log(Vector2.SignedAngle(targetVector, (transform.position - enemies[0].transform.position)));
            Debug.Log("02 " + enemies[1].ToString());
            Debug.Log(Vector2.SignedAngle(targetVector, (transform.position - enemies[1].transform.position)));
            Debug.Log("03 " + enemies[2].ToString());
            Debug.Log(Vector2.SignedAngle(targetVector, (transform.position - enemies[2].transform.position)));
            Debug.Log("04 " + enemies[3].ToString());
            Debug.Log(Vector2.SignedAngle(targetVector, (transform.position - enemies[3].transform.position)));
            target = enemies[1];
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemies.Add(collision.GetComponent<Entity>());
            if (target == null)
                target = collision.GetComponent<Entity>();
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            enemies.Remove(collision.GetComponent<Entity>());
    }

    private void Move()
    {
        rigidbody.velocity = new Vector2(moveDirection.x * moveSpeed,
            moveDirection.y * moveSpeed);

        if (target == null)
        {
            float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
            hands.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            Vector2 angleVector = target.transform.position - transform.position;
            float angle = Mathf.Atan2(angleVector.y, angleVector.x) * Mathf.Rad2Deg;
            hands.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        animator.SetFloat("Horizontal", hands.transform.right.x);
        Debug.Log(hands.transform.right.x);
        animator.SetFloat("Vertical", hands.transform.right.y);
        Debug.Log(hands.transform.right.y);
    }

    private void OnAnimatorMove()
    {

    }
}
