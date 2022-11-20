using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Entity
{

    private bool collided = false;
    private float collisionTimer = 0;
    private Quaternion modifier = Quaternion.AngleAxis(0, Vector3.up);

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        Vector3 targetDir = (target.transform.position - transform.position);

        if (animator != null)
        {
            animator.SetFloat("Horizontal", targetDir.normalized.x);
            animator.SetFloat("Vertical", targetDir.normalized.y);
        }

        if (targetDir.magnitude >= 1.5)
        {
            if (collided)
            {
                targetDir = modifier * targetDir;
            }


            transform.position += targetDir.normalized * moveSpeed * Time.deltaTime;
        }

        if (collisionTimer > 0)
            collisionTimer -= Time.deltaTime;

        if (collisionTimer <= 0)
            collided = false;


    }

    protected override void Move()
    {
        // nothing
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            collided = true;

            float rand = Random.Range(1, 3);
            if (rand == 1)
                modifier = Quaternion.AngleAxis(90, Vector3.up);
            else if (rand == 2)
                modifier = Quaternion.AngleAxis(-90, Vector3.up);
            else
                modifier = Quaternion.AngleAxis(0, Vector3.up);
            collisionTimer = 1;
        }
    }
}
