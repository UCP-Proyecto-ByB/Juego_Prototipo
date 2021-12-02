using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class ControladorObjetivos : MonoBehaviour
{
    private class ObjetivoCumplir
    {
        private int id;
        private List<string> frutas;
        private int cantidad;

        public ObjetivoCumplir(int id, List<string> frutas, int cantidad)
        {
            this.id = id;
            this.frutas = frutas;
            this.cantidad = cantidad;
        }

        public int Id { get => id; set => id = value; }
        public List<string> Frutas { get => frutas; set => frutas = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
    }

    [SerializeField] private List<Fruta> consumiblesFyV;
    [SerializeField] private UnityEvent OnObjetivoCompleto;
    private List<ObjetivoCumplir> objetivos = new List<ObjetivoCumplir>();
    private int idActual = 0;


    private void OnEnable()
    {
        foreach (var consumible in consumiblesFyV)
        {
            consumible.OnConsumidoParaObjetivo += ChequearObjetivos;
        }
    }

    private void OnDisable()
    {
        foreach (var consumible in consumiblesFyV)
        {
            consumible.OnConsumidoParaObjetivo -= ChequearObjetivos;
        }
    }

    private void ChequearObjetivos(string fruta)
    {
        foreach (var objetivo in objetivos)
        {
            //Debug.Log(objetivo.Id);
            if (objetivo.Frutas.Contains(fruta))
            {
                //Debug.Log("Es parte del objetivo");
                objetivo.Cantidad--;
                if (objetivo.Cantidad == 0)
                {
                    OnObjetivoCompleto.Invoke();
                    objetivos.Remove(objetivo);
                    break;
                }
            }
        }
    }

    public void RecibirObjetivos(List<string> consumibles, int cantidad)
    {
        ObjetivoCumplir objetivo = new ObjetivoCumplir(++idActual, consumibles, cantidad);
        objetivos.Add(objetivo);
    }


    [ContextMenu("Auto completar consumibles")]
    private void AutoCompletarConsumibles()
    {
        consumiblesFyV = FindObjectsOfType<Fruta>().ToList();
    }
}