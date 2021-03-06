﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainmenu : MonoBehaviour
{
    public Sprite first;
    public Sprite second;
    public AudioSource audio;
    int state = 1;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0 << LayerMask.NameToLayer("Default"));

        if (hit.collider != null && state == 1)
        {
            GetComponent<Image>().sprite = second;
            state = 2;
        }
        else if (hit.collider == null && state == 2)
        {
            GetComponent<Image>().sprite = first;
            state = 1;
        }


        if (Input.GetKeyDown(KeyCode.Mouse0) == true && state == 2)
        {
            StartCoroutine(click());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        IEnumerator click()
        {
            audio.Play();
            yield return new WaitWhile(() => audio.isPlaying);
            Destroy(GameObject.Find("Muzyka"));
            SceneManager.LoadScene("Prolog _cutscena");
        }


    }


}
