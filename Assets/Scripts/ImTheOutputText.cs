using UnityEngine;
using UnityEngine.UI;

public class ImTheOutputText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SQLManager.Instance) SQLManager.Instance.outputText = GetComponent<Text>();
    }
}
