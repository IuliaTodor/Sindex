using UnityEngine;
using UnityEngine.UI;

public class ButtonSearch : MonoBehaviour
{
    private Toggle toggle;

    public void Start() { toggle = transform.Find("ID Toggle").GetComponent<Toggle>(); }

    // ADD THIS TO A CANVAS
    public void Search(Text component) { SQLManager.Instance.SearchInput(component.text, true); }
}
