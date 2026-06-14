using System.Collections;
using UnityEngine;

public class LenteTarea2 : MonoBehaviour
{
    public bool isCorrectOption;

    [Header("Mancha")]
    public GameObject mancha;

    [Header("Carteles")]
    public GameObject cartelIncorrecto;
    public GameObject cartelCorrecto;

    [Header("Sonidos")]
    public AudioClip sonidoIncorrecto;
    public AudioClip sonidoCorrecto;

    private AudioSource audioSource;
    private static bool canChoose = true;
    private static bool finished = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (cartelIncorrecto != null)
            cartelIncorrecto.SetActive(false);

        if (cartelCorrecto != null)
            cartelCorrecto.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("MainCamera"))
            return;

        if (!canChoose || finished)
            return;

        if (isCorrectOption)
        {
            Correcto();
        }
        else
        {
            StartCoroutine(Incorrecto());
        }
    }

    private void Correcto()
    {
        canChoose = false;
        finished = true;

        if (cartelIncorrecto != null)
            cartelIncorrecto.SetActive(false);

        if (cartelCorrecto != null)
            cartelCorrecto.SetActive(true);

        if (mancha != null)
            mancha.SetActive(false);

        if (audioSource != null && sonidoCorrecto != null)
            audioSource.PlayOneShot(sonidoCorrecto);

        Debug.Log("CORRECTO -> " + gameObject.name);
    }

    private IEnumerator Incorrecto()
    {
        canChoose = false;

        if (cartelCorrecto != null)
            cartelCorrecto.SetActive(false);

        if (cartelIncorrecto != null)
            cartelIncorrecto.SetActive(true);

        if (audioSource != null && sonidoIncorrecto != null)
            audioSource.PlayOneShot(sonidoIncorrecto);

        Debug.Log("INCORRECTO -> " + gameObject.name);

        yield return new WaitForSeconds(3f);

        if (cartelIncorrecto != null)
            cartelIncorrecto.SetActive(false);

        canChoose = true;
    }
}