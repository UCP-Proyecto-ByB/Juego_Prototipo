using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class ControladorCamara : MonoBehaviour
{
    [SerializeField] private float cantidadTransicion = 0.2f;

    private CinemachineVirtualCamera[] camaras;
    private float screeYOriginal;

    private void Awake()
    {
        camaras = FindObjectsOfType<CinemachineVirtualCamera>();
        screeYOriginal = camaras[0].GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY;
    }

    private void OnMoverVertical(InputValue valor)
    {
        float inputMovimientoVertical = valor.Get<float>();
        foreach (var camara in camaras)
        {
            camara.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = screeYOriginal + (cantidadTransicion * inputMovimientoVertical);
        }
    }

}