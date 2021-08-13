using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderDetector : MonoBehaviour
{
    // i need somewhere to store the tag of the current item
    // i need to find the specific object tied to it and activate an
    // animation or action 
    // i need to keep track of how many times I've been hit 

    private string tag ; 
    private int hitCount; 
    private GameObject door;
    public GameObject thunderSpawn;
    // Start is called before the first frame update
    void Start()
    {
        tag = gameObject.tag;
        hitCount = 0;
    }

    // Update is called once per frame


    private void OnTriggerEnter2D(Collider2D c)
    {
        

        if (c.name == "LightningBolt(Clone)"){
            hitCount++; 
        }
    
        if (hitCount == 3) {
            if(tag == "1") {
                door = GameObject.FindWithTag("Light_" + tag);
                Animator open = door.GetComponent<Animator>();
                open.SetBool("Destroyed", true);
            } else { 
                Destroy(GameObject.FindWithTag("Light_" + tag));
            }
        }
    
        
    }
}
