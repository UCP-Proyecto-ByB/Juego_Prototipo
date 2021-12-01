using System;
using UnityEngine;

public class EnemigoSuicida : Enemigo
{
    [Header("Ataque")]
    [SerializeField] private float desfaseAtaque = 0.5f;
    [SerializeField] private float esperaAtaque = 0.2f;

    [Header("Stats")]
    [SerializeField] private float velocidad = 2.0f;

    [Header("Componentes")]
    [SerializeField] private CircleCollider2D detectorPlayer;
    [SerializeField] private float radioDetector = 3.0f;

    private Vector3 rotacion = new Vector3(0.0f, 0.0f, 180.0f);
    private bool playerDetectado = false;
    private Vector3 posicionPlayer = Vector3.zero;

    private void Awake()
    {
        detectorPlayer.radius = radioDetector;
    }

    private void Start()
    {
        rotacion.z *= RotacionAleatoria();
    }

    private void Update()
    {
        transform.Rotate(rotacion * velocidad * Time.deltaTime);

        if (!playerDetectado) { return; }

        transform.position = Vector3.MoveTowards(transform.position, posicionPlayer, velocidad * Time.deltaTime);

        if (transform.position == posicionPlayer)
        {
            this.Morir();
        }
    }

    private int RotacionAleatoria()
    {
        float aleatorio = UnityEngine.Random.Range(0.0f, 1.0f);
        if (aleatorio <= 0.5f)
        {
            return 1;
        }

        return -1;
    }

    public void Atacar(Vector3 pos)
    {
        posicionPlayer = pos;
        rotacion.z *= transform.position.x >= posicionPlayer.x ? -1 : 1;
        posicionPlayer.x += DesfasarPuntoAtaque(desfaseAtaque);
        posicionPlayer.y += DesfasarPuntoAtaque(desfaseAtaque);
        Invoke(nameof(SetearPlayerDetectado), esperaAtaque);
    }

    private void SetearPlayerDetectado()
    {
        playerDetectado = true;
    }

    private float DesfasarPuntoAtaque(float valor)
    {
        return UnityEngine.Random.Range(-valor, valor);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, radioDetector);
        //Gizmos.DrawLine(detectorSueloDerecha.transform.position, detectorSueloDerecha.transform.position + Vector3.down * largoRayCastSuelo);

    }

}