using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player" && !other.isTrigger) {
            Debug.Log("Hello World 1");
            var tag = gameObject.tag;
            Debug.Log("Tag : " + tag);
            var destroy_item = GameObject.FindGameObjectWithTag("Lock_"+tag);
            Destroy(destroy_item);
           
        }
    }
}
