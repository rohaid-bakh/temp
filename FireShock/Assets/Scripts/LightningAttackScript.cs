using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAttackScript : MonoBehaviour
{
    bool hitObject = false;
    [SerializeField] private Collider2D attackBox;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            CheckForHit();
        }
    }

    private void CheckForHit()
    {
        int resultCount = 5;
        Collider2D[] hitColliders = new Collider2D[5];
        ContactFilter2D contactFilter = new ContactFilter2D();
        resultCount = attackBox.OverlapCollider(contactFilter, hitColliders);
        Debug.Log("Hits: " + resultCount);
        foreach (Collider2D collider in hitColliders)
        {
            if (collider == null) { continue; }
            Debug.Log("Overlap: " + collider.name);
            if (collider.transform.CompareTag("Wood_1"))
            {
                Debug.Log("Hit!");
                
            }

        }
    }
    
}
