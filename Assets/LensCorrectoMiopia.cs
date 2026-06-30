using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LensCorrectoMiopia : MonoBehaviour
{
    public bool esCorrecto = false;

    public Volume globalVolume;

    [Header("Mensajes")]
    public GameObject cartelCorrecto;
    public GameObject cartelIncorrecto;

    [Header("Sonidos")]
    public AudioClip sonidoCorrecto;
    public AudioClip sonidoIncorrecto;

    private AudioSource audioSource;
    private DepthOfField dof;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (globalVolume != null)
            globalVolume.profile.TryGet(out dof);
    }

    public void Seleccionar()
    {
        if (esCorrecto)
        {
            if (dof != null)
            {
                dof.gaussianStart.value = 50f;
                dof.gaussianEnd.value = 100f;
                dof.gaussianMaxRadius.value = 0f;
            }

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