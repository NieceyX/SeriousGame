using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlay : MonoBehaviour
{
    [Header("Note  audio")]
    public AudioSource audioSource;

    void Update()
    {
        Collider[] colls = Physics.OverlapSphere(this.transform.position, 0.7f);
        foreach (Collider item in colls)
        {
            if (item.gameObject.tag == "GameController")
            {
                audioSource.Play();
                Debug.Log("play sound");
            }
        }
    }

   /* void OnCollisionEnter(Collision coll)
    {
        audioSource.Play();
        Debug.Log("play sound");
    }
   */
}
