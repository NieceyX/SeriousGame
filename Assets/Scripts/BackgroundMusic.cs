using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Audio;

public class BackgroundMusic : MonoBehaviour
{
    public AudioEvent music;
    private float radius = 1f;
    public LayerMask mask;

    void Update() {

        Collider[] colls = Physics.OverlapBox(this.transform.position, transform.localScale / 2, Quaternion.identity,mask);

        foreach (Collider item in colls)
            {
                if (item.gameObject.tag == "GameController")
                {
                    AudioManager.PlayEvent(this.music, this.gameObject);

                }
        }
    }
}
