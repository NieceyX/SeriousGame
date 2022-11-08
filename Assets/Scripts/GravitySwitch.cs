using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{

    
    public void TurnOnGravity()
    {
        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        TurnOnGravity();

        

    }
}
