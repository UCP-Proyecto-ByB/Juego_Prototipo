using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agua : Consumible
{
    public override void AfectarPlayer(Player player)
    {
        player.ModificarHidratacion(LiquidoEntregado);
    }
}
