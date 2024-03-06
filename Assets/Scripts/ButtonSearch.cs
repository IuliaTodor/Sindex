using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSearch : MonoBehaviour
{
    private Button byID;

    public void Start()
    {
        byID = transform.Find("Settings").Find("By ID").GetComponent<Button>();
    }

    // ADD THIS TO A CANVAS
    public void Search(Text component)
    {
        List<string> searches = SQLManager.Instance.SearchInput(component.text, !byID.interactable);
        SQLManager.Instance.currentMapDistribution.ColorChange(searches);
    }
}
