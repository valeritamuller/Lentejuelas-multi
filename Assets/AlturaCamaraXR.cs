using UnityEngine;

[DefaultExecutionOrder(10000)]
public class AlturaCamaraXR : MonoBehaviour
{
    [SerializeField] private Transform cameraOffset;
    [SerializeField] private float altura = 1.7f;

    private void Awake()
    {
        if (cameraOffset == null)
        {
            Transform encontrado = transform.Find("Camera Offset");

            if (encontrado != null)
                cameraOffset = encontrado;
        }
    }

    private void Start()
    {
        AplicarAltura();
    }

    private void LateUpdate()
    {
        AplicarAltura();
    }

    private void AplicarAltura()
    {
        if (cameraOffset == null)
            return;

        Vector3 posicion = cameraOffset.localPosition;
        posicion.y = altura;
        cameraOffset.localPosition = posicion;
    }
}