using System.Collections;
using UnityEngine;
using TMPro;

public class Tarea_Cafeteria : MonoBehaviour
{
    [Header("Configuracion - Marcar solo en el lente MIOPIA")]
    public bool isCorrectOption; // Tildas TRUE solo en el lente Miopia

    [Header("Menu - Texturas")]
    public Renderer menuRenderer;           // El plano del menu en la escena
    public Material menuBorrosoMaterial;    // Material con el menu blureado
    public Material menuNitidoMaterial;     // Material con el menu nitido y claro

    [Header("Mensajes")]
    public GameObject wrongMessage;         // Mensaje "Incorrecto, esperá"
    public GameObject correctMessage;       // Mensaje "¡Correcto! Ahora podés leer el menú"

    [Header("Countdown")]
    public TextMeshProUGUI countdownText;

    [Header("Sonidos")]
    public AudioClip correctSound;
    public AudioClip wrongSound;

    private AudioSource audioSource;
    private Collider myCollider;

    private static bool canChoose = true;
    private static bool alreadyCorrect = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        myCollider = GetComponent<Collider>();

        // Al inicio el menu arranca borrosoo
        if (menuRenderer != null && menuBorrosoMaterial != null)
            menuRenderer.material = menuBorrosoMaterial;

        if (correctMessage != null)
            correctMessage.SetActive(false);

        if (wrongMessage != null)
            wrongMessage.SetActive(false);

        if (countdownText != null)
            countdownText.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("MainCamera"))
            return;

        if (!canChoose || alreadyCorrect)
            return;

        if (isCorrectOption)
            CorrectChoice();
        else
            StartCoroutine(WrongChoice());
    }

    private void CorrectChoice()
    {
        canChoose = false;
        alreadyCorrect = true;

        // Saca el mensaje de error si habia uno
        if (wrongMessage != null)
            wrongMessage.SetActive(false);

        if (countdownText != null)
            countdownText.text = "";

        // Sonido correcto
        if (audioSource != null && correctSound != null)
            audioSource.PlayOneShot(correctSound);

        // Muestra mensaje de correcto
        if (correctMessage != null)
            correctMessage.SetActive(true);

        // EL MENU APARECE NITIDO
        if (menuRenderer != null && menuNitidoMaterial != null)
            menuRenderer.material = menuNitidoMaterial;

        Debug.Log("CORRECTO - Miopia seleccionada, menu ahora visible");
    }

    private IEnumerator WrongChoice()
    {
        canChoose = false;

        if (correctMessage != null)
            correctMessage.SetActive(false);

        // Sonido incorrecto
        if (audioSource != null && wrongSound != null)
            audioSource.PlayOneShot(wrongSound);

        // Muestra mensaje de error
        if (wrongMessage != null)
            wrongMessage.SetActive(true);

        // Countdown de 5 segundos
        for (int i = 5; i > 0; i--)
        {
            if (countdownText != null)
                countdownText.text = "Intentá de nuevo en " + i + "...";

            yield return new WaitForSeconds(1f);
        }

        if (countdownText != null)
            countdownText.text = "";

        if (wrongMessage != null)
            wrongMessage.SetActive(false);

        canChoose = true;

        Debug.Log("DESBLOQUEADO - Puede volver a elegir");
    }
}