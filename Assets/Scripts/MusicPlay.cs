using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlay : MonoBehaviour
{
    [Header("Note  audio")]
    public AudioSource audioSource;

    public int cooldown = 10;
    private int time = 5;

    void Start()
    {
        time = cooldown;
    }

    void Update()
    {
        if (time >= cooldown)
        {
            Collider[] colls = Physics.OverlapSphere(this.transform.position, 0.1f);
            foreach (Collider item in colls)
            {
                if (item.gameObject.tag == "GameController")
                {
                    audioSource.Play();
                    Debug.Log("play sound");
                    time = 0;
                    break;
                }
            }
        }
        time++;
    }

   /* void OnCollisionEnter(Collision coll)
    {
        audioSource.Play();
        Debug.Log("play sound");
    }
   */
}
