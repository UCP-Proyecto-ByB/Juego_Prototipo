using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruta : Consumible
{
    public event Action<Dictionary<string, float>> OnConsumido;
    public event Action<string> OnConsumidoParaObjetivo;

    private enum NombresFrutas { Banana, Manzana, Anana, Uva, Mango, Sandia }
    [SerializeField] private NombresFrutas nombre;

    [System.Serializable]
    private class ConsumibleMinerales
    {
        [SerializeField] private Minerales.Tipos mineral;
        [SerializeField] private float cantidadEntregada;

        public Minerales.Tipos Mineral { get => mineral; set => mineral = value; }
        public float CantidadEntregada { get => cantidadEntregada; set => cantidadEntregada = value; }

        public string DarNombre()
        {
            return mineral.ToString();
        }
    }

    [System.Serializable]
    private class ConsumibleVitaminas
    {
        [SerializeField] private Vitaminas.Tipos vitamina;
        [SerializeField] private float cantidadEntregada;

        public Vitaminas.Tipos Vitamina { get => vitamina; set => vitamina = value; }
        public float CantidadEntregada { get => cantidadEntregada; set => cantidadEntregada = value; }

        public string DarNombre()
        {
            return $"Vitamina {Vitamina.ToString()}";
        }
    }

    [Header("Otros")]
    [SerializeField] private float vidaEntregada = 2.0f;
    [SerializeField] private bool esRegional = false;
    private int puntosEntregados = 10;

    [Header("Vitaminas y Minerales")]
    [SerializeField] private List<ConsumibleVitaminas> vitaminas;
    [SerializeField] private List<ConsumibleMinerales> minerales;

    private void Start()
    {
        if (esRegional) { puntosEntregados *= 2; }
    }

    public override void AfectarPlayer(Player player)
    {
        player.ModificarHidratacion(LiquidoEntregado);
        player.RegenerarSalud(vidaEntregada);
        player.AumentarPuntaje(puntosEntregados, esRegional);
        OnConsumidoParaObjetivo?.Invoke(nombre.ToString());
        Invoke(nameof(GenerarListaTotalVyM), 0.05f);
    }

    private void GenerarListaTotalVyM()
    {
        Dictionary<string, float> totalVyM = new Dictionary<string, float>();

        foreach (var vitamina in vitaminas)
        {
            totalVyM.Add(vitamina.DarNombre(), vitamina.CantidadEntregada);
        }

        foreach (var mineral in minerales)
        {
            totalVyM.Add(mineral.DarNombre(), mineral.CantidadEntregada);
        }

        OnConsumido?.Invoke(totalVyM);
    }

    public override void Destruir()
    {
        Destroy(gameObject);
    }
}
