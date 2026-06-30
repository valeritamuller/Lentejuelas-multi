using UnityEngine;

public class LensCorrectoDegeneracion : MonoBehaviour
{
    public bool esCorrecto = false;

    public GameObject mancha;

    [Header("Mensajes")]
    public GameObject cartelCorrecto;
    public GameObject cartelIncorrecto;

    [Header("Sonidos")]
    public AudioClip sonidoCorrecto;
    public AudioClip sonidoIncorrecto;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Seleccionar()
    {
        if (esCorrecto)
        {
            if (mancha != null)
                mancha.SetActive(false);

            if (cartelCorrecto != null)
            {
                cartelCorrecto.SetActive(true);
                Invoke(nameof(OcultarCartelCorrecto), 5f);
            }

            if (audioSource != null && sonidoCorrecto != null)
                audioSource.PlayOneShot(sonidoCorrecto);
        }
        else
        {
            if (cartelIncorrecto != null)
            {
                cartelIncorrecto.SetActive(true);
                Invoke(nameof(OcultarCartelIncorrecto), 5f);
            }

            if (audioSource != null && sonidoIncorrecto != null)
                audioSource.PlayOneShot(sonidoIncorrecto);
        }
    }

    void OcultarCartelCorrecto()
    {
        if (cartelCorrecto != null)
            cartelCorrecto.SetActive(false);
    }

    void OcultarCartelIncorrecto()
    {
        if (cartelIncorrecto != null)
            cartelIncorrecto.SetActive(false);
    }
}