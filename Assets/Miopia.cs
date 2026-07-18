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

            // Visión normal
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
            // Miopía muy exagerada
            dof.gaussianStart.value = 0f;
dof.gaussianEnd.value = 0.5f;
dof.gaussianMaxRadius.value = 2f;
        }
    }
}