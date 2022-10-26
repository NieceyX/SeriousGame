using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BreakingObject : MonoBehaviour
{
    [SerializeField] GameObject AfterBreak;
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
            /*   Instantiate(AfterBreak, this.transform.position, Quaternion.identity);*/
            Instantiate(AfterBreak, this.transform.position, Quaternion.identity).transform.localScale = new Vector3(100, 100, 100);
            Destroy(this.gameObject);
        }
        
    }


}
