using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderScript : MonoBehaviour
{
    // settings
    [Header("Settings")]
    public float thunderLimit = 5f;

    // physics
    Vector3 vel;

    // player script here
    public ThunderPlayerMove playerWhoDroppedMe;
    //public FirePlayerMove duration;

    private void Start()
    {
        ThunderDuration();
    }

    // Update is called once per frame
    void Update()
    {

        // Commented out because it doesn't allow for the force to be applied 
        
        // GetComponent<Rigidbody2D>().MovePosition(transform.position + vel * Time.deltaTime);
    }

    // fire death by duration
    public void ThunderDuration()
    {
        Destroy(gameObject, thunderLimit);
    }

    // used to destroy a game object tagged "Wall"

    private void OnTriggerEnter2D(Collider2D c)
    {
        // if (c.tag == "Wood_1"){
        //     Destroy(c.gameObject);
        // }
        if (c.CompareTag("Wall"))
        {
            ThunderDeath();
        }
        if (c.tag != "Player") {
             playerWhoDroppedMe.thunders.Remove(gameObject);
             Destroy(gameObject);
        }
       
    }

    public void ThunderDeath()
    {
        playerWhoDroppedMe.thunders.Remove(gameObject);
        Destroy(gameObject);
    }

}
