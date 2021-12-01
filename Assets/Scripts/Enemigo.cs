using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemigo : MonoBehaviour, IDaniador
{
    [SerializeField] private float danio = 5.0f;
    [SerializeField] private GameObject explosionPrefab;
    private Rigidbody2D rBody;

    public float Danio { get => danio; set => danio = value; }
    public Rigidbody2D RBody { get => rBody; set => rBody = value; }

    public void Morir()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
