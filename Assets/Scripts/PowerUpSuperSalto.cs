using UnityEngine;

public class PowerUpSuperSalto : PowerUp
{
    [SerializeField] private float potenciadorSalto = 1.5f;
    

    private float fuerzaSaltoOriginal;

    public override void AplicarPowerUpAlPlayer(Player player)
    {
        fuerzaSaltoOriginal = player.FuerzaSalto;
        player.FuerzaSalto *= potenciadorSalto;
    }

    public override void QuitarPowerUpAlPlayer(Player player)
    {
        if (player)
        {
            player.FuerzaSalto = fuerzaSaltoOriginal;
        }
    }
}