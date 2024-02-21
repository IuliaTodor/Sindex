using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateModel : MonoBehaviour
{
    //Los input action que necesitamos. Pressed es para el ratón/touch y Axis para saber el eje del ratón/touch
    [SerializeField] private InputAction pressed, axis;

    private Transform cam;

    [SerializeField] private float speed = 0.5f;
    /// <summary>
    /// Controla si debe invertir la rotación o no
    /// </summary>
    [SerializeField] private bool inverted;
    private bool canRotate;

    private Vector2 rotation;

    private void Awake()
    {
        cam = Camera.main.transform;
        pressed.Enable();
        axis.Enable();

        //Cuando pressed es activado llama a la corutina. No le pasamos ninguna variable
        pressed.performed += _ => { StartCoroutine(Rotate()); };
        //El objeto ya no rota cuando soltamos pressed
        pressed.canceled += _ => { canRotate = false; };
        //pressed.canceled += _ => { transform.rotation = Quaternion.identity; };
        //Le pasamos un Vector2 a la rotación, que equivaldrá a la posición del ratón/touch
        axis.performed += touchVector => { rotation = touchVector.ReadValue<Vector2>(); };
    }

    /// <summary>
    /// Activa la rotación del objeto
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
