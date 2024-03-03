using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Viewer : MonoBehaviour
{
    public GameObject sinName;
    public GameObject sinSin;
    public GameObject sinDescription;
    // Start is called before the first frame update
    void Start()
    {

        List<string> sinData = SQLManager.Instance.SearchInput(SQLManager.Instance.sinSelectedFromMenu.ToString());
        sinName.GetComponent<Text>().text = sinData[1];
        sinSin.GetComponent<Text>().text = sinData[2];
        sinDescription.GetComponent<Text>().text = sinData[3];
    }
}
