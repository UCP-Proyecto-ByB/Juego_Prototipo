using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaTrampa : MonoBehaviour
{
    [SerializeField] private float tiempoParaActivarse = 1.5f;
    [SerializeField] private float timepoCaida = 3.0f;
    [SerializeField] private bool esRespawneable = true;
    [SerializeField] private float tiempoRespawn = 6.0f;
    private GameObject cuerpo;
    private Vector3 posOriginal;
    private Rigidbody2D rBody;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        cuerpo = transform.Find("Cuerpo").gameObject;
    }

    private void Start()
    {
        posOriginal = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        bool desdeArriba = other.transform.position.y > transform.position.y;

        if (other.gameObject.CompareTag("Player") && desdeArriba) { Invoke(nameof(ActivarTrampa), tiempoParaActivarse); }

    }

    private void ActivarTrampa()
    {
        rBody.isKinematic = false;
        cuerpo.SetActive(false);
        Invoke(nameof(Desactivarse), timepoCaida);
    }

    private void Desactivarse()
    {
        gameObject.SetActive(false);
        if (esRespawneable) { Invoke(nameof(Respawn), tiempoRespawn - timepoCaida); }
    }

    private void Respawn()
    {
        transform.position = posOriginal;
        gameObject.SetActive(true);
        cuerpo.SetActive(true);
        rBody.isKinematic = true;
    }
}
