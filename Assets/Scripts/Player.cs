using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action PlayerMuerto;
    public event Action PlayerLastimado;
    public event Action PlayerModificoHidratacion;
    public event Action<bool> PlayerAumentoPuntos;

    [Header("Debuguers")]
    [SerializeField] private bool verVelocidad = false;
    [SerializeField] private bool verInput = false;
    [SerializeField] private bool verHidratacion = false;
    [SerializeField] private bool verDistanciaRecorrido = false;

    [Header("Stats")]
    [SerializeField] private float saludMaxima = 20.0f;
    [SerializeField] private float hidratacionMaxima = 100.0f;
    [SerializeField] [Range(-3.0f, -0.1f)] private float gastoHidratacionCorrer = -1.5f; // Por segundo
    [SerializeField] [Range(-4.0f, -0.1f)] private float gastoHidratacionSaltar = -3f; // Por salto
    [SerializeField] [Range(-4.0f, -0.1f)] private float gastoHidratacionEscalar = -2f; // Por segundo
    private enum FasesHidratacion { Completa, Alta, Media, Baja };
    private Dictionary<FasesHidratacion, float> ModificadorEnergia = new Dictionary<FasesHidratacion, float> {
        {FasesHidratacion.Baja, 0.65f},
        {FasesHidratacion.Media, 0.75f},
        {FasesHidratacion.Alta, 0.85f},
        {FasesHidratacion.Completa, 1.0f}
    };
    private FasesHidratacion faseHidratacionActual = FasesHidratacion.Completa;
    private float saludActual;
    private float hidratacionActual;
    private bool estaVivo = true;
    private Vector3 posRespawn;
    private float distanciaRecorrida = 0.0f;

    [Header("Movimiento Horizontal")]
    [SerializeField] private float velHorizontal = 5.0f;

    [Header("Movimiento Vertical")]
    [SerializeField] private float velocidadVertical = 3.0f;
    [SerializeField] private float fuerzaSalto = 2.0f;
    [SerializeField] private bool estaEnSuelo = false;
    [SerializeField] private float tiempoCoyote = 0.2f;
    private float tiempoCoyoteTimer;
    private float gravedadOriginal;
    private bool estaEscalando = false;

    [Header("Componentes")]
    private Rigidbody2D rBody;
    private BoxCollider2D colisionador;
    private PlayerControladorSonidos controladorSonidos;
    [SerializeField] private float largoRayCastSuelo = 0.6f;
    [SerializeField] private Animator controladorAnimaciones;
    [SerializeField] private LayerMask capaPlataformas;
    //[SerializeField] private GameObject contenedorPlayer;
    [SerializeField] private GameObject detectorSueloDerecha;
    [SerializeField] private GameObject detectorSueloIzquierda;

    [Header("Inputs")]
    //Vector2 inputMovimiento;
    [SerializeField] private bool puedeHacerInput = true;
    private float inputMovimientoVertical = 0.0f;
    private float inputMovimientoHorizontal = 0.0f;
    private float modificadorInputOriginal = 1.0f;
    private float modificadorInputActual;

    public float FuerzaSalto { get => fuerzaSalto; set => fuerzaSalto = value; }
    public float SaludMaxima { get => saludMaxima; set => saludMaxima = value; }
    public float SaludActual { get => saludActual; set => saludActual = value; }
    public float VelHorizontal { get => velHorizontal; set => velHorizontal = value; }

    //[Header("Managers")]
    //private DatosJuegos datosJuegos;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        colisionador = GetComponent<BoxCollider2D>();
        controladorSonidos = GetComponent<PlayerControladorSonidos>();
        //datosJuegos = FindObjectOfType<DatosJuegos>();
    }

    private void Start()
    {
        //datosJuegos.PosRespawnPlayer = transform.position;
        AsignarPosRespawn(transform.position, false);
        SaludActual = SaludMaxima;
        distanciaRecorrida = 0.0f;
        hidratacionActual = hidratacionMaxima;
        modificadorInputActual = modificadorInputOriginal;
        DatosJuegos.HidratacionActualPlayer = hidratacionActual;
        DatosJuegos.SaludActualPlayer = SaludActual;
        DatosJuegos.PosRespawnPlayer = transform.position;
        hidratacionActual = hidratacionMaxima;
        gravedadOriginal = rBody.gravityScale;
        controladorAnimaciones.GetComponent<SpriteRenderer>().flipX = false;
    }

    private void Update()
    {
        if (!estaVivo) { return; }
        //estaEnSuelo = Physics2D.Raycast(transform.position, Vector2.down, largoRayCastSuelo, capaPlataformas);
        bool estaEnSueloDerecha = Physics2D.Raycast(detectorSueloDerecha.transform.position, Vector2.down, largoRayCastSuelo, capaPlataformas);
        bool estaEnSueloIzquierda = Physics2D.Raycast(detectorSueloIzquierda.transform.position, Vector2.down, largoRayCastSuelo, capaPlataformas);
        estaEnSuelo = estaEnSueloDerecha || estaEnSueloIzquierda;
        controladorAnimaciones.SetBool("estaSaltando", !estaEnSuelo);

        //estaEnSuelo = Physics2D.Raycast(transform.position - offsetRayCastSuelo, Vector2.down, largoRayCastSuelo, capaPlataformas);

        ChequearSaltoCoyote(estaEnSuelo);

        if (verInput) { Debug.Log($"Input H: {inputMovimientoHorizontal * modificadorInputActual} - Input V: {inputMovimientoVertical * modificadorInputActual}"); }
    }

    private void FixedUpdate()
    {
        if (!estaVivo || !puedeHacerInput) { return; }

        Correr();
        Escalar();
        if (verVelocidad) { Debug.Log(rBody.velocity); }
        if (verDistanciaRecorrido) { Debug.Log(distanciaRecorrida); }
    }


    private void OnMoverHorizontal(InputValue valor)
    {
        if (!puedeHacerInput) { inputMovimientoHorizontal = 0; return; }

        inputMovimientoHorizontal = valor.Get<float>();

        if (inputMovimientoHorizontal == 0)
        {
            return;
        }

        if (inputMovimientoHorizontal > 0)
        {
            controladorAnimaciones.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (inputMovimientoHorizontal < 0)
        {
            controladorAnimaciones.GetComponent<SpriteRenderer>().flipX = true;
        }

        //distanciaRecorrida += (1f * modificadorInputActual);

    }

    private void OnMoverVertical(InputValue valor)
    {
        if (!puedeHacerInput) { return; }
        inputMovimientoVertical = valor.Get<float>();
    }

    private void OnSaltar(InputValue valor)
    {
        bool presionandoInput = Convert.ToBoolean(valor.Get<float>());

        if (presionandoInput && tiempoCoyoteTimer > 0.0f)
        {
            rBody.velocity = new Vector2(0.0f, FuerzaSalto * modificadorInputActual);
            controladorSonidos.PlaySaltar();
            controladorAnimaciones.SetBool("estaSaltando", true);
            if (!estaEscalando) { ModificarHidratacion(gastoHidratacionSaltar, 1.0f); }
        }

        if (!presionandoInput && rBody.velocity.y > 0.0f)
        {
            rBody.velocity = new Vector2(0.0f, rBody.velocity.y * 0.4f);
        }

    }

    private void ChequearSaltoCoyote(bool enSuelo)
    {
        if (enSuelo)
        {
            tiempoCoyoteTimer = tiempoCoyote;
        }
        else
        {
            tiempoCoyoteTimer -= Time.deltaTime;
        }
    }

    private void Correr()
    {
        Vector2 velocidad = new Vector2(inputMovimientoHorizontal * modificadorInputActual * VelHorizontal, rBody.velocity.y);
        rBody.velocity = velocidad;

        bool estaCorriendo = Mathf.Abs(rBody.velocity.x) > Mathf.Epsilon;
        controladorAnimaciones.SetBool("estaCorriendo", estaCorriendo);
        ModificarHidratacion(gastoHidratacionCorrer * Mathf.Abs(inputMovimientoHorizontal), Time.deltaTime);
        distanciaRecorrida += (1f * Mathf.Abs(velocidad.x) * Time.fixedDeltaTime);
        if (verHidratacion) { Debug.Log(hidratacionActual); }
    }

    public void AsignarPosRespawn(Vector3 pos, bool desdeCheckPoint)
    {
        posRespawn = pos;
        if (desdeCheckPoint) { Debug.Log("Guardado!"); }
        DatosJuegos.PosRespawnPlayer = posRespawn;
    }

    public void ModificarHidratacion(float gasto, float unidad = 1.0f)
    {
        hidratacionActual += gasto * unidad;
        hidratacionActual = Mathf.Clamp(hidratacionActual, 0.0f, hidratacionMaxima);
        DatosJuegos.HidratacionActualPlayer = hidratacionActual;
        //OnConsumido?.Invoke(totalVyM);
        PlayerModificoHidratacion?.Invoke();
        //PlayerModificoHidratacion();

        if (hidratacionActual < hidratacionMaxima * 0.25)
        {
            faseHidratacionActual = FasesHidratacion.Baja;
        }
        else if (hidratacionActual < hidratacionMaxima * 0.50)
        {
            faseHidratacionActual = FasesHidratacion.Media;
        }
        else if (hidratacionActual < hidratacionMaxima * 0.75)
        {
            faseHidratacionActual = FasesHidratacion.Alta;
        }
        else
        {
            faseHidratacionActual = FasesHidratacion.Completa;
        }

        modificadorInputActual = modificadorInputOriginal * ModificadorEnergia[faseHidratacionActual];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(detectorSueloDerecha.transform.position, detectorSueloDerecha.transform.position + Vector3.down * largoRayCastSuelo);
        Gizmos.DrawLine(detectorSueloIzquierda.transform.position, detectorSueloIzquierda.transform.position + Vector3.down * largoRayCastSuelo);
        //Gizmos.DrawLine(transform.position, transform.position + Vector3.down * largoRayCastSuelo);
    }


    private void Escalar()
    {
        //if (!colisionador.IsTouchingLayers(LayerMask.GetMask("Escaleras"))) { return; }

        //Debug.Log("puede escalar");

        if (!estaEscalando) { return; }

        Vector2 velocidad = new Vector2(rBody.velocity.x, inputMovimientoVertical * modificadorInputActual * velocidadVertical);
        rBody.velocity = velocidad;

        bool tieneVelVertical = Mathf.Abs(rBody.velocity.y) > Mathf.Epsilon;
        controladorAnimaciones.SetBool("estaEnEscalera", tieneVelVertical);
        ModificarHidratacion(gastoHidratacionEscalar * Mathf.Abs(inputMovimientoVertical), Time.fixedDeltaTime);
        distanciaRecorrida += (0.2f * Mathf.Abs(velocidad.y) * Time.fixedDeltaTime);
    }

    public void ChequearSiEstaEnEscalera(bool estaEnEscalera)
    {
        estaEscalando = estaEnEscalera;
        controladorAnimaciones.SetBool("estaEnEscalera", estaEnEscalera);

        rBody.gravityScale = estaEnEscalera == true ? 0.0f : gravedadOriginal;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!estaVivo) { return; }
        if (other.gameObject.CompareTag("Enemigo"))
        {
            var daniador = other.GetComponent<IDaniador>();
            daniador.Morir();
            Daniar(daniador.Danio);
        }
    }

    private void Daniar(float danio)
    {
        ModificarSalud(-danio);
        controladorSonidos.PlayDanio();
    }

    public void RegenerarSalud(float regeneracion)
    {
        ModificarSalud(regeneracion);
    }

    public void AumentarPuntaje(int puntos, bool esRegional)
    {
        DatosJuegos.PuntosActualesTemp += puntos;
        PlayerAumentoPuntos?.Invoke(esRegional);
    }

    public void ModificarSalud(float valor)
    {
        SaludActual += valor;
        SaludActual = Mathf.Clamp(SaludActual, 0.0f, SaludMaxima);
        DatosJuegos.SaludActualPlayer = SaludActual;
        PlayerLastimado?.Invoke();
        //PlayerLastimado();

        if (SaludActual == 0.0f)
        {
            Morir();
        }
    }

    public void EntrarEnPortal()
    {
        DatosJuegos.DistanciaRecorridaEnNivel = ((int)distanciaRecorrida);
        puedeHacerInput = false;
        rBody.velocity = Vector2.zero;
        //TODO: anim entrar portal
    }

    public void Morir()
    {
        if (!estaVivo) { return; }
        //GetComponent<BoxCollider2D>().enabled = false;
        switchVida(false);
        controladorSonidos.PlayMuerte();
        controladorAnimaciones.SetTrigger("estaMuerto");
        rBody.velocity = Vector2.zero;
        bool puedeRespawnear = DatosJuegos.RestarVidaPlayer();
        if (puedeRespawnear)
        {
            PlayerMuerto?.Invoke();
            StartCoroutine(Spawnear());
        }
    }

    IEnumerator Spawnear()
    {
        yield return new WaitForSeconds(1.5f);

        //transform.position = posRespawn;
        transform.position = DatosJuegos.PosRespawnPlayer;
        SaludActual = SaludMaxima;
        hidratacionActual = hidratacionMaxima;
        DatosJuegos.SaludActualPlayer = SaludActual;
        PlayerLastimado();
        controladorAnimaciones.SetTrigger("estaRespawn");
        switchVida(true);
    }

    private void switchVida(bool vivo)
    {
        estaVivo = vivo;
        puedeHacerInput = vivo;
    }
}
