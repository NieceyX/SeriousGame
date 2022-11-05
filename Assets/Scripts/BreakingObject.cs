using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BreakingObject : MonoBehaviour
{
    [SerializeField] GameObject AfterBreak;
    [SerializeField] float size = 10f;
    [SerializeField] float BreakSpeed = 0.3f;
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
            if (Mathf.Abs(this.gameObject.GetComponent<Rigidbody>().velocity.x) > BreakSpeed || 
                Mathf.Abs(this.gameObject.GetComponent<Rigidbody>().velocity.y) > BreakSpeed || 
                Mathf.Abs(this.gameObject.GetComponent<Rigidbody>().velocity.z) > BreakSpeed)
            {
                Instantiate(AfterBreak, this.transform.position, this.transform.rotation).transform.localScale = new Vector3(size, size, size);
                Destroy(this.gameObject);
            }
            /*   Instantiate(AfterBreak, this.transform.position, Quaternion.identity);*/

        }
        
    }


}
