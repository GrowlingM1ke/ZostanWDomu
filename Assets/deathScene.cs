using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deathScene : MonoBehaviour
{
    float time = 0;

    void FixedUpdate()
    {
        time += Time.deltaTime;

        if (time > 3 || Input.GetKeyDown(KeyCode.Mouse0))
            SceneManager.LoadScene("Tutorial_scene");

    }
}
