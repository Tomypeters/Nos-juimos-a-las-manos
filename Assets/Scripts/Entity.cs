
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float moveSpeed;
    public Animator handAnimator;

    protected AudioSystem audioSystem;
    protected Rigidbody2D rigidbody;
    protected Vector2 moveDirection = Vector2.right;
    protected Vector2 facingDirection = Vector2.right;

    // Start is called before the first frame update
    protected void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        audioSystem = GetComponent<AudioSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        Debug.Log("ADD FORCE");
        handAnimator.SetTrigger("Attack");
        audioSystem.PlayWhoosh();
    }

    public void FinishAttack()
    {
        rigidbody.AddForce(moveDirection.normalized * 250f);
        //handAnimator.ResetTrigger("Attack");
    }
}
