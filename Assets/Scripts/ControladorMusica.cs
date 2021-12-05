using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorMusica : MonoBehaviour
{
    private static ControladorMusica instancia;

    public static ControladorMusica Instancia { get => instancia; set => instancia = value; }

    private void Awake()
    {
        int numControlador = FindObjectsOfType<ControladorMusica>().Length;

        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
            PlayMusica();
        }
    }


    private void PlayMusica()
    {
        GetComponent<AudioSource>().Play();
    }
}
