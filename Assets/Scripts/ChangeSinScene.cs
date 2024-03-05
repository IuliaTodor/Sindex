using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSinScene : MonoBehaviour
{
    public void ChangeSin(int sin)
    {
        SQLManager.Instance.sinSelectedFromMenu = sin;
    }
}
