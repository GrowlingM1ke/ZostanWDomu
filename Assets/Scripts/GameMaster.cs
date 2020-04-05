using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;

    public static int playerLife = 3;

    private void Start()
    {
        if (gm == null)
        {
            gm = this;
        }

        LifeBar.SetLife(playerLife);
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public int spawnDelay = 2;
    public Transform spawnPrefab;
    public Transform[] enemies;
    public bool sluch = false;

    public IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds((float)spawnDelay);

        Transform player;
        if (GameObject.FindGameObjectWithTag("Player") == null)
            player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        else
            player = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (sluch)
                findPlayer(player, enemies[i]);
            else
                findPlayer(enemies[i].gameObject.transform, enemies[i]);
            if (enemies[i].GetComponent<Enemy>().spawnPoint != null)
            {
                enemies[i].GetComponent<AIPath>().Teleport(enemies[i].GetComponent<Enemy>().spawnPoint.position, true);
            }
            
        }
        Transform spawnParticles = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation);
        Destroy(spawnParticles.gameObject, 3f);
    }

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        playerLife--;
        LifeBar.SetLife(playerLife);
        gm.StartCoroutine(gm.RespawnPlayer());
    }

    private void findPlayer(Transform player, Transform enemy)
    {
        enemy.gameObject.GetComponent<Enemy>().player = player;
        enemy.gameObject.GetComponent<AIDestinationSetter>().target = player;
    }
}
