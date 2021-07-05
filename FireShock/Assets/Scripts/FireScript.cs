using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    // settings
    [Header("Settings")]
    public float fireLimit = 5f;

    // physics
    Vector3 vel;

    // player script here
    public FirePlayerMove playerWhoDroppedMe;
    //public FirePlayerMove duration;

    private void Start()
    {
        FireDuration();
    }

    // Update is called once per frame
    void Update()
    {

        // Commented out because it doesn't allow for the force to be applied 
        
        // GetComponent<Rigidbody2D>().MovePosition(transform.position + vel * Time.deltaTime);
    }

    // fire death by duration
    public void FireDuration()
    {
        Destroy(gameObject, fireLimit);
    }

    // used to destroy a game object tagged "Wall"

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Wood_1"){
            Destroy(c.gameObject);
        }
        if (c.CompareTag("Wall"))
        {
            FireDeath();
        }
        if (c.tag != "Player") {
             playerWhoDroppedMe.fires.Remove(gameObject);
             Destroy(gameObject);
        }
       
    }

    public void FireDeath()
    {
        playerWhoDroppedMe.fires.Remove(gameObject);
        Destroy(gameObject);
    }

}
