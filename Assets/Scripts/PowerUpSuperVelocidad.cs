using UnityEngine;

public class PowerUpSuperVelocidad: PowerUp
{
    [SerializeField] private float potenciadorVelocidad = 1.5f;

    private float velOriginal;

    public override void AplicarPowerUpAlPlayer(Player player)
    {
        velOriginal = player.VelHorizontal;
        player.VelHorizontal *= potenciadorVelocidad;
    }

    public override void QuitarPowerUpAlPlayer(Player player)
    {
        if (player)
        {
            player.VelHorizontal = velOriginal;
        }
    }
}