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
        Ray ray = new(transform.position, transform.forward);
        //Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 2f);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.CompareTag("3DModel"))
            {
                Animator anim = hit.collider.gameObject.GetComponent<Animator>();
                if (hit.collider.name == "Lujuria_Prefab") SoundManager.instance.Play("carlos");
                anim.SetTrigger("IsTouched");
            }
        }

        yield return null;
    }
}
