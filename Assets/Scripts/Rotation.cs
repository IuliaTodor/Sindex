using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{

    void FixedUpdate()
    {
        transform.Rotate(0, 20 * Time.deltaTime, 0, Space.Self);
    }
}
