using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControladorUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textoVidas;
    [SerializeField] private TMP_Text textoSalud;
    [SerializeField] private TMP_Text textoHidratacion;
    [SerializeField] private TMP_Text textoPuntos;
    [SerializeField] private RectTransform panelRegional;
    [SerializeField] private RectTransform panelObjetivos;

    private Player player;
    private ControladorObjetivos controladorObjetivos;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        controladorObjetivos = FindObjectOfType<ControladorObjetivos>();
        MostrarPanel(false);
    }

    private void OnEnable()
    {
        player.PlayerMuerto += ActualizarTextoVidas;
        player.PlayerLastimado += ActualizarTextoSalud;
        player.PlayerModificoHidratacion += ActualizartextoHidratacion;
        player.PlayerAumentoPuntos += ActualizarTextoPuntos;
        if (controladorObjetivos) { }
    }

    private void OnDisable()
    {
        player.PlayerMuerto -= ActualizarTextoVidas;
        player.PlayerLastimado -= ActualizarTextoSalud;
        player.PlayerModificoHidratacion -= ActualizartextoHidratacion;
        player.PlayerAumentoPuntos -= ActualizarTextoPuntos;
    }

    private void Start()
    {
        ActualizarHUD();
    }

    private void ActualizarHUD()
    {
        ActualizarTextoVidas();
        ActualizarTextoSalud();
        ActualizartextoHidratacion();
        ActualizarTextoPuntos(false);
    }

    private void ActualizarTextoVidas()
    {
        textoVidas.text = $"Vidas: {DatosJuegos.VidasActualesPlayer}";
    }

    private void ActualizarTextoSalud()
    {
        textoSalud.text = $"Salud: {DatosJuegos.SaludActualPlayer}";
    }

    private void ActualizartextoHidratacion()
    {
        textoHidratacion.text = $"Hidratacion: {Mathf.Round(DatosJuegos.HidratacionActualPlayer)}";
    }

    private void ActualizarTextoPuntos(bool esRegional)
    {
        textoPuntos.text = $"Puntos: {DatosJuegos.PuntosActualesTemp}";
        if (esRegional) { MostrarPanel(true); }
    }

    public void MostrarPanelObjetivos(int objetivo)
    {
        panelObjetivos.gameObject.SetActive(true);
        Invoke(nameof(OcultarPanelObjetivos), 1.5f);
    }

    private void OcultarPanelObjetivos()
    {
        panelObjetivos.gameObject.SetActive(false);
    }

    private void MostrarPanel(bool visible)
    {
        CanvasGroup canvas = panelRegional.GetComponent<CanvasGroup>();
        canvas.alpha = visible ? 1f : 0f;
        if (visible) { Invoke(nameof(OcultarDespuesDeMostrar), 1.2f); }
    }

    private void OcultarDespuesDeMostrar()
    {
        MostrarPanel(false);
    }


}
