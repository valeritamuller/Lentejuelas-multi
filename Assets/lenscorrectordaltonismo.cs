using UnityEngine;

public class LensCorrectoDaltonismo : MonoBehaviour
{
    public bool esCorrecto = false;
    public DaltonismoManager daltonismoManager;

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

        if (audioSource == null)
            Debug.LogWarning("No hay AudioSource en " + gameObject.name);
    }

    public void Seleccionar()
    {
        if (daltonismoManager == null)
        {
            Debug.LogWarning("Falta asignar DaltonismoManager en " + gameObject.name);
            return;
        }

        if (esCorrecto)
        {
            daltonismoManager.LenteCorrecto();

            if (cartelCorrecto != null)
                cartelCorrecto.SetActive(true);

            if (audioSource != null && sonidoCorrecto != null)
                audioSource.PlayOneShot(sonidoCorrecto);
        }
        else
        {
            daltonismoManager.LenteIncorrecto();

            if (cartelIncorrecto != null)
                cartelIncorrecto.SetActive(true);

            if (audioSource != null && sonidoIncorrecto != null)
                audioSource.PlayOneShot(sonidoIncorrecto);
        }
    }
}