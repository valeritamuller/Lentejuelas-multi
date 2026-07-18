using UnityEngine;
using UnityEngine.Rendering;

public class LensCorrectoAstigmatismo : MonoBehaviour
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

    // Guarda la última lente seleccionada
    private static LensCorrectoAstigmatismo ultimaSeleccion;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Seleccionar()
    {
        // Cancela el temporizador anterior y oculta el cartel que estaba visible
        if (ultimaSeleccion != null)
        {
            ultimaSeleccion.CancelInvoke();
            ultimaSeleccion.OcultarTodosLosCarteles();
        }

        ultimaSeleccion = this;

        // Por seguridad, oculta ambos carteles antes de mostrar el nuevo
        OcultarTodosLosCarteles();

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