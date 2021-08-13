using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThunderPlayerMove : MonoBehaviour
{
    // Movement, animator, velocity, and physics for the player
    Rigidbody2D rb;
    Vector3 moveSpeed;
    public Animator animate;

    // Thunder
    [Header("Thunder")]
    public GameObject thunderSpawn;
    public List<GameObject> thunders = new List<GameObject>();
    public int shootSpeed;
    public bool canShoot; 
    public float lightCooldown; // feel free to set whatever number you want
  
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


    // Sounds header
    //[Header("Sounds Setting")]
    [Header("Character Controller")]
    public CharacterController2D controller;
    // Start is called before the first frame update
    void Start()
    {
        
        mJumps = jumps;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // horizontal float
         horizontalMove = Input.GetAxisRaw("Horizontal2") * speed;
        animate.SetFloat("Speed", Mathf.Abs(horizontalMove));
        // Moving the player using the arrow keys

        
        if (Input.GetKeyDown(KeyCode.N)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        

        // Make the player jump using the up arrow

        if (Input.GetKey(KeyCode.UpArrow) && jumps > 0)
        {
            Jump();
            animate.SetBool("isJumping", true);
        }

        // Make the player use action using the Jump key
        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            Action();
            ClearList();

            lightCooldown = Time.time + 0.50f;
            canShoot = false;
            //Action();
        }

        if (!canShoot && Time.time > lightCooldown) {
            canShoot = true;
        }

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
        GameObject f = Instantiate(thunderSpawn, transform.position, Quaternion.identity);
        if (Left){ 
             f.GetComponent<Rigidbody2D>().AddForce(-transform.right * shootSpeed , ForceMode2D.Impulse);
        } else {
             f.GetComponent<Rigidbody2D>().AddForce(transform.right * shootSpeed , ForceMode2D.Impulse);
        }
        thunders.Add(f);
        f.GetComponent<ThunderScript>().playerWhoDroppedMe = this;
    }

 public void ClearList()
    {
        for (var i = thunders.Count -1; i > -1; i--)
        {
            if (thunders[i] == null)
                thunders.RemoveAt(i);
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
             animate.SetBool("isJumping", false);
        }

        if(col.transform.CompareTag("Wood_1"))
        {
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
