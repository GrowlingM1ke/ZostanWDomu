﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;
using FMODUnity;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;

    public static int playerLife = 3;

    public bool tutorial;
    public static bool staticTutorial;

    private void Awake()
    {
        
    }

    private void Start()
    {
        if (gm == null)
        {
            gm = this;
        }
        if (!tutorial)
            LifeBar.SetLife(playerLife);
        staticTutorial = tutorial;
        playerLife = 3;
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 0.2f;
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
        if (staticTutorial)
            playerLife++;
        if (playerLife < 1)
            SceneManager.LoadScene("Death");
        if (!staticTutorial)
            LifeBar.SetLife(playerLife);
        gm.StartCoroutine(gm.RespawnPlayer());
    }

    private void findPlayer(Transform player, Transform enemy)
    {
        enemy.gameObject.GetComponent<Enemy>().player = player;
        enemy.gameObject.GetComponent<AIDestinationSetter>().target = player;
    }
}
