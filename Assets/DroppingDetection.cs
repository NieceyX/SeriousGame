using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingDetection : MonoBehaviour
{
    [SerializeField] GameObject myWeapon;
    MixedRealityToolkit myToolkit;
    // Start is called before the first frame update
    void Start()
    {
        myToolkit = FindObjectOfType<MixedRealityToolkit>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            GameObject TempWeapon = Instantiate(myWeapon, other.transform.parent.transform.position, Quaternion.identity);
            Debug.Log(other.name);
            Debug.Log(other.transform.parent.transform.position);
            TempWeapon.transform.position = other.transform.parent.transform.position;
            TempWeapon.transform.rotation = other.transform.parent.transform.rotation;
            myToolkit.GetComponent<ToolKit>().changed();
        }
    }
}
