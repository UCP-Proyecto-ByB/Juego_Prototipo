using UnityEngine;

public class PathFollow : MonoBehaviour
{

    [SerializeField] private float velocidad;
    [SerializeField] int puntoInicio;
    [SerializeField] Transform[] puntosMovimiento;

    private int indicePuntoMovimiento;
    private float rangoCambioPos = 0.05f;

    public int IndicePuntoMovimiento { get => indicePuntoMovimiento; set => indicePuntoMovimiento = value; }

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
        if (Vector2.Distance(transform.position, puntosMovimiento[IndicePuntoMovimiento].position) < rangoCambioPos)
        {
            IndicePuntoMovimiento++;
            if (IndicePuntoMovimiento == puntosMovimiento.Length)
            {
                IndicePuntoMovimiento = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[IndicePuntoMovimiento].position, velocidad * Time.fixedDeltaTime);
    }

}
