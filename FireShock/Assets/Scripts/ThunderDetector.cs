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
        
        Debug.Log(c.name);
        if (c.name == "LightningBolt(Clone)"){
            Debug.Log("hit");
            hitCount++; 
        }
    
        if (hitCount == 3) {
        Destroy(GameObject.FindWithTag("Light_" + tag));
        }
    
        
    }
}
