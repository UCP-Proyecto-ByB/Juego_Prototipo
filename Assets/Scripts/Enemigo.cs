using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour, IDaniador
{
    [SerializeField] private float danio = 5.0f;

    public float Danio { get => danio; set => danio = value; }

    public void Morir()
    {
        //Animar muerte
        Destroy(gameObject);
    }
}
