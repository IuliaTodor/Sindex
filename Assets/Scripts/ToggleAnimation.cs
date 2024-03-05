using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleAnimation : MonoBehaviour
{
    [SerializeField] private InputAction pressed;
    private Action<InputAction.CallbackContext> performHandler;
    private Action<InputAction.CallbackContext> cancelHandler;

    private void Awake()
    {
        pressed.Enable();

        void performed(InputAction.CallbackContext _) { StartCoroutine(PlayAnimation()); }
        performHandler = performed;

        void canceled(InputAction.CallbackContext _) { }
        cancelHandler = canceled;

        //Cuando pressed es activado llama a la corutina. No le pasamos ninguna variable 
        pressed.performed += performHandler;
        //El objeto ya no rota cuando soltamos pressed
        pressed.canceled += cancelHandler;
    }

    private void OnDisable()
    {
        pressed.performed -= performHandler;
        pressed.canceled -= cancelHandler;
    }

    private IEnumerator PlayAnimation()
    {
        RaycastHit hit;

        Ray ray = new Ray(transform.position, transform.forward);
        Debug.Log("Mouse");
        //Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 2f);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log("Raycast");
            if (hit.collider.gameObject.CompareTag("3DModel"))
            {
                Debug.Log("Trigger");
                hit.collider.gameObject.GetComponent<Animator>().SetTrigger("isTouched");
            }
        }

        yield return null;
    }
}
