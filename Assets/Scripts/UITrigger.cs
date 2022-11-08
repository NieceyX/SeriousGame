using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITrigger : MonoBehaviour
{
    public GameObject menuItem;

    void Start()
    {
        menuItem.SetActive(false); 
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Enter");
        if (col.gameObject.tag == "GameController")
        {
            
            menuItem.SetActive(true);
        }
    }

    void OnTriggerExit(Collider col)
    {
        Debug.Log("Exit");
        if (col.gameObject.tag == "GameController")
        {
            
            menuItem.SetActive(false);
        }
    }
}
