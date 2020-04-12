using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dotykCutscene : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            SceneManager.LoadScene("Tutorial_scene");
    }
    
}
