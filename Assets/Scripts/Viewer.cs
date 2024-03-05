using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Viewer : MonoBehaviour
{
    public GameObject sinName;
    public GameObject sinSin;
    public GameObject sinDescription;
    public GameObject element1;
    public GameObject element2;

    // Start is called before the first frame update
    void Start()
    {
        List<string> sinData = SQLManager.Instance.Query($"SELECT * FROM Pecados P WHERE P.Pecado_ID = '{SQLManager.Instance.sinSelectedFromMenu}'");
        sinName.GetComponent<Text>().text = sinData[1];
        sinSin.GetComponent<Text>().text = sinData[2];
        sinDescription.GetComponent<Text>().text = sinData[3];
        element1.GetComponent<Text>().text = sinData[4];
        element2.GetComponent<Text>().text = sinData[5];
    }
}
