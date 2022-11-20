using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rigidbody;
    
    protected Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        Debug.Log("ADD FORCE");
        rigidbody.AddForce(moveDirection.normalized * 500f);
    }
}
