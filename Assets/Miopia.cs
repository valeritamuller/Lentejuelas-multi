using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Miopia : MonoBehaviour
{
    public Volume globalVolume;
    private DepthOfField dof;

    void Start()
    {
        globalVolume.profile.TryGet(out dof);

        if (dof != null)
        {
            dof.active = true;
            dof.gaussianStart.value = 50f;
            dof.gaussianEnd.value = 100f;
            dof.gaussianMaxRadius.value = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ZONA MIOPIA por: " + other.name);

        if (!other.CompareTag("MainCamera")) return;

        if (dof != null)
        {
            dof.gaussianStart.value = 1f;
            dof.gaussianEnd.value = 8f;
            dof.gaussianMaxRadius.value = 0.7f;
        }
    }
}