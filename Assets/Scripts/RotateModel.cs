using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateModel : MonoBehaviour
{
    //Los input action que necesitamos. Pressed es para el raton/touch y Axis para saber el eje del raton/touch
    [SerializeField] private InputAction pressed, axis;

    private Transform cam;

    [SerializeField] private float speed = 0.5f;
    /// <summary>
    /// Controla si debe invertir la rotacion o no
    /// </summary>
    [SerializeField] private bool inverted;
    private bool canRotate;

    private Vector2 rotation;
    private Action<InputAction.CallbackContext> performHandler;
    private Action<InputAction.CallbackContext> cancelHandler;

    private void Awake()
    {
        cam = Camera.main.transform;
        pressed.Enable();
        axis.Enable();

        void performed(InputAction.CallbackContext _) { StartCoroutine(Rotate()); }
        performHandler = performed;

        void canceled(InputAction.CallbackContext _) { canRotate = false; }
        cancelHandler = canceled;

        //Cuando pressed es activado llama a la corutina. No le pasamos ninguna variable 
        pressed.performed += performHandler;
        //El objeto ya no rota cuando soltamos pressed
        pressed.canceled += cancelHandler;
        //pressed.canceled += _ => { transform.rotation = Quaternion.identity; };
        //Le pasamos un Vector2 a la rotacion, que equivaldra a la posicion del raton/touch
        axis.performed += touchVector => { rotation = touchVector.ReadValue<Vector2>(); };
    }

    private void OnDisable()
    {
        pressed.performed -= performHandler;
        pressed.canceled -= cancelHandler;
    }

    /// <summary>
    /// Activa la rotacion del objeto
    /// </summary>
    /// <returns></returns>
    private IEnumerator Rotate()
    {
        canRotate = true;

        while (canRotate)
        {
            rotation *= speed;

                // Rota en el eje Y
                if (inverted)
                {
                    transform.Rotate(Vector3.up, rotation.x, Space.World);
                }
                else
                {
                    transform.Rotate(Vector3.up, -rotation.x, Space.World);
                }

                // Rota en el eje X
                if (inverted)
                {
                    transform.Rotate(cam.right, -rotation.y, Space.World);
                }
                else
                {
                    transform.Rotate(cam.right, rotation.y, Space.World);
                }
            yield return null;
        }

    }

  
}
