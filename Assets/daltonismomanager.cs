using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DaltonismoManager : MonoBehaviour
{
    public Volume globalVolume;
    private ColorAdjustments colorAdj;
    private WhiteBalance whiteBalance;

    void Start()
    {
        globalVolume.profile.TryGet(out colorAdj);
        globalVolume.profile.TryGet(out whiteBalance);
        
        // Empieza con daltonismo activo
        AplicarDaltonismo(true);
    }

    // Llamá esto cuando elijan el lente CORRECTO
    public void LenteCorrecto()
    {
        AplicarDaltonismo(false);
    }

    // Llamá esto cuando elijan un lente INCORRECTO
    public void LenteIncorrecto()
    {
        AplicarDaltonismo(true);
    }

    void AplicarDaltonismo(bool activo)
    {
        if (colorAdj != null)
        {
            colorAdj.active = activo;
            colorAdj.saturation.value = activo ? -60f : 0f;
            colorAdj.hueShift.value = activo ? 30f : 0f;
        }
        if (whiteBalance != null)
        {
            whiteBalance.active = activo;
            whiteBalance.temperature.value = activo ? -20f : 0f;
        }
    }
}