using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    // settings
    [Header("Setrings")]
    public float fireLimit = 3f; // last for 3 secs


    // player script here
    public FirePlayerMove playerWhoDroppedMe;

    // no Start

    // Update is called once per frame
    void Update()
    {
        FireDuration();
    }

    // fire death by duration
    public void FireDuration()
    {
        playerWhoDroppedMe.fires.Remove(gameObject);
        Destroy(gameObject, fireLimit);
    }

    // used to destroy a game object tagged "Wall"

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Wall"))
        {
            FireDeath();
        }
    }

    public void FireDeath()
    {
        playerWhoDroppedMe.fires.Remove(gameObject);
        Destroy(gameObject);
    }
}
