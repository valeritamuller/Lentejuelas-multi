using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AstigmatismoZona : MonoBehaviour
{
    public Volume globalVolume;
    private Bloom bloom;

    void Start()
    {
        globalVolume.profile.TryGet(out bloom);

        if (bloom != null)
        {
            bloom.active = false;
            bloom.intensity.value = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ZONA ASTIGMATISMO por: " + other.name);

        if (bloom != null)
        {
            bloom.active = true;
            bloom.intensity.value = 20f;
        }
    }
}
