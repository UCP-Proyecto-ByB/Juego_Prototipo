using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agua : Consumible
{
    [SerializeField] private float tiempoRespawn = 8.0f;
    private Vector3 respawnPos;

    public override void AfectarPlayer(Player player)
    {
        player.ModificarHidratacion(LiquidoEntregado);
    }

    public override void Destruir()
    {
        //base.Destruir();
        respawnPos = this.transform.position;
        this.gameObject.SetActive(false);
        Invoke(nameof(Respawn), tiempoRespawn);
    }

    private void Respawn()
    {
        this.Consumido = false;
        this.transform.position = respawnPos;
        GetComponent<CircleCollider2D>().enabled = true;
        this.gameObject.SetActive(true);
    }
}
