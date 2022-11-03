using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuActivate : MonoBehaviour
{
    public GameObject menuButtons;

    public void doActivate()
    {
        menuButtons.SetActive(!menuButtons.activeSelf);
    }
}
