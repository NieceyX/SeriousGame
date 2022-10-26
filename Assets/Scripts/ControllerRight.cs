﻿using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Profiling;

public class ControllerRight : MonoBehaviour
{

    MixedRealityToolkit myToolkit;
    void Awake()
    {
        myToolkit = FindObjectOfType<MixedRealityToolkit>();
    }

    void OnCollisionEnter(Collision collision)
    {
        myToolkit.GetComponent<ToolKit>().changed();
    }
}
