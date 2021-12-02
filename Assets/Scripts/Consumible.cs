using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumible : MonoBehaviour
{


    //private enum TiposConsumible { Agua, Banana, Manzana, Tomate };
    //[SerializeField] private TiposConsumible tipoConsumible;
    [SerializeField] private float liquidoEntregado;
    private bool consumido = false;

    private Animator controladorAnimaciones;

    public float LiquidoEntregado { get => liquidoEntregado; }
    public bool Consumido { get => consumido; set => consumido = value; }

    private void Awake()
    {
        controladorAnimaciones = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!Consumido)
        {
            Consumido = true;
            Player player = other.gameObject.GetComponent<Player>();
            controladorAnimaciones.SetTrigger("estaConsumida");
            AfectarPlayer(player);
            GetComponent<AudioSource>().Play();
        }
    }

    public abstract void AfectarPlayer(Player player);

    public abstract void Destruir();

}
