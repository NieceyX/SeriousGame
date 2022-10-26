using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolKit : MonoBehaviour
{
    [SerializeField] MixedRealityToolkitConfigurationProfile myToolkit;
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
        MixedRealityToolkit.Instance.ActiveProfile = myToolkit;

        Debug.Log("eee");
    }
}
