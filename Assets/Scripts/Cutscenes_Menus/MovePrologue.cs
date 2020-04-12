using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovePrologue : MonoBehaviour
{

    float time = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time > 13f && gameObject.transform.position.x > -17)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - 6f * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);

        }

        if (time > 30 || Input.GetKeyDown(KeyCode.Mouse0))
            SceneManager.LoadScene("Tutorial_scene");

    }
}
