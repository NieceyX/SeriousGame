using Microsoft.MixedReality.Toolkit.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] float PositionXChange = 0.1f;
    [SerializeField] float PositionZChange = 0.1f;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("acvcc");
            Debug.Log(other.gameObject.name);
            other.transform.parent.gameObject.transform.position = new Vector3(other.transform.parent.gameObject.transform.position.x + PositionXChange, 0.1f, other.transform.parent.gameObject.transform.position.z + PositionZChange);
        }
        Debug.Log("aaaaa");

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("acvcc");
            Debug.Log(other.gameObject.name);
            other.transform.parent.gameObject.transform.position = new Vector3(other.transform.parent.gameObject.transform.position.x + PositionXChange, 0.1f, other.transform.parent.gameObject.transform.position.z + PositionZChange);
        }
        Debug.Log("aaaaa");
    }
    /*    void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Debug.Log("acvcc");
                collision.transform.position = new Vector3(collision.transform.position.x + 0.1f, 0.1f, collision.transform.position.z);
            }
            Debug.Log("aaaaa");



        }*/
}
