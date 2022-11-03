using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Audio;

public class BreakingObject : MonoBehaviour
{
    [SerializeField] GameObject AfterBreak;
    public AudioEvent GlassBreak;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            Debug.Log(this.gameObject.GetComponent<Rigidbody>().velocity.x);
            if (Mathf.Abs(this.gameObject.GetComponent<Rigidbody>().velocity.x) > 0.3f || 
                Mathf.Abs(this.gameObject.GetComponent<Rigidbody>().velocity.y) > 0.3f || 
                Mathf.Abs(this.gameObject.GetComponent<Rigidbody>().velocity.z) > 0.3f)
            {
                Instantiate(AfterBreak, this.transform.position, Quaternion.identity).transform.localScale = new Vector3(100, 100, 100);
                AudioManager.PlayEvent(this.GlassBreak, this.gameObject);
                Destroy(this.gameObject);
            }
            /*   Instantiate(AfterBreak, this.transform.position, Quaternion.identity);*/

        }
        
    }


}
