using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Viewer : MonoBehaviour
{
    public GameObject sinName;
    public GameObject sinSin;
    public GameObject sinDescription;
    public GameObject element1;
    public GameObject element2;
    public GameObject fortaleza;
    public GameObject debilidad;
    public Map mapReference;

    void Start()
    {
        List<string> sinData = SQLManager.Instance.Query($"SELECT * FROM Pecados P WHERE P.Pecado_ID = '{SQLManager.Instance.sinSelectedFromMenu}'");
        sinName.GetComponent<Text>().text = sinData[1];
        sinSin.GetComponent<Text>().text = sinData[2];
        sinDescription.GetComponent<Text>().text = sinData[3];
        element1.GetComponent<Text>().text = SQLManager.Instance.Query($"SELECT Nombre FROM Elementos E WHERE E.Elemento_ID = '{sinData[4]}'")[0];
        element2.GetComponent<Text>().text = SQLManager.Instance.Query($"SELECT Nombre FROM Elementos E WHERE E.Elemento_ID = '{sinData[5]}'")[0];
        fortaleza.GetComponent<Text>().text = sinData[7];
        debilidad.GetComponent<Text>().text = sinData[8];

        GameObject model = (GameObject)Instantiate(Resources.Load($"Models/{sinData[2]}/{sinData[2]}_Prefab"), transform.position, Quaternion.identity, transform);
        if (sinData[2] != "Pereza") model.transform.rotation = Quaternion.Euler(0, 180, 0);
        else model.transform.rotation = Quaternion.Euler(0, -90, 0);
    }
}