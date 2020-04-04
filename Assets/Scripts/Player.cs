using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int fallBoundary = -20;

    public void DamagePlayer(int damage)
    {
        GameMaster.KillPlayer(this);
    }

    private void Update()
    {
        if (transform.position.y <= fallBoundary)
            GameMaster.KillPlayer(this);
    }
}
