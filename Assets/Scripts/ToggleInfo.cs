using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleInfo : MonoBehaviour
{
    public GameObject oldInfoPanel;
    public GameObject newInfoPanel;

    public void TogglePanel()
    {
        oldInfoPanel.SetActive(false);
        newInfoPanel.SetActive(true);
    }
}
