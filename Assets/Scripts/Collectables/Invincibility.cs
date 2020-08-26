using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : PowerUpBase
{
    [SerializeField] Material M_Invincibility;
    [SerializeField] Material M_Player;

    protected override void PowerUp(Player player)
    {
        player._invincibility = true;
        player.GetComponent<Renderer>().material = M_Invincibility;
    }

    protected override void PowerDown(Player player)
    {
        player._invincibility = false;
        player.GetComponent<Renderer>().material = M_Player;
    }
}
