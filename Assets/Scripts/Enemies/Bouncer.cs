using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : Enemy
{
    [SerializeField] int _bounceForce = 100;

    Vector3 offset;

    protected override void PlayerImpact(Player player)
    {
        base.PlayerImpact(player);

        offset = transform.position - player.transform.position;
        GetComponent<Rigidbody>().AddForce(-offset * _bounceForce * 10);
    }
}
