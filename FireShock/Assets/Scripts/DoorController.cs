using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
  

    public void Door() {
    Debug.Log ("Inside Destroy");
    Destroy(gameObject);
}
}
