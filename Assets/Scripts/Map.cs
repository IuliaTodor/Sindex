using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class Map : MonoBehaviour
{
    // Enables/disables a portion of the map
    public void ColorChange(List<string> regionNames)
    {
        List<RawImage> regions = new();
        for (int i = 0; i < transform.childCount; i++)
        {
            regions.Add(transform.GetChild(i).Find("DataContainer").GetChild(0).GetComponent<RawImage>());
        }
        ApplyColor(regions, regionNames);
    }

    public void MapColorChange(string regionName)
    {
        ApplyColor(GetComponentsInChildren<Image>().ToList(), regionName);
    }

    // Click events to send to the dex filter, filtering by area (its actually by id but shh)
    public void AreaToDex(string region)
    {
        if (!SQLManager.Instance) return;
        SQLManager.Instance.areaSelectedToMenu = new() { region };
    }

    // Apply color for map elements
    public void ApplyColor(List<Image> regions, string regionName)
    {
        // Change the color
        foreach (var image in regions)
        {
            if (image.name.Contains("Event")) continue;
            if (regionName.Contains(image.name)) image.color = Color.white;
            else image.color = new Color(0.3f, 0.3f, 0.3f);
        }
    }

    // Apply colors for GAME MENU elements
    public void ApplyColor(List<RawImage> regions, List<string> regionNames)
    {
        // Change the color
        foreach (var image in regions)
        {
            if (regionNames == null || regionNames.Count == 0) { image.color = new Color(0f, 0f, 0f); continue;  }
            if (regionNames.Contains(image.name.Replace("Image", ""))) image.color = Color.white;
            else image.color = new Color(0f, 0f, 0f);
        }
    }
}
