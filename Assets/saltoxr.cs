using UnityEngine;
using UnityEngine.InputSystem;

public class SaltoXR : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;

    [Header("Salto")]
    [SerializeField] private float fuerzaSalto = 5f;
    [SerializeField] private float gravedad = -15f;

    private float velocidadVertical;

    private void Awake()
    {
        if (characterController == null)
            characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (characterController == null)
        {
            Debug.LogError("No se encontró Character Controller.");
            return;
        }

        bool tocarSalto = false;

        // Teclado
        if (Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            tocarSalto = true;
        }

        // Joystick: A en Xbox / X en PlayStation
        if (Gamepad.current != null &&
            Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            tocarSalto = true;
        }

        if (characterController.isGrounded)
        {
            if (velocidadVertical < 0f)
                velocidadVertical = -2f;

            if (tocarSalto)
            {
                velocidadVertical = fuerzaSalto;
                Debug.Log("SALTO");
            }
        }

        velocidadVertical += gravedad * Time.deltaTime;

        characterController.Move(
            Vector3.up * velocidadVertical * Time.deltaTime
        );
    }
}