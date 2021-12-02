using UnityEngine;

public class Objetivos : MonoBehaviour
{
    private GameObject canvasJuego;
    private Player player;

    private void Awake()
    {
        canvasJuego = GameObject.Find("CanvasJuego");
        player = FindObjectOfType<Player>();
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
        this.gameObject.SetActive(false);
    }

}