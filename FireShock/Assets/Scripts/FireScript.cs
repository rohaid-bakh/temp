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

        GetComponent<Rigidbody2D>().MovePosition(transform.position + vel * Time.deltaTime);
    }

    // fire death by duration
    public void FireDuration()
    {
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
