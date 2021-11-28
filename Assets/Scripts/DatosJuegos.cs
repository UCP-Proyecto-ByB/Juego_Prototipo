using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DatosJuegos : MonoBehaviour
{
    private static DatosJuegos instancia;
    public static DatosJuegos Instancia { get => instancia; set => instancia = value; }

    // Vidas
    [SerializeField] private static bool vidasInfinitas = false;
    private static int vidasMaxPlayer = 3;
    private static int vidasActualesPlayer;

    // Stats
    private static float saludActualPlayer;
    private static float hidratacionActualPlayer;
    private static int puntosActuales = 0;
    private static int puntosActualesTemp = 0;
    private static Dictionary<string, float> cantidadVyMConsumidas = new Dictionary<string, float>();
    private static Dictionary<string, float> cantidadVyMConsumidasTemp = new Dictionary<string, float>();
    private static int distanciaRecorridaEnNivel = 0;

    // Niveles
    private static Vector3 posRespawnPlayer;
    private static string nivelSuperado = "0";
    private static string proximoNivel = "MenuPrincipal";

    // Setters y Getters
    public static Vector3 PosRespawnPlayer { get => posRespawnPlayer; set => posRespawnPlayer = value; }
    public static int VidasActualesPlayer { get => vidasActualesPlayer; set => vidasActualesPlayer = value; }
    public static float SaludActualPlayer { get => saludActualPlayer; set => saludActualPlayer = value; }
    public static float HidratacionActualPlayer { get => hidratacionActualPlayer; set => hidratacionActualPlayer = value; }
    public static Dictionary<string, float> CantidadVyMConsumidas { get => cantidadVyMConsumidas; set => cantidadVyMConsumidas = value; }
    public static int PuntosActuales { get => puntosActuales; set => puntosActuales = value; }
    public static string NivelSuperado { get => nivelSuperado; set => nivelSuperado = value; }
    public static string ProximoNivel { get => proximoNivel; set => proximoNivel = value; }
    public static int PuntosActualesTemp { get => puntosActualesTemp; set => puntosActualesTemp = value; }
    public static Dictionary<string, float> CantidadVyMConsumidasTemp { get => cantidadVyMConsumidasTemp; set => cantidadVyMConsumidasTemp = value; }
    public static int DistanciaRecorridaEnNivel { get => distanciaRecorridaEnNivel; set => distanciaRecorridaEnNivel = value; }

    private void Awake()
    {
        int numDatosJuegos = FindObjectsOfType<DatosJuegos>().Length;

        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
            ResetearValores();
        }
    }

    public static void ResetearValores()
    {
        VidasActualesPlayer = vidasMaxPlayer;
        PuntosActualesTemp = PuntosActuales;
        CopiarValoresDiccionarios(CantidadVyMConsumidasTemp, CantidadVyMConsumidas);
        //CantidadVyMConsumidasTemp.Clear();
        // foreach (var item in CantidadVyMConsumidas)
        // {
        //     CantidadVyMConsumidasTemp.Add(item.Key, item.Value);
        // }
        //CantidadVyMConsumidasTemp = CantidadVyMConsumidas;
    }

    public static void GuardarValoresTemporales()
    {
        PuntosActuales = PuntosActualesTemp;
        CopiarValoresDiccionarios(CantidadVyMConsumidas, CantidadVyMConsumidasTemp);
        //CantidadVyMConsumidas = CantidadVyMConsumidasTemp;
        // CantidadVyMConsumidas.Clear();
        // foreach (var item in CantidadVyMConsumidasTemp)
        // {
        //     CantidadVyMConsumidas.Add(item.Key, item.Value);
        // }
    }

    private static void CopiarValoresDiccionarios(Dictionary<string, float> aModificar, Dictionary<string, float> aCopiar)
    {
        aModificar.Clear();
        foreach (var item in aCopiar)
        {
            aModificar.Add(item.Key, item.Value);
        }
    }

    public static bool RestarVidaPlayer()
    {
        VidasActualesPlayer--;

        if (VidasActualesPlayer == 0 && !vidasInfinitas)
        {
            ResetearValores();
            FindObjectOfType<ControladorNiveles>().ResetearNivel();
            return false;
        }
        return true;
    }

    public static void AumentarVyM(Dictionary<string, float> VyM)
    {
        foreach (var consumible in VyM)
        {
            if (CantidadVyMConsumidasTemp.ContainsKey(consumible.Key))
            {
                CantidadVyMConsumidasTemp[consumible.Key] += consumible.Value;
            }
            else
            {
                CantidadVyMConsumidasTemp.Add(consumible.Key, consumible.Value);
            }
        }
    }


    // public void AumentarPuntaje(int puntos)
    // {
    //     puntaje += puntos;
    // }

}
