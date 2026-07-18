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

    // Guarda la última opción seleccionada
    private static LensCorrectoDaltonismo ultimaSeleccion;

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

        // Oculta el mensaje anterior y cancela su temporizador
        if (ultimaSeleccion != null)
        {
            ultimaSeleccion.CancelInvoke();
            ultimaSeleccion.OcultarTodosLosCarteles();
        }

        ultimaSeleccion = this;

        // Evita que los dos carteles estén visibles juntos
        OcultarTodosLosCarteles();

        if (esCorrecto)
        {
            daltonismoManager.LenteCorrecto();

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
            daltonismoManager.LenteIncorrecto();

            if (cartelIncorrecto != null)
            {
                cartelIncorrecto.SetActive(true);
                Invoke(nameof(OcultarCartelIncorrecto), 5f);
            }

            if (audioSource != null && sonidoIncorrecto != null)
                audioSource.PlayOneShot(sonidoIncorrecto);
        }
    }

    private void OcultarTodosLosCarteles()
    {
        if (cartelCorrecto != null)
            cartelCorrecto.SetActive(false);

        if (cartelIncorrecto != null)
            cartelIncorrecto.SetActive(false);
    }

    private void OcultarCartelCorrecto()
    {
        if (cartelCorrecto != null)
            cartelCorrecto.SetActive(false);
    }

    private void OcultarCartelIncorrecto()
    {
        if (cartelIncorrecto != null)
            cartelIncorrecto.SetActive(false);
    }
}