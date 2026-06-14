using System.Collections;
using UnityEngine;
using TMPro;

public class LensOption : MonoBehaviour
{
    [Header("Configuracion")]
    public bool isCorrectOption;

    [Header("Autos")]
    public Renderer auto1Renderer;
    public Renderer auto2Renderer;
    public Renderer auto3Renderer;

    public Material auto1CorrectMaterial;
    public Material auto2CorrectMaterial;
    public Material auto3CorrectMaterial;

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
        Debug.Log("TOQUE: " + gameObject.name);

        if (!other.CompareTag("Player"))
        {
            Debug.Log("NO ES PLAYER -> " + other.name);
            return;
        }

        if (!canChoose)
        {
            Debug.Log("BLOQUEADO -> " + gameObject.name);
            return;
        }

        if (isCorrectOption)
        {
            Debug.Log("CORRECTA -> " + gameObject.name);
            CorrectChoice();
        }
        else
        {
            Debug.Log("INCORRECTA -> " + gameObject.name);
            StartCoroutine(WrongChoice());
        }
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

        if (auto1Renderer != null && auto1CorrectMaterial != null)
            auto1Renderer.material = auto1CorrectMaterial;

        if (auto2Renderer != null && auto2CorrectMaterial != null)
            auto2Renderer.material = auto2CorrectMaterial;

        if (auto3Renderer != null && auto3CorrectMaterial != null)
            auto3Renderer.material = auto3CorrectMaterial;
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