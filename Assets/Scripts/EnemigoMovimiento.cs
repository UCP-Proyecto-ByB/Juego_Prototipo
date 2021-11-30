using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoMovimiento : Enemigo
{
    [Header("Debuguers")]
    [SerializeField] private bool mostrarPosicionesRayCasts = false;

    [Header("Movimiento")]
    [SerializeField] private float velHorizontal = 2.0f;
    [SerializeField] private bool puedeMoverse = false;

    [Header("Componentes")]
    [SerializeField] private float largoRayCastPared = 0.1f;
    [SerializeField] private float largoRayCastVacio = 0.1f;
    [SerializeField] private LayerMask capaPlataformas;
    [SerializeField] private GameObject posicionDetectorVacio;
    private Rigidbody2D rBody;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        velHorizontal *= DireccionAleatoria();
    }

    private void Update()
    {
        if (!puedeMoverse) { return; }
        
        bool tocoPared = Physics2D.Raycast(transform.position, Vector2.right, largoRayCastPared, capaPlataformas);
        bool tocaSuelo = Physics2D.Raycast(posicionDetectorVacio.transform.position, Vector2.down, largoRayCastVacio, capaPlataformas);

        if (tocoPared || !tocaSuelo)
        {
            velHorizontal *= -1;
            CambiarPosicionRayCasts();
        }
    }

    private void FixedUpdate()
    {
        if (!puedeMoverse) { return; }

        rBody.velocity = new Vector2(velHorizontal, rBody.velocity.y);
    }

    private void CambiarPosicionRayCasts()
    {
        largoRayCastPared *= -1;
        Vector3 posicionActual = posicionDetectorVacio.transform.localPosition;
        posicionDetectorVacio.transform.localPosition = new Vector3(posicionActual.x *= -1, posicionActual.y, posicionActual.z);

        if (mostrarPosicionesRayCasts)
        {
            Debug.Log($"Detector Vacio: {posicionDetectorVacio.transform.localPosition}");
        }
    }

    private int DireccionAleatoria()
    {
        float aleatorio = UnityEngine.Random.Range(0.0f, 1.0f);
        if (aleatorio <= 0.5f)
        {
            return 1;
        }

        CambiarPosicionRayCasts();
        return -1;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * largoRayCastPared);
        Gizmos.DrawLine(posicionDetectorVacio.transform.position, posicionDetectorVacio.transform.position + Vector3.down * largoRayCastVacio);
    }
}
