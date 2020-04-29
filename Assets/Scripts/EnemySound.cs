using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class EnemySound : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter studioEventEmitter;
    private GameObject player;
    public AIPath aIPath;

    private void Start()
    {
        studioEventEmitter.EventInstance.setVolume(0);
    }
    void Update()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
            if (distance < 40)
            {
                float volume = ConvertRange(40, 0, 0, 5, distance);
                studioEventEmitter.EventInstance.setVolume(volume);
            }
        }
    }

    private float ConvertRange(float originalStart, float originalEnd, float newStart, float newEnd, float value)
    {
        float scale = (float)(newEnd - newStart) / (originalEnd - originalStart);
        return (float)(newStart + ((value - originalStart) * scale));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "music_trigger")
            aIPath.maxSpeed = 8;
    }
}
