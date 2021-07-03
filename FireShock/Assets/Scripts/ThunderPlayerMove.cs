using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderPlayerMove : MonoBehaviour
{
    // Movement, animator, velocity, and physics for the player
    Rigidbody2D rb;
    Vector3 moveSpeed;
    Animator animate;

    // Thunder
    //[Header("Thunder")]
    //public GameObject thunderSpawn;
    //public List<GameObject> thunders = new List<GameObject>();
    //private int thundersMax = 3; // feel free to set whatever number you want
    //public int thundersNum;

    // Header velocity input

    [Header("Speed")]
    public float speed;

    // Header onGround input
    [Header("Is the object on the ground?")]
    public bool onGround;

    // Jump header (List mJumps to 1)
    [Header("Jump Setting")]
    public int jumps;
    public int mJumps;

    // Animation header
    [Header("Animation Setting")]
    public bool Idle_Left;
    public bool Idle_Right;
    public bool Jump_Left;
    public bool Jump_Right;
    public bool Run_Left;
    public bool Run_Right;

    // Sounds header
    //[Header("Sounds Setting")]

    // Start is called before the first frame update
    void Start()
    {
        mJumps = jumps;
        rb = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // horizontal float
        float horizontal = 1;
        // Moving the player using the arrow keys

        // left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * horizontal * speed * Time.deltaTime;
        }

        // right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * horizontal * speed * Time.deltaTime;
        }

        // Make the player jump using the up arrow

        if (Input.GetKey(KeyCode.UpArrow) && jumps > 0)
        {
            Jump();
        }

        // Make the player use action using the Jump key
        if (Input.GetButtonDown("Jump"))
        {
            //Action();
        }

        // Animation void here
        AnimationUpdate();
        // RigidBody move
        rb.MovePosition(transform.position + moveSpeed * Time.deltaTime);

    }

    // jump code (Y has been set to 8. Feel free to change)
    public void Jump()
    {
        // Animation void here
        //AnimationUpdate();
        rb.velocity = new Vector3(0, 8, 0);
        onGround = false;
        jumps--;
    }

    // action code
    //public void Action()
    //{

    //}

    // animation code
    public void AnimationUpdate()
    {
        // flip to the left
        if (moveSpeed.x < 0)
        {
            animate.SetBool("Run_Left", true);
            animate.SetBool("Idle_Left", false);
        }
        else
        {
            animate.SetBool("Run_Left", false);
            animate.SetBool("Idle_Left", true);

        }
        // flip to the right
        if (moveSpeed.x > 0)
        {
            animate.SetBool("Run_Right", true);
            animate.SetBool("Idle_Right", false);
        }
        else
        {
            animate.SetBool("Run_Right", false);
            animate.SetBool("Idle_Right", true);
        }
        // jump to the left
        if (moveSpeed.x < 0 && moveSpeed.y > 0)
        {
            animate.SetBool("Jump_Left", true);
        }
        else
        {
            animate.SetBool("Jump_Left", false);
        }
        // jump to the right
        if (moveSpeed.x > 0 && moveSpeed.y > 0)
        {
            animate.SetBool("Jump_Right", true);
        }
        else
        {
            animate.SetBool("Jump_Right", false);
        }
    }

    // collision code
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Ground"))
        {
            moveSpeed.y = 0;
            onGround = true;
            jumps = mJumps;
        }

        if(col.transform.CompareTag("Wood_1"))
        {
            Debug.Log("Test1");
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.CompareTag("Ground"))
        {
            onGround = false;
        }
    }
}
