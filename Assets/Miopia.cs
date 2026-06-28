using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Miopia : MonoBehaviour
{
    [Header("Post Processing")]
    public Volume postProcessingVolume;
    public float bloomIntensidad = 15f;
    public float vignetteIntensidad = 0f;

    [Header("Distancia")]
    public Transform jugador; // Arrastrás el XR Origin (VR)
    public float distanciaActivacion = 10f;

    private Bloom bloom;
    private Vignette vignette;
    private bool activado = false;

    private void Awake()
    {
        if (postProcessingVolume != null)
        {
            if (postProcessingVolume.profile.TryGet<Bloom>(out bloom))
                bloom.intensity.value = 0f;

            if (postProcessingVolume.profile.TryGet<Vignette>(out vignette))
                vignette.intensity.value = 0f;
        }
    }

    private void Update()
    {
        if (activado || jugador == null) return;

        float distancia = Vector3.Distance(transform.position, jugador.position);

        if (distancia < distanciaActivacion)
        {
            activado = true;

            if (bloom != null)
                bloom.intensity.value = bloomIntensidad;

            if (vignette != null)
                vignette.intensity.value = vignetteIntensidad;

            Debug.Log("BLUR ACTIVADO - distancia: " + distancia);
        }
    }
}
