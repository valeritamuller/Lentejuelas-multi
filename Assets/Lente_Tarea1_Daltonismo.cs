using UnityEngine;
using System.Collections;
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

    private static bool canChoose = true;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (!canChoose)
            return;

        if (isCorrectOption)
            CorrectChoice();
        else
            StartCoroutine(WrongChoice());
    }

    private void CorrectChoice()
    {
        canChoose = false;

        if (correctSound != null)
            audioSource.PlayOneShot(correctSound);

        if (correctMessage != null)
            correctMessage.SetActive(true);

        auto1Renderer.material = auto1CorrectMaterial;
        auto2Renderer.material = auto2CorrectMaterial;
        auto3Renderer.material = auto3CorrectMaterial;
    }

    private IEnumerator WrongChoice()
    {
        canChoose = false;

        if (wrongSound != null)
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