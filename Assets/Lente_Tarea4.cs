using System.Collections;
using UnityEngine;
using TMPro;

public class Tarea4 : MonoBehaviour
{
    [Header("Configuracion")]
    public bool isCorrectOption;

    [Header("Bondis")]
    public Renderer Bondi1Renderer;
    public Renderer Bondi2Renderer;
    public Renderer Bondi3Renderer;

    public Material Bondi1CorrectMaterial;
    public Material Bondi2CorrectMaterial;
    public Material Bondi3CorrectMaterial;

    [Header("Mensajes")]
    public GameObject wrongMessage;
    public GameObject correctMessage;

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

        if (correctMessage != null)
            correctMessage.SetActive(false);

        if (wrongMessage != null)
            wrongMessage.SetActive(false);

        if (countdownText != null)
            countdownText.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("----- TRIGGER -----");
        Debug.Log("Opción activada: " + gameObject.name);
        Debug.Log("Collider que entró: " + other.name);
        Debug.Log("Tag del que entró: " + other.tag);
        Debug.Log("Posición opción: " + transform.position);

        if (myCollider != null)
            Debug.Log("Centro del collider opción: " + myCollider.bounds.center + " / Tamaño: " + myCollider.bounds.size);

        Debug.Log("Posición del que entró: " + other.transform.position);
        Debug.Log("-------------------");

        if (!other.CompareTag("MainCamera"))
            return;

        if (!canChoose || alreadyCorrect)
        {
            Debug.Log("BLOQUEADO -> " + gameObject.name);
            return;
        }

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

        if (Bondi1Renderer != null && Bondi1CorrectMaterial != null)
            Bondi1Renderer.material = Bondi1CorrectMaterial;

        if (Bondi2Renderer != null && Bondi2CorrectMaterial != null)
            Bondi2Renderer.material = Bondi2CorrectMaterial;

        if (Bondi3Renderer != null && Bondi3CorrectMaterial != null)
            Bondi3Renderer.material = Bondi3CorrectMaterial;

        Debug.Log("CORRECTA -> " + gameObject.name);
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

        Debug.Log("DESBLOQUEADO");
    }
}