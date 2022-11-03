using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolKit : MonoBehaviour
{
    [SerializeField] MixedRealityToolkitConfigurationProfile myToolkit;
    [SerializeField] MixedRealityToolkitConfigurationProfile Begin_Toolkit;
    private bool canPickWeapon = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changed()
    {
        if (MixedRealityToolkit.Instance.ActiveProfile == Begin_Toolkit)
        {
            MixedRealityToolkit.Instance.ActiveProfile = myToolkit;
        }
        else
        {
            MixedRealityToolkit.Instance.ActiveProfile = Begin_Toolkit;
        }
        canPickWeapon = false;
        StartCoroutine(PickUpCooldown());
        
    }

    IEnumerator PickUpCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        canPickWeapon = true;
    }

    public bool GetCanPickWeapon()
    {
        return canPickWeapon;
    }

}
