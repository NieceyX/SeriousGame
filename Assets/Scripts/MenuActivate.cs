using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuActivate : MonoBehaviour
{
    public GameObject exit;
    public GameObject restart;

    public void doActivate()
    {
        exit.SetActive(!exit.activeSelf);
        restart.SetActive(!restart.activeSelf);
    }

}
