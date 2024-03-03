using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Viewer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<string> sinData = SQLManager.Instance.SearchInput(SQLManager.Instance.sinSelectedFromMenu.ToString());
        GameObject.Find("Canvas").transform.Find("Sin Name").GetComponent<Text>().text = sinData[1];
    }
}
