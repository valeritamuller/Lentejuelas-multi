using UnityEngine;
using UnityEngine.Rendering;

public class LensCorrectoAstigmatismo : MonoBehaviour
{
    public bool esCorrecto = false;
    public Volume globalVolume;

    public GameObject cartelCorrecto;
    public GameObject cartelIncorrecto;

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
            if (globalVolume != null)
                globalVolume.enabled = false;

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