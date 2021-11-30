using UnityEngine;

public class PathFollow : MonoBehaviour
{

    [SerializeField] private float velocidad;
    [SerializeField] int puntoInicio;
    [SerializeField] Transform[] puntosMovimiento;

    private int indicePuntoMovimiento;
    private float rangoCambioPos = 0.05f;

    private void Start()
    {
        transform.position = puntosMovimiento[puntoInicio].position;

        foreach (var item in puntosMovimiento)
        {
            var color = item.GetComponent<SpriteRenderer>().color;
            color.a = 0.0f;
            item.GetComponent<SpriteRenderer>().color = color;
        }
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, puntosMovimiento[indicePuntoMovimiento].position) < rangoCambioPos)
        {
            indicePuntoMovimiento++;
            if (indicePuntoMovimiento == puntosMovimiento.Length)
            {
                indicePuntoMovimiento = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[indicePuntoMovimiento].position, velocidad * Time.deltaTime);
    }

}
