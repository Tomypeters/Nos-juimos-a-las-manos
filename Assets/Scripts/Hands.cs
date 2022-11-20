using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{

    protected Entity owner;
    protected Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        owner = transform.parent.GetComponent<Entity>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnAnimatorMove()
    {
        
    }
}
