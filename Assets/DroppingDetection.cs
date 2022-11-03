using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingDetection : MonoBehaviour
{
    [SerializeField] GameObject myWeapon;
    MixedRealityToolkit myToolkit;
    GameObject TempWeapon;
    // Start is called before the first frame update
    void Start()
    {
        myToolkit = FindObjectOfType<MixedRealityToolkit>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Weapon") && myToolkit.GetComponent<ToolKit>().GetCanPickWeapon()) 
        {
            TempWeapon = Instantiate(myWeapon, other.transform.parent.transform.position, Quaternion.identity);
        /*    TempWeapon.transform.GetChild(0).GetComponent<CapsuleCollider>().isTrigger = true;*/
            StartCoroutine(AvoidCollisionWhenChanging());
            TempWeapon.transform.position = other.transform.parent.transform.position;
            TempWeapon.transform.rotation = other.transform.parent.transform.rotation;
            TempWeapon.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
            myToolkit.GetComponent<ToolKit>().changed();
            
        }
    }

    IEnumerator AvoidCollisionWhenChanging()
    {
        yield return new WaitForSeconds(1f);
        TempWeapon.transform.GetChild(0).GetComponent<BoxCollider>().isTrigger = false;
    }
}
