using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Map : MonoBehaviour
{
    void Start()
    {
        ColorChange("Lujuria");
    }
    void ColorChange(string region)
    {
        UnityEngine.UI.Image[] regions = GetComponentsInChildren<UnityEngine.UI.Image>();
        foreach (UnityEngine.UI.Image child in regions)
        {
            if (child.name == region) child.color = Color.white;
            else child.color = new Color(0.3f, 0.3f, 0.3f);
        }
    }
}
