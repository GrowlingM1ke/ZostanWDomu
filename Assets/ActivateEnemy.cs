using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class ActivateEnemy : MonoBehaviour
{
    public Transform enemy;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemy.gameObject.GetComponent<AIDestinationSetter>().target = collision.gameObject.transform;
        }
    }

}
