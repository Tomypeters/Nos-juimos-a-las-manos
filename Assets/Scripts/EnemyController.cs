using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Entity
{

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        Vector3 targetDir = (target.transform.position - transform.position);

        if (targetDir.magnitude >= 1.5)
        {
            transform.position += targetDir.normalized * moveSpeed * Time.deltaTime;
        }


        if (animator != null)
        {
            animator.SetFloat("Horizontal", targetDir.normalized.x);
            animator.SetFloat("Vertical", targetDir.normalized.y);
        }
    }

    protected override void Move()
    {
        rigidbody.velocity = new Vector2(moveDirection.x * moveSpeed,
            moveDirection.y * moveSpeed);
    }
}
