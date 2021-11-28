using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaSalida : MonoBehaviour
{
    //[SerializeField] private float tiempoEspera = 1.0f;
    [SerializeField] private string proximoNivel;
    private ControladorNiveles controladorNiveles;

    private void Awake()
    {
        controladorNiveles = FindObjectOfType<ControladorNiveles>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GetComponent<AudioSource>().Play();
        GetComponent<BoxCollider2D>().enabled = false;
        other.GetComponent<Player>().EntrarEnPortal();
        controladorNiveles.FinalizarNivel(proximoNivel);
        //StartCoroutine(CargarProximoNivel());
    }

    // IEnumerator CargarProximoNivel()
    // {
    //     int indiceNivelActual = SceneManager.GetActiveScene().buildIndex;

    //     //TODO: protegerse de fuera de indice

    //     yield return new WaitForSeconds(tiempoEspera);
    //     SceneManager.LoadScene(++indiceNivelActual);
    // }
}
