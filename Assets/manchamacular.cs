using System.Collections;
using UnityEngine;
using TMPro;

public class LensOptionMacular : MonoBehaviour
{
    [Header("Configuracion")]
    public bool isCorrectOption;

    [Header("Mancha Macular")]
    public GameObject manchaMacular;

    [Header("Zona de activacion")]
    public Transform jugador;
    public Transform zonaTarea;
    public float distanciaActivacion = 10f;

    [Header("Mensajes")]
    public GameObject wrongMessage;
    public GameObject correctMessage;

    [Header("Countdown")]
    public TextMeshProUGUI countdownText;

    [Header("Sonidos")]
    public AudioClip correctSound;
    public AudioClip wrongSound;

    private AudioSource audioSource;
    private bool canChoose = true;
    private bool manchaActivada = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (correctMessage != null)
            correctMessage.SetActive(false);

        if (wrongMessage != null)
            wrongMessage.SetActive(false);

        if (countdownText != null)
            countdownText.text = "";

        // Mancha desactivada al inicio
        if (manchaMacular != null)
            manchaMacular.SetActive(false);
    }

    private void Update()
    {
        if (manchaActivada) return;
        if (jugador == null || zonaTarea == null) return;

        float distancia = Vector3.Distance(jugador.position, zonaTarea.position);

        if (distancia < distanciaActivacion)
        {
            manchaActivada = true;
            if (manchaMacular != null)
                manchaMacular.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("MainCamera")) return;
        if (!canChoose) return;

        if (isCorrectOption)
            CorrectChoice();
        else
            StartCoroutine(WrongChoice());
    }

    private void CorrectChoice()
    {
        canChoose = false;

        if (wrongMessage != null)
            wrongMessage.SetActive(false);

        if (countdownText != null)
            countdownText.text = "";

        if (audioSource != null && correctSound != null)
            audioSource.PlayOneShot(correctSound);

        if (correctMessage != null)
            correctMessage.SetActive(true);

        // Desaparece la mancha
        if (manchaMacular != null)
            manchaMacular.SetActive(false);
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
                countdownText.text = "Intentá de nuevo en " + i;

            yield return new WaitForSeconds(1f);
        }

        if (countdownText != null)
            countdownText.text = "";

        if (wrongMessage != null)
            wrongMessage.SetActive(false);

        canChoose = true;
    }
}