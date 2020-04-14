using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public Transform player;
    public int m_radius = 3;
    public Transform spawnPoint;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
            GameMaster.KillPlayer(collision.gameObject.GetComponent<Player>());
    }

}
