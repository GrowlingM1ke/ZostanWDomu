using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class introductionScreen : MonoBehaviour
{
    public GameObject music;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            DontDestroyOnLoad(music);
            SceneManager.LoadScene("Main_Menu");
        }
    }
}
