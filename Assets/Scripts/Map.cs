using UnityEngine;

public class Map : MonoBehaviour
{
    public void ColorChange(string region)
    {
        Debug.Log(region);
        UnityEngine.UI.Image[] regions = GetComponentsInChildren<UnityEngine.UI.Image>();
        foreach (UnityEngine.UI.Image child in regions)
        {
            if (child.name.Contains("Event")) continue;
            if (child.name == region) child.color = Color.white;
            else child.color = new Color(0.3f, 0.3f, 0.3f);
        }
    }
}
