using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJoystick : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Camera camara;

    [Header("Movimiento")]
    [SerializeField] private float velocidad = 4f;

    [Header("Cámara")]
    [SerializeField] private float sensibilidadHorizontal = 120f;
    [SerializeField] private float sensibilidadVertical = 80f;
    [SerializeField] private float limiteVertical = 70f;

    [Header("Salto")]
    [SerializeField] private float fuerzaSalto = 7f;
    [SerializeField] private float gravedad = -20f;

    [Header("Selección")]
    [SerializeField] private float distanciaSeleccion = 30f;
    [SerializeField] private float radioSeleccion = 0.25f;

    private float anguloVertical;
    private float velocidadVertical;

    private void Awake()
    {
        if (characterController == null)
            characterController = GetComponent<CharacterController>();

        if (camara == null)
            camara = Camera.main;

        if (camara != null)
        {
            Vector3 rotacionInicial = camara.transform.localEulerAngles;

            if (rotacionInicial.x > 180f)
                rotacionInicial.x -= 360f;

            anguloVertical = rotacionInicial.x;
        }
    }

    private void Update()
    {
        if (Gamepad.current == null)
            return;

        Mover();
        Mirar();
        SaltarYAplicarGravedad();

        // Triángulo en PlayStation / Y en Xbox
        if (Gamepad.current.buttonNorth.wasPressedThisFrame)
            SeleccionarOpcion();
    }

    private void Mover()
    {
        if (characterController == null || camara == null)
            return;

        Vector2 entrada = Gamepad.current.leftStick.ReadValue();

        Vector3 adelante = camara.transform.forward;
        Vector3 derecha = camara.transform.right;

        adelante.y = 0f;
        derecha.y = 0f;

        adelante.Normalize();
        derecha.Normalize();

        Vector3 direccion =
            adelante * entrada.y +
            derecha * entrada.x;

        if (direccion.sqrMagnitude > 1f)
            direccion.Normalize();

        characterController.Move(
            direccion * velocidad * Time.deltaTime
        );
    }

    private void Mirar()
    {
        if (camara == null)
            return;

        Vector2 entrada = Gamepad.current.rightStick.ReadValue();

        // Izquierda y derecha: rota todo el XR Origin
        transform.Rotate(
            0f,
            entrada.x * sensibilidadHorizontal * Time.deltaTime,
            0f
        );

        // Arriba y abajo: rota la Main Camera
        anguloVertical -=
            entrada.y * sensibilidadVertical * Time.deltaTime;

        anguloVertical = Mathf.Clamp(
            anguloVertical,
            -limiteVertical,
            limiteVertical
        );

        camara.transform.localRotation =
            Quaternion.Euler(anguloVertical, 0f, 0f);
    }

    private void SaltarYAplicarGravedad()
    {
        if (characterController == null)
            return;

        if (characterController.isGrounded &&
            velocidadVertical < 0f)
        {
            velocidadVertical = -2f;
        }

        // X en PlayStation / A en Xbox
        if (characterController.isGrounded &&
            Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            velocidadVertical = fuerzaSalto;
        }

        velocidadVertical += gravedad * Time.deltaTime;

        characterController.Move(
            Vector3.up * velocidadVertical * Time.deltaTime
        );
    }

    private void SeleccionarOpcion()
    {
        if (camara == null)
            return;

        Ray ray = new Ray(
            camara.transform.position,
            camara.transform.forward
        );

        Debug.DrawRay(
            ray.origin,
            ray.direction * distanciaSeleccion,
            Color.red,
            2f
        );

        bool detecto = Physics.SphereCast(
            ray,
            radioSeleccion,
            out RaycastHit hit,
            distanciaSeleccion,
            ~0,
            QueryTriggerInteraction.Collide
        );

        if (!detecto)
        {
            Debug.LogWarning(
                "Triángulo: no se detectó ningún objeto."
            );
            return;
        }

        Debug.Log(
            "Triángulo detectó: " + hit.collider.name
        );

        LensCorrectoMiopia miopia =
            hit.collider.GetComponentInParent<LensCorrectoMiopia>();

        if (miopia != null)
        {
            miopia.Seleccionar();
            return;
        }

        LensCorrectoDegeneracion degeneracion =
            hit.collider.GetComponentInParent<LensCorrectoDegeneracion>();

        if (degeneracion != null)
        {
            degeneracion.Seleccionar();
            return;
        }

        LensCorrectoDaltonismo daltonismo =
            hit.collider.GetComponentInParent<LensCorrectoDaltonismo>();

        if (daltonismo != null)
        {
            daltonismo.Seleccionar();
            return;
        }

        LensCorrectoAstigmatismo astigmatismo =
            hit.collider.GetComponentInParent<LensCorrectoAstigmatismo>();

        if (astigmatismo != null)
        {
            astigmatismo.Seleccionar();
            return;
        }

        Debug.LogWarning(
            "Detectó el objeto, pero no encontró un script LensCorrecto."
        );
    }
}