using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlay : MonoBehaviour
{
    [Header("Note  audio")]
    public AudioSource audioSource;

    void Start()
    {

    }

    void OnCollisionEnter(Collision coll)
    {
        //audioSource.Play();
        Debug.Log("play sound");
    }

}
