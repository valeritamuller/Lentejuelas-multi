using UnityEngine;

public class LenteSeleccionable : MonoBehaviour
{
    public bool esCorrecto;

    public void Seleccionar()
    {
        if (esCorrecto)
            Debug.Log("Lente correcto");
        else
            Debug.Log("Lente incorrecto");
    }
}