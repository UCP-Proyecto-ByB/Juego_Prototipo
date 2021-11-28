using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Text.RegularExpressions;

public class ControladorNiveles : MonoBehaviour
{
    [SerializeField] private float tiempoEspera = 2.0f;

    private DatosJuegos datosJuegos;

    public void ResetearNivel()
    {
        StartCoroutine(CargarNivel(true));
    }

    public void FinalizarNivel(string proxNivel)
    {
        DatosJuegos.ProximoNivel = proxNivel;
        DatosJuegos.GuardarValoresTemporales();
        StartCoroutine(CargarPostNivel());
    }

    public void CargarProximoNivel()
    {
        StartCoroutine(CargarNivel(false));
    }

    public void CargarPrimerNivel()
    {
        SceneManager.LoadScene("Level1");
    }

    IEnumerator CargarNivel(bool actual)
    {
        int indiceNivelActual = SceneManager.GetActiveScene().buildIndex;

        yield return new WaitForSeconds(tiempoEspera);

        if (actual)
        {
            SceneManager.LoadScene(indiceNivelActual);
        }
        else
        {
            //TODO: protegerse fuera indice
            SceneManager.LoadScene(DatosJuegos.ProximoNivel);
        }
    }

    IEnumerator CargarPostNivel()
    {
        Scene escenaActual = SceneManager.GetActiveScene();
        string numeroEscena = (Regex.Match(escenaActual.name, @"\d+").Value);
        DatosJuegos.NivelSuperado = numeroEscena;

        yield return new WaitForSeconds(tiempoEspera);
        SceneManager.LoadScene("PostNivel");
    }

    

}
