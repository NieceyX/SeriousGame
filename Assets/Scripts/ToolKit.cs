using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolKit : MonoBehaviour
{
    [SerializeField] MixedRealityToolkitConfigurationProfile myToolkit;
    [SerializeField] MixedRealityToolkitConfigurationProfile Begin_Toolkit;
    private bool canPickWeapon = true;
    private GameObject varGameObject;
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

        varGameObject = GameObject.Find("MixedRealityPlayspace");
        varGameObject.GetComponent<ThumbstickMover>().enabled = false;
        StartCoroutine(MovementOn());
        StartCoroutine(PickUpCooldown());
        

    }

    IEnumerator PickUpCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        canPickWeapon = true;
    }

    IEnumerator MovementOn()
    {
        yield return new WaitForSeconds(0.3f);
        varGameObject.GetComponent<ThumbstickMover>().enabled = true;
    }

    public bool GetCanPickWeapon()
    {
        return canPickWeapon;
    }

}
