using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSearch : MonoBehaviour
{
    private Toggle toggle;

    public void Start() { toggle = transform.Find("ID Toggle").GetComponent<Toggle>(); }

    // ADD THIS TO A CANVAS
    public void Search(Text component)
    {
        List<string> searches = SQLManager.Instance.SearchInput(component.text, toggle.isOn);
        SQLManager.Instance.currentMapDistribution.ColorChange(searches);
    }
}
