using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FirePlayerMove : MonoBehaviour
{
    // Movement, animator, velocity, and physics for the player
    Rigidbody2D rb;
    Vector3 moveSpeed;
    public Animator animate;

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

    [Header("Brackeys Tutorial")]
    float horizontalMove = 0f;
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

    [Header("Character Controller")]
    public CharacterController2D controller;

    // Sounds header

    //[Header("Sounds Setting")]
    // Start is called before the first frame update
    void Start()
    {
        fires = new List<GameObject>();
        mJumps = jumps;
        rb = GetComponent<Rigidbody2D>();
        // animate = GetComponent<Animator>();
    }

    // Update is called once per frame
  
    void Update() {
        
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        animate.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if (Input.GetKeyDown(KeyCode.G) && canShoot)
        {
            // the prefab and the list clearing
            Action();
            ClearList();
            // the cooldown after the bullet has been shot
            fireCooldown = Time.time + 0.50f;
            canShoot = false;
        }

        if (!canShoot && Time.time > fireCooldown)
        {
            canShoot = true;
        }

         if (Input.GetKey(KeyCode.W) && jumps > 0)
        {
            Jump();
            animate.SetBool("isJumping", true);
        }
        
         if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        

    }

     public void Jump()
    {
        // Animation void here
        //AnimationUpdate();
        rb.velocity = new Vector3(0, 10, 0);
        onGround = false;
        jumps--;
    }

    void FixedUpdate () {
        if (horizontalMove < 0 ) { 
            Left = true;
        }else if (horizontalMove > 0) {
            Left = false;
        }
        controller.Move(horizontalMove * Time.fixedDeltaTime ,false, false);
    }
    // jump code (Y has been set to 8. Feel free to change)
    // public void Jump()
    // {
    //     // Animation void here
    //     //AnimationUpdate();
    //     rb.velocity = new Vector3(0, 8, 0);
    //     onGround = false;
    //     jumps--;
    // }

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

    // // remove list?
    public void ClearList()
    {
        for (var i = fires.Count -1; i > -1; i--)
        {
            if (fires[i] == null)
                fires.RemoveAt(i);
        }
    }

      private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Ground"))
        {
            moveSpeed.y = 0;
            onGround = true;
            jumps = mJumps;
            animate.SetBool("isJumping", false);
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
