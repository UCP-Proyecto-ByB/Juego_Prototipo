using UnityEngine;
using TMPro;
using System;

public class Objetivos : MonoBehaviour
{
    [SerializeField] private TMP_Text[] listaObjetivos;
    [SerializeField] private RectTransform panelObjetivos;

    private GameObject canvasJuego;
    private Player player;

    private void Awake()
    {
        canvasJuego = GameObject.Find("CanvasJuego");
        player = FindObjectOfType<Player>();
        OrdenarTextosObjetivos();
    }

    private void OrdenarTextosObjetivos()
    {
        foreach (var texto in listaObjetivos)
        {
            Transform panel = panelObjetivos.transform;

            var nuevoTexto = Instantiate(texto, panel);
        }
    }

    private void Start()
    {
        player.ModificarControlJugador(false);
        canvasJuego.GetComponent<CanvasGroup>().alpha = 0.0f;
    }


    public void Jugar()
    {
        canvasJuego.GetComponent<CanvasGroup>().alpha = 1.0f;
        player.ModificarControlJugador(true);
        Cursor.visible = false;
        this.gameObject.SetActive(false);
    }

}