using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PostNivel : MonoBehaviour
{
    [SerializeField] private TMP_Text textoNivelSuperado;
    [SerializeField] private TMP_Text textoPuntaje;
    [SerializeField] private TMP_Text textoDistancia;

    private void Start()
    {
        textoNivelSuperado.text = $"Nivel {DatosJuegos.NivelSuperado} Completo";
        textoPuntaje.text = $"Tenes {DatosJuegos.PuntosActuales} puntos!!";
        textoDistancia.text = $"Recorriste {DatosJuegos.DistanciaRecorridaEnNivel} metros en este nivel!";
        GetComponent<PanelVyM>().CrearTextosPanel(DatosJuegos.CantidadVyMConsumidas);
    }
}
