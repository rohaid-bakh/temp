using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//issues,  animation keeps looping
public class MovementController : MonoBehaviour
{
    public LayerMask groundLayer;
    private Rigidbody2D rigidbody2d;
    private CapsuleCollider2D collider;
    private float animatorTime;
    public float speed = 4f;
    [SerializeField] private Animator animator;
    [SerializeField] private float jumpVelocity = 5f;
    // Start is called before the first frame update
    void Start()
    {
        collider = gameObject.GetComponent<CapsuleCollider2D>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        animatorTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        IsGrounded();
        IsDodging();
        float horizontal = 1;
        float vertical = Input.GetAxis("Vertical");
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        transform.position = position;
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            animator.SetBool("Jumping", true);
        }
        if(Input.GetKeyDown(KeyCode.DownArrow) && IsGrounded()) {
            animator.SetBool("Dodging", true);
        }
    }

    public void Jump()
    {
        rigidbody2d.velocity = Vector2.up * jumpVelocity;
    }
    public void EndJump()
    {
        animator.SetBool("Jumping", false);
    }

    public void EndDodge()
    {
        animator.SetBool("Dodging", false);
    }

    public void EndAttack()
    {
        animator.SetBool("Attacking", false);
    } 

    //animator.IsInTransition is to not cut the animation while stopping
    //aimatorTime is to check on what loop count the animation is on
    //Have to put in .4f instead of 1 because it would overshoot

    private void IsDodging(){
     if (animator.GetCurrentAnimatorStateInfo(0).IsName("MC_Dodge") && 
     ( animatorTime >= .4f && !animator.IsInTransition(0)))
        {
            EndDodge();
        }
    }



    private bool IsGrounded()
    {
        Vector2 position = collider.transform.position;
        Vector2 direction = Vector2.down;
        //variable that needs to be changed if you change size of sprite
        float distance = 1.5f;
        Debug.DrawRay(position, direction, Color.red, Time.deltaTime, false);
        //draws a line and checks to see if there's anything below
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            animator.SetBool("Grounded", true);
            return true;
        }
        animator.SetBool("Grounded", false);
        return false;
    }
}
