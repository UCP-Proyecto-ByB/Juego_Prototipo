using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] private float tiempoPowerUp = 5.0f;

    private bool powerUpAplicado = false;
    private Animator controladorAnimaciones;
    private Player player;

    private void Awake()
    {
        controladorAnimaciones = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!powerUpAplicado) { return; }
        tiempoPowerUp -= Time.deltaTime;
        if (tiempoPowerUp <= 0.0)
        {
            QuitarPowerUpAlPlayer(player);
            //this.enabled = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!powerUpAplicado)
        {
            powerUpAplicado = true;
            controladorAnimaciones.SetTrigger("estaConsumida");
            GetComponent<AudioSource>().Play();
            player = other.GetComponent<Player>();
            if (player) { AplicarPowerUpAlPlayer(player); }
        }
    }

    private void Destruir()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public abstract void AplicarPowerUpAlPlayer(Player player);
    public abstract void QuitarPowerUpAlPlayer(Player player);
}