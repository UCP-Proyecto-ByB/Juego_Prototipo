using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class ControladorPopUpVyM : MonoBehaviour
{
    // hasta 8 textos para 140 height = 18 x linea
    [SerializeField] private RectTransform panelPopUp;
    [SerializeField] private TMP_Text textoPlantilla;
    [SerializeField] private List<Fruta> consumiblesFyV;
    [SerializeField] private float tiempoVisible = 2.0f;
    private RectTransform panelPopUpRect;

    private void OnEnable()
    {
        foreach (var consumible in consumiblesFyV)
        {
            consumible.OnConsumido += CrearTextosPanel;
        }
    }

    private void OnDisable()
    {
        foreach (var consumible in consumiblesFyV)
        {
            consumible.OnConsumido -= CrearTextosPanel;
        }
    }

    private void Awake()
    {
        MostrarPanel(false);
        //gameObject.SetActive(false);
        panelPopUpRect = panelPopUp.GetComponent<RectTransform>();
    }

    private void Start()
    {
        ResetearPanel();
    }

    private void ResetearPanel()
    {
        panelPopUpRect.sizeDelta = new Vector2(panelPopUp.rect.width, 18);
        Transform panel = panelPopUp.transform;
        foreach (Transform item in panel.transform)
        {
            if (item.name == "VyM")
            {
                Destroy(item.gameObject);
            }
        }
    }

    public void CrearTextosPanel(Dictionary<string, float> listaVyM)
    {
        DatosJuegos.AumentarVyM(listaVyM);

        foreach (var item in listaVyM.Keys)
        {
            CrearTexto(item);
        }

        MostrarPanel(true);
    }

    private void CrearTexto(string texto)
    {
        panelPopUpRect.sizeDelta += new Vector2(0, 18);
        Transform panel = panelPopUp.transform;
        TMP_Text nuevoTexto = Instantiate(textoPlantilla, panel);
        nuevoTexto.name = "VyM";
        nuevoTexto.text = $"+ {texto}";
        nuevoTexto.gameObject.SetActive(true);
    }

    private void MostrarPanel(bool visible)
    {
        CanvasGroup canvas = panelPopUp.GetComponent<CanvasGroup>();
        canvas.alpha = visible ? 1f : 0f;
        if (visible) { Invoke(nameof(OcultarDespuesDeMostrar), tiempoVisible); }

    }

    private void OcultarDespuesDeMostrar()
    {
        MostrarPanel(false);
        ResetearPanel();
    }

    [ContextMenu("Auto completar consumibles")]
    private void AutoCompletarConsumibles()
    {
        consumiblesFyV = FindObjectsOfType<Fruta>().ToList();
    }
}
