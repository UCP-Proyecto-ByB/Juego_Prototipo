using UnityEngine;

public class EnemigoPath : Enemigo
{
    private Vector3 rotacion = new Vector3(0.0f, 0.0f, 180.0f);
    private PathFollow pathfollow;

    private void Awake()
    {
        pathfollow = GetComponent<PathFollow>();
    }

    private void Update()
    {
        if (pathfollow.IndicePuntoMovimiento > 0)
        {
            transform.Rotate(-rotacion * Time.deltaTime);
        }
        else
        {
            transform.Rotate(rotacion * Time.deltaTime);
        }
        // if (RBody.velocity.x >= 0.0f)
        // {
        //     transform.Rotate(rotacion * Time.deltaTime);
        // }
        // else
        // {
        //     transform.Rotate(-rotacion * Time.deltaTime);
        // }
    }
}