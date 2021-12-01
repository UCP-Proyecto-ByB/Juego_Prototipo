using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    // [SerializeField] private float velocidad;
    // [SerializeField] int puntoInicio;
    // [SerializeField] Transform[] puntosMovimiento;

    // private int indicePuntoMovimiento;
    // private float rangoCambioPos = 0.05f;

    // private void Start()
    // {
    //     transform.position = puntosMovimiento[puntoInicio].position;

    //     foreach (var item in puntosMovimiento)
    //     {
    //         var color = item.GetComponent<SpriteRenderer>().color;
    //         color.a = 0.0f;
    //         item.GetComponent<SpriteRenderer>().color = color;
    //     }
    // }

    // private void FixedUpdate()
    // {
    //     if (Vector2.Distance(transform.position, puntosMovimiento[indicePuntoMovimiento].position) < rangoCambioPos)
    //     {
    //         indicePuntoMovimiento++;
    //         if (indicePuntoMovimiento == puntosMovimiento.Length)
    //         {
    //             indicePuntoMovimiento = 0;
    //         }
    //     }

    //     transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[indicePuntoMovimiento].position, velocidad * Time.deltaTime);
    // }

    private void OnCollisionEnter2D(Collision2D other)
    {
        bool desdeArriba = other.transform.position.y > transform.position.y;

        if (other.gameObject.CompareTag("Player") && desdeArriba)
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
