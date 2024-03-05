using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleAnimation : MonoBehaviour
{
    [SerializeField] private InputAction pressed;
    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Debug.Log("Mouse");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log("Raycast");
                if (hit.collider.gameObject.CompareTag("3DModel"))
                {
                    Debug.Log("Trigger");
                    hit.collider.gameObject.GetComponent<Animator>().SetTrigger("isTouched");
                }
            }
        }
    }
}
