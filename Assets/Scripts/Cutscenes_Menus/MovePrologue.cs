using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MovePrologue : MonoBehaviour
{
    public Camera cam;
    float time = 0;
    float spriteWidth;
    float spriteHeight;
    public RectTransform rt;



    void Update()
    {

        Vector3[] v = new Vector3[4];
        rt.GetWorldCorners(v);
        float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;
        float edgeVisiblePositionRight = v[3].x - camHorizontalExtend;


        time += Time.deltaTime;
        if (time > 13f && edgeVisiblePositionRight - 0.1f >= cam.transform.position.x)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - 9f * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);

        }

        if (time > 30 || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene("Tutorial_scene");

    }
}
