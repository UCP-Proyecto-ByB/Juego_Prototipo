using UnityEngine;

public class PowerUpRegenerarSalud : PowerUp
{
    public override void AplicarPowerUpAlPlayer(Player player)
    {
        float saludRegenerar = player.SaludMaxima - player.SaludActual;
        player.ModificarSalud(saludRegenerar);
    }

    public override void QuitarPowerUpAlPlayer(Player player)
    {
        return;
    }
}