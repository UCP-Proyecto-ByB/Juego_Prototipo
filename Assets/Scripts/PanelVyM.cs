using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class PanelVyM : MonoBehaviour
{
    [SerializeField] private RectTransform panelPopUp;
    [SerializeField] private TMP_Text textoPlantilla;

    public void CrearTextosPanel(Dictionary<string, float> listaVyM)
    {
        // foreach (var item in listaVyM)
        // {
        //     CrearTexto(item.Key, item.Value);
        // }

        foreach (KeyValuePair<string, float> item in listaVyM.OrderBy(nombre => nombre.Key))
        {
            //Debug.Log($"{item.Key} - {item.Value}");
            CrearTexto(item.Key, item.Value);
        }
    }

    private void CrearTexto(string texto, float cantidad)
    {
        Transform panel = panelPopUp.transform;
        TMP_Text nuevoTexto = Instantiate(textoPlantilla, panel);
        nuevoTexto.name = "VyM";
        nuevoTexto.text = $"{texto}: {cantidad}";
        nuevoTexto.gameObject.SetActive(true);
    }

    //TODO: solo debug, quitar esto
    private void MostrarVyM()
    {
        var dicVyM = DatosJuegos.CantidadVyMConsumidas;

        Debug.Log("-------------------------");
        foreach (KeyValuePair<string, float> item in dicVyM.OrderBy(nombre => nombre.Key))
        {
            Debug.Log($"{item.Key} - {item.Value}");
        }
        Debug.Log("-------------------------");
    }

}
