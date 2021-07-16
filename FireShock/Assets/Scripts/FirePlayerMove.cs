using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlayerMove : MonoBehaviour
{
    // Movement, animator, velocity, and physics for the player
    Rigidbody2D rb;
    Vector3 moveSpeed;
    Animator animate;

    // Fireballs yum
    [Header("Fireballs")]
    public GameObject fireSpawn;
    public List<GameObject> fires;
    public int shootSpeed;
    public bool canShoot;
    public float fireCooldown;

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

    //Facing left or right? this is for shooting
    [Header("Left or Right?")]
    public bool Left = true;


    // Sounds header
    //[Header("Sounds Setting")]

    // Start is called before the first frame update
    void Start()
    {
        fires = new List<GameObject>();
        mJumps = jumps;
        rb = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // horizontal float
        float horizontal = 1;

        // Moving the player using the A S D keys

        // left
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * horizontal *speed * Time.deltaTime;
            Left = true;
        
        }

        // right 
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * horizontal * speed * Time.deltaTime;
            Left = false;

        }

        // Make the player jump using the W key

        if (Input.GetKey(KeyCode.W) && jumps > 0)
        {
            Jump();
        }

        // Make the player use action using the G key
        if (Input.GetKeyDown(KeyCode.G) && canShoot)
        {
            // the prefab and the list clearing
            Action();
            ClearList();
            // the cooldown after the bullet has been shot
            fireCooldown = Time.time + 0.50f;
            canShoot = false;
        }

        // cooldown done
        if (!canShoot && Time.time > fireCooldown)
        {
            canShoot = true;
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
    public void Action()
    {
       // call FireMove script
        GameObject f = Instantiate(fireSpawn, transform.position, Quaternion.identity);
        if (Left){ 
             f.GetComponent<Rigidbody2D>().AddForce(-transform.right * shootSpeed , ForceMode2D.Impulse);
        } else {
             f.GetComponent<Rigidbody2D>().AddForce(transform.right * shootSpeed , ForceMode2D.Impulse);
        }
        fires.Add(f);
        f.GetComponent<FireScript>().playerWhoDroppedMe = this;
    }

    // remove list?
    public void ClearList()
    {
        for (var i = fires.Count -1; i > -1; i--)
        {
            if (fires[i] == null)
                fires.RemoveAt(i);
        }
    }

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
    private void OnCollisionEnter2D (Collision2D col)
    {
        if(col.transform.CompareTag("Ground"))
        {
            moveSpeed.y = 0;
            onGround = true;
            jumps = mJumps;
        }
    }
    private void OnCollisionExit2D (Collision2D col)
    {
        if (col.transform.CompareTag("Ground"))
        {
            onGround = false;
        }
    }

}
