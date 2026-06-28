using System.Collections;
using UnityEngine;
using TMPro;

public class Tarea_Cafeteria : MonoBehaviour
{
    [Header("Configuracion - Marcar solo en el lente MIOPIA")]
    public bool isCorrectOption;

    [Header("Menu - Textura nitida")]
    public Renderer menuRenderer;
    public Material menuNitidoMaterial;

    [Header("Mensajes")]
    public GameObject wrongMessage;
    public GameObject correctMessage;

    [Header("Countdown")]
    public TextMeshProUGUI countdownText;

    [Header("Sonidos")]
    public AudioClip correctSound;
    public AudioClip wrongSound;

    private AudioSource audioSource;

    private static bool canChoose = true;
    private static bool alreadyCorrect = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

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

        if (wrongMessage != null)
            wrongMessage.SetActive(false);

        if (countdownText != null)
            countdownText.text = "";

        if (audioSource != null && correctSound != null)
            audioSource.PlayOneShot(correctSound);

        if (correctMessage != null)
            correctMessage.SetActive(true);

        if (menuRenderer != null && menuNitidoMaterial != null)
            menuRenderer.material = menuNitidoMaterial;

        Debug.Log("CORRECTO - Miopia seleccionada, menu ahora visible");
    }

    private IEnumerator WrongChoice()
    {
        canChoose = false;

        if (correctMessage != null)
            correctMessage.SetActive(false);

        if (audioSource != null && wrongSound != null)
            audioSource.PlayOneShot(wrongSound);

        if (wrongMessage != null)
            wrongMessage.SetActive(true);

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