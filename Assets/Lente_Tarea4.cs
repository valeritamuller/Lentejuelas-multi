using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Lente_tarea4 : MonoBehaviour
{
    [Header("Configuracion")]
    public bool isCorrectOption;

    [Header("Post Processing")]
    public Volume globalVolume;

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
    private Bloom bloom;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (correctMessage != null)
            correctMessage.SetActive(false);

        if (wrongMessage != null)
            wrongMessage.SetActive(false);

        if (countdownText != null)
            countdownText.text = "";

        if (globalVolume != null)
            globalVolume.profile.TryGet(out bloom);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("LENTE TOCADO: " + gameObject.name + " por: " + other.name + " tag: " + other.tag);

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

        if (bloom != null)
        {
            bloom.active = false;
            bloom.intensity.value = 0f;
        }

        Debug.Log("CORRECTO");
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