using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenus : MonoBehaviour
{

    public GameObject TargetUI;

    public GameObject targettext1, targettext2;

    public void ToggleUIOn()
    {
        TargetUI.SetActive(true);
    }

    public void ToggleUIOff()
    {
        TargetUI.SetActive(false);
    }

    public void ToggleNextPage()
    {
        targettext1.SetActive(false);
        targettext2.SetActive(true);
        
    }

    public void TogglePreviousPage()
    {
        targettext2.SetActive(false);
        targettext1.SetActive(true);
        
    }
}
