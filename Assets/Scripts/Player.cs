using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class Player : MonoBehaviour
{
    public int fallBoundary = -5;

    public void DamagePlayer(int damage)
    {
        GameMaster.KillPlayer(this);
    }

    private void Update()
    {
        if (transform.position.y <= fallBoundary)
            gameObject.GetComponent<PlatformerCharacter2D>().Fallen();
    }
}
